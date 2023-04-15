using UnityEngine;
using ReadyPlayerMe.AvatarLoader;
using StarterAssets;

public class AvatarLoader : MonoBehaviour
{
    enum AvatarType {Menu, Gameplay, Cutscene};

    [SerializeField] private AvatarLoaderDataSO avatarLoaderDataSO;
    private GameObject avatar;
    private AvatarObjectLoader avatarObjectLoader;
    [SerializeField]
    [Tooltip("Animator to use on loaded avatar")]
    private RuntimeAnimatorController animatorController;
    [SerializeField]
    [Tooltip("If true it will try to load avatar on start from avatarUrl inside the avatarLoaderDataSO")]
    private bool loadOnStart = true;
    [SerializeField]
    [Tooltip("Preview avatar to display until avatar loads. Will be destroyed after new avatar is loaded")]
    private GameObject avatarLoadingInProgress;
    //[Tooltip("Indicate the context of loading the avatar")]
    [SerializeField] private AvatarType currentAvatarType;
    //[Tooltip("Only to reference in a gameplay context to get the needed componenets from the template")]
    [SerializeField] private GameObject avatarComponentsTemplate;

    private const string CAMERA_TARGET_OBJECT_NAME = "CameraTarget";

    void Start()
    {
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.OnCompleted += OnLoadCompleted;
        avatarObjectLoader.OnFailed += OnLoadFailed;

        if (avatarLoadingInProgress != null) avatarLoadingInProgress.SetActive(true);
        if (loadOnStart) LoadAvatar(avatarLoaderDataSO.avatarURL);
    }

    public void LoadAvatar(string avatarUrl)
    {
        if (avatarUrl != null)
        {
            //If we are loading a new avatar, we want to update the data SO so it is saved to be use for gameplay later
            if (avatarUrl != avatarLoaderDataSO.avatarURL)  avatarLoaderDataSO.avatarURL = avatarUrl;
            avatarLoadingInProgress.SetActive(true);
            if(avatar!=null) avatar.SetActive(false);
            avatarObjectLoader.AvatarConfig = avatarLoaderDataSO.Config;
            avatarObjectLoader.LoadAvatar(avatarUrl);
        }
    }

    private void OnLoadFailed(object sender, FailureEventArgs args)
    {
        Debug.LogError("Avatar failed to load");
    }

    private void OnLoadCompleted(object sender, CompletionEventArgs args)
    {
        if (avatarLoadingInProgress != null) avatarLoadingInProgress.SetActive(false);
        SetupAvatar(args.Avatar);
    }

    private void SetupAvatar(GameObject targetAvatar)
    {
        if (avatar != null)
        {
            Destroy(avatar);
        }

        avatar = targetAvatar;
        // Re-parent and set transforms from data scriptable object
        avatar.transform.parent = transform;
        avatar.transform.localPosition = avatarLoaderDataSO.SpawnPosition;
        avatar.transform.localRotation = Quaternion.Euler(avatarLoaderDataSO.SpawnRotation);
        avatar.transform.localScale = avatarLoaderDataSO.SpawnScale;

        if (animatorController != null)
        {
            Animator animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.applyRootMotion = false;
        }

        //If we are in a gameplay context, we want to add the components needed
        if (currentAvatarType == AvatarType.Gameplay)
        {
            avatarComponentsTemplate.transform.parent = avatar.transform;

            // Create camera follow target
            GameObject cameraTarget = new GameObject(CAMERA_TARGET_OBJECT_NAME);
            cameraTarget.transform.parent = avatar.transform;
            cameraTarget.transform.localPosition = new Vector3(0, 1.5f, 0);
            cameraTarget.tag = "CinemachineTarget";
            avatarComponentsTemplate.GetComponent<ThirdPersonController>().SetCinemachineCameraTarget(cameraTarget);
            //SetupCharacter();
        }
    }

    //private void SetupCharacter()
    //{

    //    // Cache selected object to add the components
    //    GameObject character = this.gameObject;


    //    character.tag = "Player";

    //    // Create camera follow target
    //    GameObject cameraTarget = new GameObject(CAMERA_TARGET_OBJECT_NAME);
    //    cameraTarget.transform.parent = character.transform;
    //    cameraTarget.transform.localPosition = new Vector3(0, 1.5f, 0);
    //    cameraTarget.tag = "CinemachineTarget";



    //    character.AddComponent<ThirdPersonController>();
    //    CopyComponent(avatarComponentsTemplate.GetComponent<ThirdPersonController>(), this.gameobject.GetComponent<ThirdPersonController>());



    //    //// Add tp controller and set values
    //    //ThirdPersonController tpsController = character.AddComponent<ThirdPersonController>();
    //    //tpsController.GroundedOffset = 0.1f;
    //    //tpsController.GroundLayers = 1;
    //    //tpsController.JumpTimeout = 0.5f;
    //    //tpsController.CinemachineCameraTarget = cameraTarget;
    //    //tpsController.LandingAudioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(LANDING_AUDIO_PATH);
    //    //tpsController.FootstepAudioClips = new AudioClip[]
    //    //{
    //    //    AssetDatabase.LoadAssetAtPath<AudioClip>($"{FOOTSTEP_AUDIO_PATH}_01.wav"),
    //    //    AssetDatabase.LoadAssetAtPath<AudioClip>($"{FOOTSTEP_AUDIO_PATH}_02.wav")
    //    //};

    //    //// Add character controller and set size
    //    //CharacterController characterController = character.GetComponent<CharacterController>();
    //    //characterController.center = new Vector3(0, 1, 0);
    //    //characterController.radius = 0.3f;
    //    //characterController.height = 1.9f;

    //    //// Add components with default values
    //    //character.AddComponent<BasicRigidBodyPush>();
    //    //character.AddComponent<StarterAssetsInputs>();

    //    //// Add player input and set actions asset
    //    //PlayerInput playerInput = character.GetComponent<PlayerInput>();
    //    //playerInput.actions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(INPUT_ASSET_PATH);

    //    //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

    //    //// 
    //    //var camera = Object.FindObjectOfType<CinemachineVirtualCamera>();
    //    //if (camera)
    //    //{
    //    //    camera.Follow = cameraTarget.transform;
    //    //    camera.LookAt = cameraTarget.transform;
    //    //}
    //}

    //T CopyComponent<T>(T original, GameObject destination) where T : Component
    //{
    //    System.Type type = original.GetType();
    //    Component copy = destination.AddComponent(type);
    //    System.Reflection.FieldInfo[] fields = type.GetFields();
    //    foreach (System.Reflection.FieldInfo field in fields)
    //    {
    //        field.SetValue(copy, field.GetValue(original));
    //    }
    //    return copy as T;
    //}
}

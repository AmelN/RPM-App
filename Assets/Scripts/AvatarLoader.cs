using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

public class AvatarLoader : MonoBehaviour
{
    enum AvatarType {Menu, Gameplay, Cutscene};

    [Tooltip("Scriptable object containing loader config and transform data ")]
    [SerializeField] private AvatarLoaderDataSO avatarLoaderDataSO;
    [Tooltip("Scriptable object containing the avatar url")]
    public AvatarUrlDataSO avatarUrlDataSO;
    [Tooltip("Animator to use on loaded avatar")]
    [SerializeField]private RuntimeAnimatorController animatorController;
    [Tooltip("If true it will try to load avatar on start from avatarUrl inside the avatarLoaderDataSO")]
    [SerializeField] private bool loadOnStart = true;
    [Tooltip("Preview avatar to display until avatar loads. Will be destroyed after new avatar is loaded")]
    [SerializeField] private GameObject avatarLoadingInProgress;
    [Tooltip("Indicate the context of loading the avatar")]
    [SerializeField] private AvatarType currentAvatarType;
    [Tooltip("Only to reference in a gameplay context to get the needed gameplay componenets from the template")]
    [SerializeField] private GameObject gameplayAvatarTemplate;
    private GameObject avatar;
    private AvatarObjectLoader avatarObjectLoader;

    void Start()
    {
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.OnCompleted += OnLoadCompleted;
        avatarObjectLoader.OnFailed += OnLoadFailed;

        if (avatarLoadingInProgress != null) avatarLoadingInProgress.SetActive(true);
        if (loadOnStart) LoadAvatar(avatarUrlDataSO.avatarURL);
        if(gameplayAvatarTemplate != null) gameplayAvatarTemplate.SetActive(false);
    }

    public void LoadAvatar(string avatarUrl)
    {
        if (avatarUrl != null)
        {
            //If we are loading a new avatar, we want to update the data SO so it is saved to be use for gameplay later
            if (avatarUrl != avatarUrlDataSO.avatarURL) avatarUrlDataSO.avatarURL = avatarUrl;
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

        if (currentAvatarType == AvatarType.Menu && animatorController != null)
        {
            Animator animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.applyRootMotion = false;
        }
        //If we are in a gameplay context, we want to add the components needed
        else if (currentAvatarType == AvatarType.Gameplay)
        {
            //We move both Armature and Renderer_Avatar under the avatar template so we have all the componenets needed for gameplay
            avatar.transform.GetChild(1).parent = gameplayAvatarTemplate.transform;
            avatar.transform.GetChild(0).parent = gameplayAvatarTemplate.transform;
            gameplayAvatarTemplate.SetActive(true);
            //We remove the parent of loaded avatar as we don't need it after moving Armature and Renderer_Avatar under gameplay avatar
            Destroy(transform.GetChild(0).gameObject);
        }
        //If we are in a gameplay context, we want to play the cutscene
        else if (currentAvatarType == AvatarType.Cutscene)
        {
            CutsceneTrigger cutsceneTrigger = GameObject.FindWithTag("CutsceneTrigger").GetComponent<CutsceneTrigger>();
            cutsceneTrigger.PlayCutscene();
            cutsceneTrigger.BindTimeline("CutsceneAvatarActivationTrack", avatar.GetComponent<Animator>());
            cutsceneTrigger.BindTimeline("CutsceneAvatarAnimationTrack", avatar.GetComponent<Animator>());
        }
    }
}

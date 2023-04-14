using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

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
    [Tooltip("If menu it Will show UI indicating loading in progress as a visual feedback. Otherwise it shows a black screen while loading")]
    [SerializeField] private AvatarType currentAvatarType;

    void Start()
    {
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.OnCompleted += OnLoadCompleted;
        avatarObjectLoader.OnFailed += OnLoadFailed;

        if (currentAvatarType == AvatarType.Menu && avatarLoadingInProgress != null) avatarLoadingInProgress.SetActive(true);
        if (loadOnStart) LoadAvatar(avatarLoaderDataSO.avatarURL);
    }

    public void LoadAvatar(string avatarUrl)
    {
        if (avatarUrl != null)
        {
            //If we are loading a new avatar, we want to update the data SO so it is saved to be use for gameplay later
            if (avatarUrl != avatarLoaderDataSO.avatarURL)  avatarLoaderDataSO.avatarURL = avatarUrl;
            if (currentAvatarType == AvatarType.Menu) avatarLoadingInProgress.SetActive(true);
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
        if (currentAvatarType == AvatarType.Menu && avatarLoadingInProgress != null) avatarLoadingInProgress.SetActive(false);
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
            avatar.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        }
    }
}

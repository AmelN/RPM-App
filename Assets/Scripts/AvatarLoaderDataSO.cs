using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

[CreateAssetMenu(fileName = "AvatarLoaderDataSO", menuName = "ScriptableObjects/AvatarLoaderDataSO", order = 1)]
public class AvatarLoaderDataSO : ScriptableObject
{

    public string avatarURL;

    [Header("Avatars Configs")]
    public AvatarConfig mainMenu;
    public AvatarConfig gameplay;
    public AvatarConfig cutscene;

    public Vector3 mainMenuSpawnPosition;
    public Vector3 gameplaySpawnPosition;
}

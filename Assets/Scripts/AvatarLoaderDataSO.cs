using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

[CreateAssetMenu(fileName = "AvatarLoaderDataSO", menuName = "ScriptableObjects/AvatarLoaderDataSO", order = 1)]
public class AvatarLoaderDataSO : ScriptableObject
{

    public string avatarURL;

    [Header("Avatars Configs")]
    public AvatarConfig mainMenuConfig;
    public AvatarConfig gameplayConfig;
    public AvatarConfig cutsceneConfig;

    [Header("Main menu avatar transform")]
    public Vector3 MMSpawnPosition;
    public Vector3 MMSpawnRotation;
    public Vector3 MMSpawnScale;

    [Header("Gameplay avatar transform")]
    public Vector3 GPSpawnPosition;
    public Vector3 GPSpawnRotation;
    public Vector3 GPSpawnScale;
}

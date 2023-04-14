using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

[CreateAssetMenu(fileName = "AvatarLoaderDataSO", menuName = "ScriptableObjects/AvatarLoaderDataSO", order = 1)]
public class AvatarLoaderDataSO : ScriptableObject
{
    public string avatarURL;
    public AvatarConfig Config;

    [Header("Main menu avatar transform")]
    public Vector3 MMSpawnPosition;
    public Vector3 MMSpawnRotation;
    public Vector3 MMSpawnScale;
}

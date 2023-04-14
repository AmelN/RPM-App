using UnityEngine;
using ReadyPlayerMe.AvatarLoader;

[CreateAssetMenu(fileName = "AvatarLoaderDataSO", menuName = "ScriptableObjects/AvatarLoaderDataSO", order = 1)]
public class AvatarLoaderDataSO : ScriptableObject
{
    public string avatarURL;
    public AvatarConfig Config;

    [Header("Avatar spwan transform")]
    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;
    public Vector3 SpawnScale;
}

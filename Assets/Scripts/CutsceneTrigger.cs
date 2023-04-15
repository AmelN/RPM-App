using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private AvatarLoader avatarLoader;
    [SerializeField] private PlayableDirector playableDirector;

    private bool cutscenePlayed;
    private void OnTriggerEnter(Collider other)
    {
        if (!cutscenePlayed && other.gameObject.GetComponent<CharacterController>() != null)
        {
            avatarLoader.LoadAvatar(avatarLoader.GetComponent<AvatarLoader>().avatarUrlDataSO.avatarURL);
        }
    }

    public void PlayCutscene()
    {
        cutscenePlayed = true;
        GetComponent<PlayableDirector>().Play();
    }

    public void BindTimeline(string trackName, Animator objectToBind)
    {
        //The "outputs" in this case are the tracks of the PlayableAsset
        foreach (var playableAssetOutput in playableDirector.playableAsset.outputs)
        {
            if (playableAssetOutput.streamName == trackName)
            {
                playableDirector.SetGenericBinding(playableAssetOutput.sourceObject, objectToBind);

                break;
            }
        }
    }
}

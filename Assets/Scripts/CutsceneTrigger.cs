using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private AvatarLoader avatarLoader;
    [SerializeField] private PlayableDirector playableDirector;
    private bool cutscenePlayed;

    void OnEnable()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

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

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (playableDirector == aDirector)
            avatarLoader.gameObject.SetActive(false);
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

    void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}

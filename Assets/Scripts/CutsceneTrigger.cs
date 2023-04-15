using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private AvatarLoader cutsceneAvatarLoader;
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
            cutsceneAvatarLoader.LoadAvatar(cutsceneAvatarLoader.GetComponent<AvatarLoader>().avatarUrlDataSO.avatarURL);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void PlayCutscene()
    {
        cutscenePlayed = true;
        GetComponent<PlayableDirector>().Play();
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        // We disable the cutscene avatar when cutscene ends, so we can resume with gameplay avatar
        if (playableDirector == aDirector)
            cutsceneAvatarLoader.gameObject.SetActive(false);
    }

    public void BindTimeline(string trackName, Animator objectToBind)
    {
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

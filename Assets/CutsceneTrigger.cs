using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    private bool cutscenePlayed;
    private void OnTriggerEnter(Collider other)
    {
        if (!cutscenePlayed && other.gameObject.GetComponent<CharacterController>() != null)
        {
            cutscenePlayed = true;
            GetComponent<PlayableDirector>().Play();
        }
    }
}

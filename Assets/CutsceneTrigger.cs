using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            GetComponent<PlayableDirector>().Play();
        }
    }
}

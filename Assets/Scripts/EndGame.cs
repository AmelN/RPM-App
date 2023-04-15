using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject gameEndingUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            SceneManager.LoadScene("MainMenu");

            ////Show UI to go back to menu
            //gameEndingUI.SetActive(true);
            ////We want to disable gameplay input when we show UI 
            //other.gameObject.SetActive(false);
        }
    }
}

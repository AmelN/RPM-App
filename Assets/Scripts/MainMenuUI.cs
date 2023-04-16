using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private GameObject ChangeAvatartUI;
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowOptions()
    {
        Debug.Log("Not implemented");
    }

    public void DisplayChangeAvatarUI()
    {
        ChangeAvatartUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

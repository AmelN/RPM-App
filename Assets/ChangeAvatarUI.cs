using UnityEngine;
using TMPro;

public class ChangeAvatarUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private MainMenuAvatarLoader mainMenuAvatarLoader;

    public void LoadNewAvatar()
    {
        if (!string.IsNullOrEmpty(inputUI.text))
        {
            mainMenuAvatarLoader.LoadAvatar(inputUI.text.Trim(' '));
        }
        else
        {
            Debug.LogWarning("url can't be empty!");
        }
    }

    public void BackToMainMenu()
    {
        mainMenuUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

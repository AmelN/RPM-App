using UnityEngine;
using TMPro;

public class ChangeAvatarUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private MainMenuAvatarLoader mainMenuAvatarLoader;

    public void LoadNewAvatar()
    {
        if (inputUI.text != null)
        {
            mainMenuAvatarLoader.LoadAvatar(inputUI.text.Trim(' '));
        }
    }

    public void BackToMainMenu()
    {
        mainMenuUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

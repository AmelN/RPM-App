using UnityEngine;
using TMPro;

public class ChangeAvatarUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private AvatarLoader avatarLoader;

    public void LoadNewAvatar()
    {
        if (!string.IsNullOrEmpty(inputUI.text))
        {
            avatarLoader.LoadAvatar(inputUI.text.Trim(' '));
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

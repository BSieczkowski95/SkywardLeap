using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReturnToMainMenuButton : MonoBehaviour
{
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(PlayButtonClickSound);
    }

    public void OnReturnToMainMenuButton()
    {
        Time.timeScale = 1; // Resuming the game time
        SceneManager.LoadScene("GameStart");
    }

    private void PlayButtonClickSound()
    {
        SoundManager.Instance.PlaySound("buttonClick");
    }
}

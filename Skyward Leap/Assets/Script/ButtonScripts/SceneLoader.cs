using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Dropdown levelDropdown;

    public void LoadSelectedLevel()
    {
        string selectedLevel = levelDropdown.options[levelDropdown.value].text;
        SceneManager.LoadScene(selectedLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonClickSound()
    {
        SoundManager.Instance.PlaySound("buttonClick");
    }
}

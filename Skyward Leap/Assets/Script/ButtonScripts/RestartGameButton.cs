using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// This script manages the behavior of the Restart Game button in a Unity game.

public class RestartGameButton : MonoBehaviour
{   
    // Start is called before the first frame update.
    void Start()
    {
        // This line retrieves the Button component attached to this GameObject.
        var button = GetComponent<Button>();

        // This line adds a listener to the button which triggers the PlayButtonClickSound method when the button is clicked.
        button.onClick.AddListener(PlayButtonClickSound);
    }

    // This public method is called to restart the current game level.
    public void OnRestartGame()
    {
        Time.timeScale = 1; // Resuming the game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // This line reloads the currently active scene, effectively restarting the game.
    }

    // This private method is called to play the button click sound.
    private void PlayButtonClickSound()
    {
        // This line plays the "buttonClick" sound through the SoundManager.
        SoundManager.Instance.PlaySound("buttonClick");
    }
}


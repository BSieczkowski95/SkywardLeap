using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    // This public method is called to load the "Level 2" scene.
    public void LoadLevel2()
    {
        // This line loads the scene named "Level 2".
        SceneManager.LoadScene("Level 2");
    }

    // This public method is called to play the button click sound.
    public void PlayButtonClickSound()
    {
        // This line plays the "buttonClick" sound through the SoundManager.
        SoundManager.Instance.PlaySound("buttonClick");
    }
}

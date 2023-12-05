using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel1Button : MonoBehaviour
{
    // This line of code is called when the script starts.
    void Start()
    {
        // This line gets the Button component attached to this GameObject.
        var button = GetComponent<Button>();
        // This line adds a listener to the button's onClick event, which triggers PlayButtonClickSound when clicked.
        button.onClick.AddListener(PlayButtonClickSound);
    }

    // This public method is called to load the "Level 1" scene.
    public void LoadLevel1()
    {
        // This line loads the scene named "Level 1".
        SceneManager.LoadScene("Level 1");
    }

    // This private method is called when the button is clicked.
    private void PlayButtonClickSound()
    {
        // This line plays the "buttonClick" sound through the SoundManager.
        SoundManager.Instance.PlaySound("buttonClick");
    }
}

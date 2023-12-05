using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource soundEffectSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip levelCompleteSound;
    public AudioClip buttonClickSound;
    public AudioClip sawInteractionSound;
    public AudioClip coinSound; // Add this line for coin sound

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "jump":
                soundEffectSource.PlayOneShot(jumpSound);
                break;
            case "death":
                soundEffectSource.PlayOneShot(deathSound);
                break;
            case "levelComplete":
                soundEffectSource.PlayOneShot(levelCompleteSound);
                break;
            case "buttonClick":
                soundEffectSource.PlayOneShot(buttonClickSound);
                break;
            case "sawInteraction":
                soundEffectSource.PlayOneShot(sawInteractionSound);
                break;
            case "coin": // Add this case for playing coin sound
                soundEffectSource.PlayOneShot(coinSound);
                break;
        }
    }
}

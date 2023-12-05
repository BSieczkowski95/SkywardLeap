using UnityEngine;

public class ClosePrefabButton : MonoBehaviour
{
    public GameObject prefabToClose; // Assign the prefab you want to close in the inspector

    public void ClosePrefab()
    {
        // Destroy the prefab instance
        if (prefabToClose != null)
        {
            Destroy(prefabToClose);
        }
    }

    public void PlayButtonClickSound()
    {
        // Play the button click sound
        SoundManager.Instance.PlaySound("buttonClick");
    }
}

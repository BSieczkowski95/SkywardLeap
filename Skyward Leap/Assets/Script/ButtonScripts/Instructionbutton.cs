using UnityEngine;

public class InstructionButtonBehavior : MonoBehaviour
{
    public GameObject instructionsPrefab; // Assign this in the inspector

    public void ShowInstructions()
    {
        // Instantiate the instructions prefab
        Instantiate(instructionsPrefab, Vector3.zero, Quaternion.identity);
    }
    public void PlayButtonClickSound()
    {
        SoundManager.Instance.PlaySound("buttonClick");
    }
}

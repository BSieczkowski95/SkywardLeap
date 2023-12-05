using UnityEngine;

public class Platform : MonoBehaviour
{
    public int levelNumber; // Variable to identify the level

    // Variables for movement (used only in Level 2)
    public float minSpeed = 0.1f;
    public float maxSpeed = 1f;
    public float minHeight = 0f;
    public float maxHeight = 10f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    public bool IsLastPlatform { get; set; } = false;
    private float speed;
    private bool movingRight = true;

    void Start()
    {
        if (levelNumber == 2) // Check if it's Level 2
        {
            // Calculate the speed based on the platform's height
            float heightFactor = Mathf.InverseLerp(minHeight, maxHeight, transform.position.y);
            speed = Mathf.Lerp(minSpeed, maxSpeed, heightFactor);
        }
    }

    void Update()
    {
        if (levelNumber == 2 && !IsLastPlatform) // Check if it's Level 2
        {
            // Move the platform left and right
            if (movingRight)
            {
                if (transform.position.x >= rightBoundary)
                {
                    movingRight = false;
                }
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                if (transform.position.x <= leftBoundary)
                {
                    movingRight = true;
                }
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
    }
}


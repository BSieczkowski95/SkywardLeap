using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private bool movingRight;
    private float offScreenOffset = 5f; // Offset for when the cloud is considered off screen
    private float directionChangeDelay = 1f;
    private float timeSinceOffScreen = 0f;

    void Start()
    {
        // Randomly decide the initial direction
        movingRight = Random.Range(0, 2) == 0;
    }

    void Update()
    {
        // Move the cloud
        transform.position += (movingRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;

        // Check if the cloud is off screen
        if (movingRight && transform.position.x > Camera.main.orthographicSize * Camera.main.aspect + offScreenOffset
            || !movingRight && transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect - offScreenOffset)
        {
            if (timeSinceOffScreen <= 0f)
            {
                timeSinceOffScreen = Time.time;
            }
            else if (Time.time - timeSinceOffScreen > directionChangeDelay)
            {
                movingRight = !movingRight; // Change direction
                timeSinceOffScreen = 0f;
            }
        }
    }
}

using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;
    public float rotationSpeed = 200f;
    private bool movingRight = true;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        MoveSaw();
    }

    private void MoveSaw()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.x > rightBoundary)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.x < leftBoundary)
                movingRight = true;
        }
    }
}

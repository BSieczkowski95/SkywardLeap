using UnityEngine;

// Define a class for infinite background behavior
public class InfiniteBackground : MonoBehaviour
{
    // Array to store the background GameObjects
    public GameObject[] backgrounds; 
    // Array to store the side wall GameObjects
    public GameObject[] sideWalls;   
    // Variable to control the speed of the background movement
    public float backgroundSpeed;
    // Reference to the main camera in the scene
    public Camera mainCamera;

    // Array to store the heights of each background image
    private float[] imageHeights;
    // Array to store the heights of each side wall
    private float[] wallHeights;     
    // Variable to keep track of the camera's last Y position
    private float lastCameraY;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the imageHeights array with the number of backgrounds
        imageHeights = new float[backgrounds.Length];
        // Initialize the wallHeights array with the number of side walls
        wallHeights = new float[sideWalls.Length];

        // Loop through each background and store its height
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Get the SpriteRenderer component of the background
            SpriteRenderer spriteRenderer = backgrounds[i].GetComponent<SpriteRenderer>();
            // Store the height of the background's sprite
            imageHeights[i] = spriteRenderer.sprite.bounds.size.y;
        }

        // Loop through each side wall and store its height
        for (int i = 0; i < sideWalls.Length; i++)
        {
            // Get the SpriteRenderer component of the side wall
            SpriteRenderer wallRenderer = sideWalls[i].GetComponent<SpriteRenderer>();
            // Store the height of the side wall's sprite
            wallHeights[i] = wallRenderer.sprite.bounds.size.y;
        }

        // Store the initial Y position of the main camera
        lastCameraY = mainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the change in camera's Y position since the last frame
        float deltaY = mainCamera.transform.position.y - lastCameraY;
        // Update the lastCameraY to the current Y position
        lastCameraY = mainCamera.transform.position.y;

        // Move and loop the background objects based on deltaY
        MoveAndLoopObjects(backgrounds, imageHeights, deltaY);
        // Move and loop the side wall objects based on deltaY
        MoveAndLoopObjects(sideWalls, wallHeights, deltaY);
    }

    // Method to move and loop the background and side wall objects
    private void MoveAndLoopObjects(GameObject[] objects, float[] heights, float deltaY)
    {
        // Loop through each object in the array
        for (int i = 0; i < objects.Length; i++)
        {
            // Move the object downwards based on the background speed and deltaY
            objects[i].transform.Translate(Vector2.down * (backgroundSpeed * deltaY));

            // Check if the object's Y position is below a threshold based on its height
            if (objects[i].transform.position.y < mainCamera.transform.position.y - heights[i])
            {
                // Calculate the new reset position for the object
                Vector2 resetPosition = new Vector2(objects[i].transform.position.x, objects[i].transform.position.y + 2 * heights[i]);
                // Reset the object's position to the top
                objects[i].transform.position = resetPosition;
            }
        }
    }
}

//This script helps me make a never-ending scrolling background
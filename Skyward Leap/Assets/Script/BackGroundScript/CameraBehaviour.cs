using UnityEngine;

// Define a class for camera follow behavior
public class CameraFollow : MonoBehaviour
{
    // Public variable to assign the player's Transform component
    public Transform player;
    // Public variable to set the offset between the camera and the player
    public Vector3 offset;
    // Public variable to set the fixed Z-axis position of the camera
    public float fixedZAxis = -10f;
    // Private variable to determine if the camera should follow the player
    private bool followPlayer = true;

    // Update is called once per frame
    void Update()
    {
        // Check if the camera should follow the player and if the player's Y position is higher than the camera's Y position
        if (followPlayer && player.position.y > transform.position.y)
        {
            // Set the camera's position to follow the player's position with an offset and a fixed Z-axis value
            transform.position = new Vector3(transform.position.x, player.position.y + offset.y, fixedZAxis);
        }
    }

    // Public method to stop the camera from following the player
    public void StopFollowing()
    {
        // Set followPlayer to false to stop the camera from following the player
        followPlayer = false;
    }
}

//I wrote this script to help the camera follow my player's up and down movements. 
//It keeps the camera a set distance away from the player, which I can adjust with the 
//offset setting. Also, the camera stays at the same height all the time, it doesn't 
//go up or down. If I ever need the camera to stop following my player, I just use the 
//StopFollowing function I added.

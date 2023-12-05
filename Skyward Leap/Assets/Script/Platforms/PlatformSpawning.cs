using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab for platforms to spawn.
    public Transform player; // Reference to the player's Transform component.
    public Text textscore; // UI Text to display Score.
    public GameObject completionMessagePrefab; // Prefab for the completion message.
    public float spawnRate = 2f; // Rate at which platforms are spawned.
    public float verticalDistance = 2f; // Vertical distance between each platform.
    public float minX = -5f; // Minimum X position for spawning platforms.
    public float maxX = 5f; // Maximum X position for spawning platforms.
    public float initialScaleX = 1.2f; // Initial scale in X-axis for platforms.
    public float minScaleX = 0.5f; // Minimum scale in X-axis for platforms.
    public float initialSpawnHeight = 15f; // Initial height above the player for spawning platforms.
    private Camera mainCamera; // Reference to the main camera in the scene.
    private float screenWidth; // To store screen width for full-width platforms.
    private bool levelCompleted = false; // Flag to check if the level is completed.
    
    void Start()
    {
        // Initializing camera and screen width.
        mainCamera = Camera.main;
        screenWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        
        // Pre-generate platforms at the start of the game.
        PreGeneratePlatforms();
    }

    void Update()
    {
        // Check if level is completed, and if so, stop further updates.
        if (levelCompleted)
        {
            return;
        }
    }

    void PreGeneratePlatforms()
    {
        // Calculate initial spawn height based on the player's position.
        float spawnHeight = player.position.y + initialSpawnHeight;

        // Loop to spawn a set number of platforms.
        for (int i = 0; i < 50; i++)
        {
            // On the last iteration, spawn a full-width platform.
            if (i == 49)
            {
                SpawnFullWidthPlatform(spawnHeight);
            }
            else
            {
                // For other iterations, spawn regular platforms.
                SpawnPlatform(spawnHeight);
            }
            spawnHeight += verticalDistance; // Increment the spawn height.
        }
    }

    public GameObject SpawnPlatform(float height)
    {
        // Randomize the X position within specified range.
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, height, 0);

        // Create a platform instance at the calculated position.
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Calculate and apply scale based on height.
        float scale = Mathf.Lerp(initialScaleX, minScaleX, (height - player.position.y) / 100f);
        newPlatform.transform.localScale = new Vector3(scale, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

        return newPlatform;
    }

    public GameObject SpawnFullWidthPlatform(float height)
    {
        // Position for full-width platform is centered on X axis.
        Vector3 spawnPosition = new Vector3(0, height, 0);

        // Create a full-width platform.
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.transform.localScale = new Vector3(screenWidth, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

        // Check and set if this platform is the last in the level.
        Platform platformComponent = newPlatform.GetComponent<Platform>();
        if (platformComponent != null)
        {
            platformComponent.IsLastPlatform = true;
        }

        return newPlatform;
    }
}

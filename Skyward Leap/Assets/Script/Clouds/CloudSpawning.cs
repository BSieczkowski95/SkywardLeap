using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public int cloudCount = 10;
    public float minCloudHeight = 25f;
    public float maxCloudHeight = 50f;
    public float verticalSpacing = 5f; // Adjustable vertical spacing between clouds
    public float minX;
    public float maxX;

    void Start()
    {
        minX = -Camera.main.orthographicSize * Camera.main.aspect;
        maxX = Camera.main.orthographicSize * Camera.main.aspect;

        float currentHeight = minCloudHeight;

        for (int i = 0; i < cloudCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            Instantiate(cloudPrefab, new Vector3(randomX, currentHeight, 0), Quaternion.identity);
            currentHeight += verticalSpacing; // Increment the height for the next cloud

            if (currentHeight > maxCloudHeight)
            {
                currentHeight = minCloudHeight; // Reset to minimum height if exceeded max height
            }
        }
    }
}

using UnityEngine;

public class SawSpawner : MonoBehaviour
{
    public GameObject sawPrefab;
    public int levelNumber;
    public float gapBetweenSaws = 15f; // Consistent gap between saws
    public float startHeightLevel1 = 20f; // Starting height for Level 1
    public float startHeightLevel2 = 30f; // Starting height for Level 2

    void Start()
    {
        int numberOfSawsToSpawn = (levelNumber == 1) ? 4 : 8;
        float startingHeight = levelNumber == 1 ? startHeightLevel1 : startHeightLevel2;
        SpawnSaws(numberOfSawsToSpawn, startingHeight);
    }

    void SpawnSaws(int maxSaws, float startingHeight)
    {
        float spawnHeight = startingHeight;
        for (int i = 0; i < maxSaws; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnHeight, transform.position.z);
            Instantiate(sawPrefab, spawnPosition, Quaternion.identity);
            spawnHeight += gapBetweenSaws;
        }
    }
}

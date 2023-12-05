using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int noOfCoinsLevel1 = 5;
    public int noOfCoinsLevel2 = 10;
    public float minXPosition = -5f;
    public float maxXPosition = 5f;
    public float gapBetweenCoins = 10f; // Consistent gap between coins
    public float startHeightLevel1 = 10f; // Starting position for Level 1
    public float startHeightLevel2 = 20f; // Starting position for Level 2

    void Start()
    {
        int noOfCoins = SceneManager.GetActiveScene().name == "Level 1" ? noOfCoinsLevel1 : noOfCoinsLevel2;
        float startingHeight = SceneManager.GetActiveScene().name == "Level 1" ? startHeightLevel1 : startHeightLevel2;
        SpawnCoins(noOfCoins, startingHeight);
    }

    void SpawnCoins(int numberOfCoins, float startingHeight)
    {
        float yPosition = startingHeight;
        for (int i = 0; i < numberOfCoins; i++)
        {
            float xPosition = Random.Range(minXPosition, maxXPosition);
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            yPosition += gapBetweenCoins;
        }
    }
}

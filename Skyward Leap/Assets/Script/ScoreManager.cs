using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text textScore;
    private int currentScore = 0;
    private int scoreFromHeight = 0; // Separate score based on height

    void Start()
    {
        ResetScore();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetScore();
    }

    public void UpdateScore(int playerHeight)
    {
        if (playerHeight > scoreFromHeight)
        {
            scoreFromHeight = playerHeight;
            UpdateTotalScore();
        }
    }

    public void AddToScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UpdateTotalScore();
    }

    private void UpdateTotalScore()
    {
        int totalScore = scoreFromHeight + currentScore;
        textScore.text = "Score: " + totalScore;
    }

    public int GetCurrentScore()
    {
        return scoreFromHeight + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreFromHeight = 0;
        textScore.text = "Score: " + (scoreFromHeight + currentScore);
    }
}

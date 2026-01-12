using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.ShaderGraph.Internal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Lives")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int pointsPerBrick = 10;
    [SerializeField] private float maxScoreMultiplier = 5f;

    [Header("High Score")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    private int highScore = 0;

    [Header("Bricks")]
    private int remainingBricks;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private BallMovement ball;

    private int score = 0;
    private int currentLives = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        remainingBricks = 0;
        currentLives = startingLives;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateLivesUI();
        UpdateScoreUI();
        UpdateHighScore();
    }

    public void RegisterBrick()
    {
        remainingBricks++;
    }
    
    public void BrickDestroyed()
    {
        remainingBricks--;

        if (remainingBricks <= 0)
        {
            WinGame();
        }
    }

    private void UpdateHighScore()
    {
        highScoreText.text = $"High Score: {highScore}";
    }

    private void CheckForHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            UpdateHighScore();
        }
    }

    public void AddScore()
    {
        float speedMultiplier = ball.CurrentSpeed / ball.BaseSpeed;

        speedMultiplier = Mathf.Clamp(speedMultiplier, 1f, maxScoreMultiplier);
        int pointsToAdd = Mathf.RoundToInt(pointsPerBrick * speedMultiplier);

        score += pointsToAdd;
        UpdateScoreUI();

        ball.IncreaseSpeed();
    }

    private void UpdateLivesUI()
    {
        livesText.text = $"Lives: {currentLives}";
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLivesUI();

        AudioManager.Instance.PlaySound(AudioManager.Instance.lifeLost);

        if (currentLives <= 0)
        {
            EndGame();
        }
        else
        {
            ball.ResetBall();
        }
    }

    private void EndGame()
    {
        CheckForHighScore();
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }

    private void WinGame()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.winSound);
        CheckForHighScore();
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        Debug.Log("YOU WIN!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

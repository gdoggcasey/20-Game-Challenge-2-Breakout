using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Lives")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int pointsPerBrick = 10;

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
        currentLives = startingLives;
        UpdateLivesUI();
        UpdateScoreUI();
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

    public void AddScore()
    {
        score += pointsPerBrick;
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
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }

    private void WinGame()
    {
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

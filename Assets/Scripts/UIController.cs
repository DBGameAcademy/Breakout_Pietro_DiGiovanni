using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] List<GameObject> livesIcons = new List<GameObject>();
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject pauseGamePanel;
    [SerializeField] GameObject winGamePanel;


    public static UIController Instance = null;

    void Awake()
    {
        Instance = (Instance == null) ? this : null;
    }

    void Start()
    {
        ActiveGameOverPanel(false);
        ActivePauseGamePanel(false);
        ActiveStartGamePanel(true);
    }

    public void UpdateScoreText(int scoreValue)
    {
        scoreText.text = "Score: " + scoreValue;
    }

    public void UpdateLives(int currentLives)
    {
        for (int totalLives = livesIcons.Count - 1; totalLives >= 0; totalLives--)
        {
            livesIcons[totalLives].SetActive(currentLives > totalLives);
        }
    }

    public void ActiveGameOverPanel(bool isActive)
    {
        gameOverPanel.SetActive(isActive);
    }

    public void ActiveStartGamePanel(bool isActive)
    {
        startGamePanel.SetActive(isActive);
    }

    public void ActivePauseGamePanel(bool isActive)
    {
        pauseGamePanel.SetActive(isActive);
    }

    public void ActiveWinnerPanel(bool isActive)
    {
        winGamePanel.SetActive(isActive);
    }
}

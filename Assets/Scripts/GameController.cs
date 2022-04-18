using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int _score = 0;
    int _initialLives = 3;
    int _lives = 3;

    bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }
    bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    [SerializeField] Ball ball;
    [SerializeField] Paddle paddle;
    [SerializeField] Vector3 initialBallPosition;

    public GameObject ExplosionFXPrefab;
    public GameObject[] effects;
    int randomIndex;

    public static GameController Instance = null;

    void Awake()
    {
        Instance = (Instance == null) ? this : null;
    }

    void Start()
    {
        PauseGame();
        StartCoroutine(SpawnRandomEffect());

        UIController.Instance.UpdateScoreText(_score);
        UIController.Instance.UpdateLives(_lives);
    }

    void Update()
    {
        randomIndex = Random.Range(0, 2);

        UIController.Instance.UpdateScoreText(_score);

        WinGame();
    }

    public void AddScore(int scoreValue)
    {
        _score += scoreValue;
    }

    public void LostBall()
    {
        ball.transform.position = initialBallPosition;

        RevertBall(ball);

        _lives--;

        UIController.Instance.UpdateLives(_lives);

        if (_lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isPlaying = false;
        UIController.Instance.ActiveGameOverPanel(true);
        PauseGame();
    }

    public void StartGame()
    {
        isPlaying = true;
        ResetGame();
        UnpauseGame();
    }

    void ResetGame()
    {
        _score = 0;
        _lives = _initialLives;

        UIController.Instance.ActiveStartGamePanel(false);
        UIController.Instance.ActiveGameOverPanel(false);
        UIController.Instance.ActiveWinnerPanel(false);

        UIController.Instance.UpdateScoreText(_score);
        UIController.Instance.UpdateLives(_lives);

        BlockController.Instance.ResetBlocks();

        ResetPaddle(paddle);
        RevertBall(ball);
        ball.transform.position = initialBallPosition;
        
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (isPlaying)
        {
            UIController.Instance.ActivePauseGamePanel(true);
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        UIController.Instance.ActivePauseGamePanel(false);
    }

    public void QuitGame()
    {
        isPlaying = false;
        PauseGame();

        UIController.Instance.UpdateScoreText(_score);
        UIController.Instance.UpdateLives(_lives);

        UIController.Instance.ActiveGameOverPanel(false);
        UIController.Instance.ActivePauseGamePanel(false);
        UIController.Instance.ActiveStartGamePanel(true);
    }

    void WinGame()
    {
        if (BlockController.Instance.BlocksCount() <= 0)
        {
            isPlaying = false;
            PauseGame();
            UIController.Instance.ActiveWinnerPanel(true);
        }
    }

    void RevertBall(Ball _ball)
    {
        Vector3 currentVelocity = _ball.Velocity;
        currentVelocity.y = Mathf.Abs(currentVelocity.y);
        _ball.Velocity = currentVelocity;
    }

    void ResetPaddle(Paddle _paddle)
    {
        _paddle.transform.position = _paddle.InitialPosition;
        _paddle.GetComponent<SpriteRenderer>().size = _paddle.InitialSize;
    }

    IEnumerator SpawnRandomEffect()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(10f);

            Instantiate(effects[randomIndex], new Vector2(Random.Range(-2, 2f), 0f), Quaternion.identity);

        }
    }
}

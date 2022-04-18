using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gameController;
    Paddle paddle;
    bool isVerticalPaddle = false;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        paddle = FindObjectOfType<Paddle>();
    }

    void Update()
    {
        if (gameController.IsPlaying && gameController.IsPaused)
        {
            if (Input.anyKeyDown && !Input.GetMouseButton(0))
            {
                gameController.UnpauseGame();
            }
        }

        else if (gameController.IsPlaying && !gameController.IsPaused)
        {
            InputChangeRotation();

            InputGoLeft();
            InputGoRight();
        }

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            GameController.Instance.PauseGame();
        }
    }

    public void InputChangeRotation()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isVerticalPaddle = true;
            paddle.transform.eulerAngles = new Vector3(0, 0, 90);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            isVerticalPaddle = false;
            paddle.transform.rotation = Quaternion.identity;
        }
    }

    public void InputGoLeft()
    {

        if (isVerticalPaddle)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                paddle.Move(Vector2.up);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                paddle.Move(Vector2.left);
            }
        }
    }

    public void InputGoRight()
    {

        if (isVerticalPaddle)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                paddle.Move(Vector2.down);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                paddle.Move(Vector2.right);
            }
        }
    }
}

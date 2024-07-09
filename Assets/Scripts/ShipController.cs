using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class ShipController : NetworkBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector3 newPosition;

    private CountdownController countdownController;
    public GameOver1 gameOver1;

    private static bool gameEnded = false; 

    [SerializeField] public uint playerId; 

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game_1")
        {
            this.enabled = false; // Disable this script if not in Game_2
            return;
        }
        else
        {
            this.enabled = true;
        }

        // find CountdownController
        countdownController = FindObjectOfType<CountdownController>();
        if (countdownController == null)
        {
            Debug.LogError("CountdownController not found in the scene.");
        }

        gameOver1 = FindObjectOfType<GameOver1>();
        if (gameOver1 == null)
        {
            Debug.LogError("GameOver2 not found in the scene.");
        }

        ResetGame(); 
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game_1")
        {
            return;
        }

        if (!gameEnded)
        {
            HandleTouchInput();
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    touchEndPos = touch.position;
                    MovePlayer();
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    touchStartPos = Vector2.zero;
                    touchEndPos = Vector2.zero;
                    break;
            }
        }
    }

    void MovePlayer()
    {
        Vector3 screenToWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchEndPos.x, touchEndPos.y, Camera.main.nearClipPlane));
        newPosition = new Vector3(screenToWorldPos.x, screenToWorldPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name != "Game_1")
        {
            return;
        }

        if (collision.gameObject.CompareTag("Meteor"))
        {
            // Trigger game over
            gameOver1 = FindObjectOfType<GameOver1>();
            GameOver();
        }
    }

    void GameOver()
    {
        if (SceneManager.GetActiveScene().name != "Game_1")
        {
            return;
        }

        Debug.Log("Oyun Bitti!");
        gameEnded = true; 

        // all players should know the game has finished from server 
        if (isServer)
        {
            RpcGameOver(playerId);
        }

        if (gameOver1 != null)
        {
            gameOver1.ShowGameOverScreen(playerId);
        }
    }

    //stop player movement
    void StopMoving()
    {
        touchStartPos = Vector2.zero;
        touchEndPos = Vector2.zero;
        newPosition = transform.position; 
    }

    //stop meteor movement
    void StopMeteorMove()
    {
        MeteorMove[] meteorMoves = FindObjectsOfType<MeteorMove>();

        foreach (MeteorMove meteorMove in meteorMoves)
        {
            meteorMove.enabled = false; 
        }
    }


    [ClientRpc]
    void RpcGameOver(uint winnerId)
    {
        gameEnded = true;
        StopMoving();
        StopMeteorMove();
        Debug.Log("Oyun Bitti!");

        if (gameOver1 != null)
        {
            gameOver1.ShowGameOverScreen(winnerId);
        }
    }

    void ResetGame()
    {
        gameEnded = false; 
        StopMoving();
        StopMeteorMove();
    }
}
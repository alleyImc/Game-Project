using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Game2_moveShip : NetworkBehaviour
{
    public float baseSpeed = 0.6f;
    public float speedIncrement = 0.6f;
    private float currentSpeed = 0f;
    private float maxSpeed = 5f;
    private bool isMoving = false;
    private CountdownController countdownController;

    public GameOver2 gameOver2;

    private static bool gameEnded = false; // Oyunun biti� durumunu kontrol eden de�i�ken

    [SerializeField] public uint playerId; // Oyuncu ID'si

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game_2")
        {
            this.enabled = false; // Disable this script if not in Game_2
            return;
        }
        else
        {
            this.enabled = true;
        }

        // CountdownController'� bul
        countdownController = FindObjectOfType<CountdownController>();
        if (countdownController == null)
        {
            Debug.LogError("CountdownController not found in the scene.");
        }

        gameOver2 = FindObjectOfType<GameOver2>();
        if (gameOver2 == null)
        {
            Debug.LogError("GameOver2 not found in the scene.");
        }

        ResetGame(); // Oyunun ba�lang�c�nda h�z ve oyun biti� durumunu s�f�rla
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game_2")
        {
            return;
        }

        // Countdown bitene kadar bekle
        if (countdownController != null && countdownController.IsCountdownFinished() && !gameEnded)
        {
            // Ekran kontrol�
            if (Input.GetMouseButtonDown(0))
            {
                IncreaseSpeed();
            }

            // Hareket kontrol�
            if (isMoving)
            {
                float translation = currentSpeed * Time.deltaTime;
                transform.Translate(0, translation, 0);
            }
        }
    }

    void IncreaseSpeed()
    {
        if (countdownController.IsCountdownFinished())
        {
            isMoving = true;
            currentSpeed += speedIncrement;

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
    }

    void StopMoving()
    {
        isMoving = false;
        currentSpeed = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name != "Game_2")
        {
            return;
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            gameOver2 = FindAnyObjectByType<GameOver2>();
            GameOver();
        }
    }

    void GameOver()
    {
        if (SceneManager.GetActiveScene().name != "Game_2")
        {
            return;
        }

        Debug.Log("Oyun Bitti!");
        gameEnded = true; // Oyunun bitti�ini belirt

        // Sunucudan t�m oyunculara oyun bitti�ini bildir
        if (isServer)
        {
            RpcGameOver(playerId);
        }

        if (gameOver2 != null)
        {
            gameOver2.ShowGameOverScreen(playerId);
        }
    }

    [ClientRpc]
    void RpcGameOver(uint winnerId)
    {
        gameEnded = true;
        StopMoving();

        if (gameOver2 != null)
        {
            gameOver2.ShowGameOverScreen(winnerId);
        }
    }

    void ResetGame()
    {
        gameEnded = false; // Oyunun biti� durumunu s�f�rla
        StopMoving(); // H�z� ve hareketi s�f�rla
    }
}

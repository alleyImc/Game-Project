using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Game3_moveShip : NetworkBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private CountdownController countdownController;
    public GameOver3 gameOver3;

    private static bool gameEnded = false;

    [SerializeField] public uint playerId;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game_3")
        {
            this.enabled = false; // Disable this script if not in Game_3
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

        gameOver3 = FindObjectOfType<GameOver3>();
        if (gameOver3 == null)
        {
            Debug.LogError("GameOver3 not found in the scene.");
        }

        ResetGame();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game_3")
        {
            return;
        }

        if (!gameEnded)
        {
            ShipMovement();
        }
    }

    void ShipMovement()
    {
        // Handle mouse or touch input
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the spaceship
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            if (collider != null && collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - mousePosition;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // Move the spaceship with the mouse only on the horizontal axis
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float newYPosition = transform.position.y; // Keep the current Y position
            transform.position = new Vector3(mousePosition.x + offset.x, newYPosition, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name != "Game_3")
        {
            return;
        }

        if (collision.gameObject.CompareTag("laser"))
        {
            // Trigger game over
            gameOver3 = FindObjectOfType<GameOver3>();
            GameOver();
        }
    }

    void GameOver()
    {
        if (SceneManager.GetActiveScene().name != "Game_3")
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

        if (gameOver3 != null)
        {
            gameOver3.ShowGameOverScreen(playerId);
        }
    }

    [ClientRpc]
    void RpcGameOver(uint winnerId)
    {
        gameEnded = true;
        StopMove();

        Debug.Log("Oyun Bitti!");

        if (gameOver3 != null)
        {
            gameOver3.ShowGameOverScreen(winnerId);
        }
    }

    void ResetGame()
    {
        gameEnded = false;
        //stop move
    }

    void StopMove()
    {
        // Disable the spaceship movement script
        this.enabled = false;

        // Disable all alien movement scripts
        Game3AlienMove[] aliens = FindObjectsOfType<Game3AlienMove>();
        foreach (var alien in aliens)
        {
            //alien.StopAlienMovement();
        }

        // Disable all laser movements (you might need to add a script to manage lasers)
        // Example: Find all lasers and disable their Rigidbody2D components
        LaserAilen[] lasers = FindObjectsOfType<LaserAilen>();
        foreach (var laser in lasers)
        {
            Destroy(laser.gameObject);
        }
    }
}

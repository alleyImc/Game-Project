using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2_moveShip : MonoBehaviour
{
    public float baseSpeed = 0.6f;
    public float speedIncrement = 0.6f;
    private float currentSpeed = 0f;
    private float maxSpeed = 5f;
    private bool isMoving = false;

    void Update()
    {
        //screen control
        if (Input.GetMouseButtonDown(0))
        {
            IncreaseSpeed();
        }

        // movement control
        if (isMoving)
        {
            float translation = currentSpeed * Time.deltaTime;
            transform.Translate(0, translation, 0);
        }
    }

    void IncreaseSpeed()
    {
        isMoving = true;
        currentSpeed += speedIncrement;

        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    void StopMoving()
    {
        isMoving = false;
        currentSpeed = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Oyun Bitti!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("You Win!");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector3 newPosition;

    void Update()
    {
        HandleTouchInput();
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
        if (collision.gameObject.CompareTag("Meteor"))
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




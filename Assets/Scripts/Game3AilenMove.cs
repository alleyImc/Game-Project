using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Game3AlienMove : NetworkBehaviour
{
    public float speed = 2.0f; // Speed of movement
    public float minMoveTime = 0.5f; // Minimum time for moving in one direction
    public float maxMoveTime = 2.0f; // Maximum time for moving in one direction
    public float minX = -5.0f; // Minimum x position
    public float maxX = 5.0f; // Maximum x position

    public GameObject laserPrefab; // Reference to the laser prefab
    public float laserSpeed = 5.0f; // Speed of the laser
    public float fireRate = 1.0f; // Time interval between shots

    private float moveTime;
    private float moveTimer;
    private bool movingRight = true;
    private float fireTimer;

    private CountdownController countdownController;
    private static bool gameEnded = false;

    [SyncVar]
    private Vector3 syncPosition;

    [SyncVar]
    private bool syncMovingRight;

    private bool canMove = true;
    private bool canShoot = true;

    void Start()
    {
        // Find CountdownController
        countdownController = FindObjectOfType<CountdownController>();
        if (countdownController == null)
        {
            Debug.LogError("CountdownController not found in the scene.");
        }

        SetRandomMoveTime();
        fireTimer = fireRate;
    }

    void Update()
    {
        if (!isServer) return; // Only the server handles movement and shooting

        // Wait until countdown finishes
        if (countdownController != null && countdownController.IsCountdownFinished() && !gameEnded)
        {
            if (canMove)
            {
                MoveAlien();
                UpdateMoveTimer();
            }

            if (canShoot)
            {
                HandleShooting();
            }
        }
    }

    void FixedUpdate()
    {
        if (isServer)
        {
            syncPosition = transform.position;
            syncMovingRight = movingRight;
        }
        else
        {
            transform.position = syncPosition;
            movingRight = syncMovingRight;
        }
    }

    void SetRandomMoveTime()
    {
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        moveTimer = moveTime;
    }

    [Server]
    void MoveAlien()
    {
        if (!canMove) return;

        float direction = movingRight ? 1 : -1;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Boundary check
        if (transform.position.x <= minX && !movingRight)
        {
            movingRight = true;
            SetRandomMoveTime();
        }
        else if (transform.position.x >= maxX && movingRight)
        {
            movingRight = false;
            SetRandomMoveTime();
        }
    }

    [Server]
    void UpdateMoveTimer()
    {
        if (!canMove) return;

        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            movingRight = !movingRight;
            SetRandomMoveTime();
        }
    }

    [Server]
    void HandleShooting()
    {
        if (!canShoot) return;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            RpcShootLaser();
            fireTimer = fireRate;
        }
    }

    [ClientRpc]
    void RpcShootLaser()
    {
        if (!canShoot) return;

        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * laserSpeed;
        }
    }

    public void StopAlienMovement()
    {
        canMove = false;
    }

    public void StopAlienShooting()
    {
        canShoot = false;
    }
}

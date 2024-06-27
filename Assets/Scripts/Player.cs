using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cameraObject;

    [Header("Player Attributes")]
    [SerializeField] float movingSpeed = 3;

    float AxisXMaxBorder;
    float AxisXMinBorder;
    float AxisYMaxBorder;
    float AxisYMinBorder;
    float padding = 1f;

    private void Awake()
    {
        cameraObject = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetBorders();
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();   
    }

    private void MoveShip()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movingSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movingSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, AxisXMinBorder, AxisXMaxBorder);
        transform.position = new Vector2(newXPos, transform.position.y);

        var newYPos = Mathf.Clamp(transform.position.y + deltaY, AxisYMinBorder, AxisYMaxBorder);
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    private void SetBorders()
    {
        AxisXMaxBorder = cameraObject.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        AxisXMinBorder = cameraObject.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;

        AxisYMaxBorder = cameraObject.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        AxisYMinBorder = cameraObject.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float deadZone = -8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;

        if (transform.position.y < deadZone)
        {
            Debug.Log("meteor deleted");
            Destroy(gameObject);
        }
    }
}

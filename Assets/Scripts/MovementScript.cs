using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using System;

public class MovementScript : MonoBehaviour
{
    public float movementForce;
    private float up;
    private float right;
    public float drag;
    private Vector2 speed;
    public Vector2 direction;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = new Vector2(0.0f, 0.0f);
        direction = new Vector2(1, 1);

        if (drag > 1)
        {
            drag = 1;
        }
    }


    private void FixedUpdate()
    {
        up = drag * up;
        right = drag * right;

        if(Math.Abs(up) < 0.01)
        {
            up = 0;
        }
        if(Math.Abs(right) < 0.01)
        {
            right = 0;
        }
        

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction.y = up = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction.y = up = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction.x = right = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction.x = right = -1;
        }

        speed.x = movementForce * right;
        speed.y = movementForce * up;

        rb.velocity = speed;



    }
}
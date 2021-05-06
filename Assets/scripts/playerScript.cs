using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        doMovement();
    }


    void doMovement()
    {
        Vector2 currentVelocity = rb.velocity;
        float yVelocity = currentVelocity.y;


        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector2(1, yVelocity);
        }
        if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-1, yVelocity);
        }

    }

}

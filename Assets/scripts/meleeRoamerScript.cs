using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeRoamerScript : MonoBehaviour
{
    Rigidbody2D rb;
    float gravity = 0.8f;
    float moveSpeed = 2f;
    int damage = 1;
    public int health = 5;

    bool wasGrounded;
    bool isGrounded;
    float lastTurned;
    float turnCooldown = 0.4f;

    float currentTime;
    float deltaTime;

    Vector2 velocity;
    SpriteRenderer spriteRenderer;

    float height;
    float width;

    playerScript playerScript;

    float direction = -1f; //Defaults to moving left



    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        deltaTime = Time.fixedDeltaTime;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();


        lastTurned = currentTime;
        isGrounded = getGrounded();

        width = spriteRenderer.bounds.size.x / 2;
        height = spriteRenderer.bounds.size.y / 2;
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }


        currentTime = Time.time;
        deltaTime = Time.fixedDeltaTime;

        //wasGrounded tells us whether the enemy was grounded during the last physics update
        wasGrounded = isGrounded;
        isGrounded = getGrounded();

        //oldVelocity is the object's velocity during the last physics update. 
        //We want to keep y velocity across frames, but reset x
        Vector2 oldVelocity = rb.velocity;
        velocity = new Vector2(0, oldVelocity.y);


        //Have the enemy change direction when they reach an edge instead of falling
        if (!isGrounded && wasGrounded && (currentTime - lastTurned > turnCooldown))
        {
            direction = -direction;
            lastTurned = currentTime;
        }
        //Temporarily disables gravity so the enemy has time to get back onto ground.
        if (currentTime - lastTurned < turnCooldown)
        {
            velocity.y = 0;
        }
        else
        {
            doGravity(ref velocity, gravity);
        }

        doMovement(ref velocity);

        if (checkFront())
        {
            direction = -direction;
        }


        //Faces sprite in the direction it is moving. Also keeping it's rotation around the other axis locked
        transform.eulerAngles = new Vector3(0, direction * 90 + 90, 0);


        rb.velocity = velocity;
    }



    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerScript.receiveDamage(damage);
        }
    }

    public void receiveDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    bool checkFront()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction * new Vector2(1, 0), 1f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.collider != null)
            {
                if (hit.distance < 0.5 + width / 2 && hit.collider.tag == "platform")
                {
                    return true;
                }
            }
        }
        return false;
    }















    bool getGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, 1f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.collider != null)
            {
                if (hit.distance < 0.5 + height / 2 && hit.collider.tag == "platform")
                {
                    return true;
                }
            }
        }
        return false;
    }

    void doGravity(ref Vector2 velocity, float gravity)
    {
        velocity.y -= gravity;

        if (velocity.y < -20)
        {
            velocity.y = -20;
        }
    }

    void doMovement(ref Vector2 velocity)
    {
        velocity += new Vector2(direction, 0) * moveSpeed;
    }
}

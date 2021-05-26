using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;

    float lastJump;
    float jumpCooldown = 0.1f;
    float currentTime;
    float deltaTime;
    float gravity = 0.8f;
    float moveSpeed = 5f;
    float jumpVel = 18f;

    public int gunDamage = 1;

    public float direction = 1f;

    public int health = 10;

    float lastHurt;
    float hurtCooldown = 0.5f;

    Vector2 velocity;

    String chosenAnimation = "player_idle";

    Animator anim;
    SpriteRenderer spriteRenderer;
    int playerLayer;

    float height;
    float width;
    
    public Vector2 preMove;
    public Vector2 preJump;
    public Vector2 tester;
    public float floatTest;
    public bool boolTest;

    Transform emitter;
    public GameObject bulletPrefab;

    float shotCoolDown = 0.1f;
    float lastShot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        emitter = transform.GetChild(0).GetChild(0);

        width = spriteRenderer.bounds.size.x / 2;
        height = spriteRenderer.bounds.size.y / 2;

        playerLayer = 10;
        lastJump = Time.time;

        


    }

    void FixedUpdate()
    {
        chosenAnimation = "player_idle";

        currentTime = Time.time;
        deltaTime = Time.fixedDeltaTime;

        Vector2 oldVelocity = rb.velocity;
        velocity = new Vector2(0, oldVelocity.y);

        bool isGrounded = getGrounded();

        

        doGravity(ref velocity, gravity);
        doMovement(ref velocity);
        doJump(ref velocity, isGrounded);

        doShoot();

        float yAngle = (direction == -1) ? 180 : 0;

        transform.eulerAngles = new Vector3(0, yAngle, 0);
        rb.velocity = velocity;
        anim.Play(chosenAnimation);
    }

    void doShoot()
    {
        bool left = Input.GetKey("left");
        bool right = Input.GetKey("right");


        if (left || right)
        {
            chosenAnimation = "player_shooting"; // (chosenAnimation == "player_run") ? "player_shooting" : "player_stand";
            if (left) direction = -1f;
            if (right) direction = 1f;
            if (currentTime - lastShot > shotCoolDown)
            {
                lastShot = currentTime;
                GameObject bulletRef = GameObject.Instantiate(bulletPrefab);


                
                bulletRef.GetComponent<playerBulletScript>().init(emitter.position, new Vector2(direction, 0));
            }

        }
    }

    public void receiveDamage(int damage, bool force = false)
    {
        if (currentTime - lastHurt > hurtCooldown || force)
        {
            lastHurt = currentTime;
            health -= damage;
        }
    }



    bool getGrounded()
    {
        int layerMask = ~(1 << playerLayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, layerMask);
        if (hit.collider != null)
        {
            
            tester = new Vector2(hit.distance, 0.5f + height / 2);
            if (hit.distance < 0.5 + height / 2 && hit.collider.tag == "platform")
            {
                return true;
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
        if (Input.GetKey("d"))
        {
            velocity += new Vector2(1, 0) * moveSpeed;
            direction = 1f;
            chosenAnimation = "player_run";
        }
        if (Input.GetKey("a"))
        {
            velocity += new Vector2(-1, 0) * moveSpeed;
            direction = -1f;
            chosenAnimation = "player_run";
        }

    }

    void doJump(ref Vector2 velocity, bool isGrounded)
    {
        floatTest = currentTime - lastJump;
        if (Input.GetKey("space") && currentTime - lastJump > jumpCooldown && isGrounded)
        {
            lastJump = currentTime;
            velocity += new Vector2(0, 1) * jumpVel;
        }

    }

}

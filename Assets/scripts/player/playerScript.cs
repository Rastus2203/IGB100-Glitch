using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;

    sceneManager sceneMan;

    float lastJump;
    float jumpCooldown = 0.4f;
    public bool doubleJumpUnlocked = false;
    public float lastDoubleJump;
    public float doubleJumpCoolDown = 1f;
    float doubleJumpScalar = 0.7f;

    float currentTime;
    float deltaTime;

    float gravity = 0.8f;
    float moveSpeed = 5f;
    float jumpVel = 18f;
    public float direction = 1f;
    Vector2 velocity;
    public int health = 10;

    public int gunDamage = 1;
    public int grenadeDamage = 10;

    float lastHurt;
    float hurtCooldown = 0.5f;

    String chosenAnimation = "player_idle";

    Animator anim;
    SpriteRenderer spriteRenderer;
    int playerLayer;

    float height;
    float width;
    

    Transform emitter;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;

    public float shotCoolDown = 0.1f;
    [HideInInspector] public float lastShot = 0;

    public bool grenadeUnlocked = false;
    public float grenadeCoolDown = 5f;
    [HideInInspector] public float lastGrenade;

    public AudioSource shootSound;
    public AudioSource hurtSound;
    public AudioSource grenadeSound;

    void Start()
    {

        sceneMan = GameObject.FindWithTag("sceneManager").GetComponent<sceneManager>();
        lastGrenade = -grenadeCoolDown;
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
        if (health <= 0)
        {
            sceneMan.diedScene();
        }


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
        doGrenade();

        float yAngle = (direction == -1) ? 180 : 0;

        transform.eulerAngles = new Vector3(0, yAngle, 0);
        rb.velocity = velocity;
        anim.Play(chosenAnimation);
    }


    void doGrenade()
    {
        if (grenadeUnlocked)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentTime - lastGrenade > grenadeCoolDown)
                {
                    lastGrenade = currentTime;

                    GameObject grenadeRef = GameObject.Instantiate(grenadePrefab);

                    grenadeRef.GetComponent<grenadeScript>().init(emitter.position, new Vector2(direction, 0));

                }
            }
        }
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
                shootSound.Play();

                
                bulletRef.GetComponent<playerBulletScript>().init(emitter.position, new Vector2(direction, 0));
            }

        }
    }


    public void receiveDamage(int damage, bool force = false)
    {
        if (currentTime - lastHurt > hurtCooldown || force)
        {
            lastHurt = currentTime;
            hurtSound.Play();
            health -= damage;
        }
    }


    bool getGrounded()
    {
        int layerMask = ~(1 << playerLayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, layerMask);
        if (hit.collider != null)
        {
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
        if ((Input.GetKey("space") || Input.GetKey("w")) && currentTime - lastJump > jumpCooldown)
        {
            if (isGrounded)
            {
                lastJump = currentTime;
                velocity += new Vector2(0, 1) * jumpVel;
            } else if ((currentTime - lastDoubleJump > doubleJumpCoolDown) && doubleJumpUnlocked)
            {
                lastDoubleJump = currentTime;
                lastJump = currentTime;
                velocity.y = 0;
                velocity += new Vector2(0, 1) * jumpVel * doubleJumpScalar;
            }
        }
    }
}

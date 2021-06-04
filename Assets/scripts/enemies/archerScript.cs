using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerScript : MonoBehaviour
{
    Rigidbody2D rb;
    float gravity = 0.8f;
    public float moveSpeed = 2f;
    int damage = 1;

    public int health = 3;

    float range = Mathf.Pow(7f, 2);

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

    float lastShot;
    float shotCoolDown = 1f;

    Transform emitter;
    public GameObject bulletPrefab;

    GameObject player;
    playerScript playerScript;

    float direction = -1f; //Defaults to moving left

    Animator anim;
    string chosenAnimation = "Archer1_Idle";


    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        deltaTime = Time.fixedDeltaTime;

        emitter = transform.GetChild(0);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<playerScript>();


        lastTurned = currentTime;
        isGrounded = getGrounded();

        width = spriteRenderer.bounds.size.x / 2;
        height = spriteRenderer.bounds.size.y / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        chosenAnimation = "Archer1_Idle";


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

        //Debug.DrawLine(transform.position, (transform.position - new Vector3((player.transform.position - transform.position), 0, 0));


        if ((player.transform.position - transform.position).sqrMagnitude < range)
        {
            doShoot();
            if (player.transform.position.x < transform.position.x)
            {
                direction = -1f;
            } else
            {
                direction = 1f;
            }
        } else
        {
            chosenAnimation = "Archer1_Walk 0";
            doMovement(ref velocity);
        }


        

        if (checkFront())
        {
            direction = -direction;
        }



        //Faces sprite in the direction it is moving. Also keeping it's rotation around the other axis locked
        transform.eulerAngles = new Vector3(0, (direction * 90 - 90), 0);


        anim.Play(chosenAnimation);
        rb.velocity = velocity;
    }



    void doShoot()
    {

        if (currentTime - lastShot > shotCoolDown)
        {

            lastShot = currentTime;
            GameObject bulletRef = GameObject.Instantiate(bulletPrefab);



            bulletRef.GetComponent<enemyBulletScript>().init(emitter.position, new Vector2(direction, 0), damage);
        }
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

    }

    bool checkFront()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.3f, 0), direction * new Vector2(1, 0), width);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.collider != null)
            {
                if (hit.distance < 0.5 + width / 2 && hit.distance > width / 2 + 0.1f && (hit.collider.tag == "platform" || hit.collider.tag == "enemy"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool getGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, height + 0.1f);
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

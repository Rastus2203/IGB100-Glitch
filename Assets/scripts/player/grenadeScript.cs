using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    Vector2 velocity = new Vector3(0, 0);
    public float initialYVelocity = 15f;
    float speedScalar = 15f;
    int damage;
    float gravity = 0.8f;

    Rigidbody2D rb;

    playerScript playerScript;


    float currentTime;
    float spawnTime;
    float rotationScalar = 500f;


    public GameObject grenadeExplosion;
    AudioSource grenadeSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentTime = Time.time;
        spawnTime = currentTime;

        rb.freezeRotation = true;

        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        damage = playerScript.grenadeDamage;
        grenadeSound = playerScript.grenadeSound;
    }

    void FixedUpdate()
    {
        currentTime = Time.time;
        doGravity(ref velocity, gravity);

        Vector3 velocity3 = new Vector3(velocity.x * speedScalar, velocity.y, 0);


        float newRotation = (currentTime - spawnTime) * rotationScalar;
        

        rb.velocity = velocity3;
        rb.rotation = newRotation;
    }

    void doGravity(ref Vector2 velocity, float gravity)
    {
        velocity.y -= gravity;

        if (velocity.y < -20)
        {
            velocity.y = -20;
        }
    }

    void onHit(Collider2D other)
    {
        if (other.tag == "platform" || other.tag == "enemy")
        {
            GameObject explosionRef = Instantiate(grenadeExplosion);
            grenadeSound.Play();
            explosionRef.GetComponent<grenadeExplosionScript>().init(transform.position);

            
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        onHit(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //onHit(other);
    }



    public void init(Vector3 position, Vector2 inVelocity)
    {
        transform.position = position;
        velocity = new Vector2(inVelocity.x, initialYVelocity);
    }
}

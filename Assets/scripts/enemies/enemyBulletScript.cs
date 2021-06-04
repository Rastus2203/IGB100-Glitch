using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletScript : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0, 0, 0);
    float speedScalar = 0.3f;
    float gravity = 0.01f;
    int damage;

    float initialYSpeed = 0.1f;
    public float test;

    float startTime;

    playerScript playerScript;

    float hitTimer = 0f;
    float hitDuration = 0.1f;

    float lastX;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        
        doGravity();

        transform.position += velocity;

        if (hitTimer > 0)
        {
            if (Time.time - hitTimer > hitDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    void doGravity()
    {
        velocity.y -= gravity;
    }

    void onHit(Collider2D other)
    {
        if ((other.tag == "Player" || other.tag == "platform") && other.tag != "enemyBullet")
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<playerScript>().receiveDamage(damage);
                //Debug.Log(startTime - Time.time);
            }

            Destroy(gameObject);

        }


    }

    void OnTriggerStay2D(Collider2D other)
    {
        onHit(other);
    }

    public void init(Vector3 startPosition, Vector2 startDirection, int bulletDamage)
    {
        velocity = new Vector3(startDirection.x * speedScalar, initialYSpeed, 0);

        
        transform.position = startPosition;
        damage = bulletDamage;
    }

}

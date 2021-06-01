using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletScript : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    float speedScalar = 0.2f;
    int damage;

    playerScript playerScript;

    float hitTimer = 0f;
    float hitDuration = 0.1f;



    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        damage = playerScript.gunDamage;
    }


    void onHit(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "playerBullet")
        {
            other.gameObject.SendMessage("receiveDamage", damage, SendMessageOptions.DontRequireReceiver);
            //hitTimer = Time.time;
            Destroy(gameObject);

        }


    }

    void OnTriggerStay2D(Collider2D other)
    {
        onHit(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //onHit(other);
    }

    void FixedUpdate()
    {
        transform.position += velocity * speedScalar;

        if (hitTimer > 0)
        {
            if (Time.time - hitTimer > hitDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    public void init(Vector3 position, Vector2 inVelocity)
    {
        transform.position = position;
        velocity = inVelocity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletScript : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    float speedScalar = 0.2f;
    int damage;

    playerScript playerScript;



    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        damage = playerScript.gunDamage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "playerBullet")
        {
            other.gameObject.SendMessage("receiveDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position += velocity * speedScalar;
    }

    public void init(Vector3 position, Vector2 inVelocity)
    {
        transform.position = position;
        velocity = inVelocity;
    }
}

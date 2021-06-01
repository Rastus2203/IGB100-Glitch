using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldHazardScript : MonoBehaviour
{
    int damage = 1;
    playerScript playerScript;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerScript.receiveDamage(damage);
        }
    }
}

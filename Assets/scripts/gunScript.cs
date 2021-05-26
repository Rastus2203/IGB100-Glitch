using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    /*
    playerScript playerScript;
    float direction;

    public GameObject bulletPrefab;

    float shotCoolDown = 0.1f;
    float lastShot;
    float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        emitter = transform.GetChild(0);
        playerScript = transform.parent.GetComponent<playerScript>();

        currentTime = Time.time;
        lastShot = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        bool left = Input.GetKey("left");
        bool right = Input.GetKey("right");

        
        if (left || right)
        {
            if (currentTime - lastShot > shotCoolDown)
            {
                lastShot = currentTime;
                GameObject bulletRef = GameObject.Instantiate(bulletPrefab);
                if (left) direction = -1f;
                if (right) direction = 1f;


                bulletRef.GetComponent<playerBulletScript>().init(emitter.position, new Vector2(direction, 0));
            }
                
        }

    }
    */
}

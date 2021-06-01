using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeUnlockScript : MonoBehaviour
{
    bool alreadyUnlocked = false;
    playerScript player;
    grenadeIconScript grenadeIcon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        grenadeIcon = GameObject.FindWithTag("UICanvas").transform.GetChild(2).GetComponent<grenadeIconScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!alreadyUnlocked)
        {
            if (other.tag == "Player")
            {
                alreadyUnlocked = true;
                player.grenadeUnlocked = true;
                grenadeIcon.enableIcon();
                Destroy(transform.GetChild(1).gameObject);
            }
        }

    }

}

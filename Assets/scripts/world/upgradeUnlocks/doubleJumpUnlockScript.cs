using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpUnlockScript : MonoBehaviour
{
    bool alreadyUnlocked = false;
    playerScript player;
    doubleJumpIconScript doubleJumpIcon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        doubleJumpIcon = GameObject.FindWithTag("UICanvas").transform.GetChild(3).GetComponent<doubleJumpIconScript>();
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
                player.doubleJumpUnlocked = true;
                doubleJumpIcon.enableIcon();
                Destroy(transform.GetChild(1).gameObject);
            }
        }

    }
}

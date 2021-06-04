using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour
{
    int childCount = 10;
    playerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();

        for (int i=0; i < childCount; i++)
        {
            Transform halfHeart = transform.GetChild(i);
            halfHeart.GetComponent<Image>().color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int heartCount = playerScript.health;
        for (int i=0; i < childCount; i++)
        {
            Transform halfHeart = transform.GetChild(i);
            if (i < heartCount)
            {
                halfHeart.GetComponent<Image>().enabled = true;
            } else
            {
                halfHeart.GetComponent<Image>().enabled = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grenadeIconScript : MonoBehaviour
{
    playerScript player;
    float grenadeCoolDown;
    float lastGrenade;

    public float colourModifier = 1f;

    public float floatTest;
    public float timeTest;

    float maxGreyPercent = 0.6f;


    Image image;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        image = GetComponent<Image>();
        grenadeCoolDown = player.grenadeCoolDown + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        float colourBase = 0;
        lastGrenade = player.lastGrenade;
        floatTest = lastGrenade;
        timeTest = currentTime;

        if (currentTime - lastGrenade < grenadeCoolDown)
        {
            colourBase = ((currentTime - lastGrenade) / grenadeCoolDown) * maxGreyPercent;
        } else
        {
            colourBase = 1f;
        }







        float newColour = colourBase; //(1 - colourModifier) + colourBase * colourModifier;

        image.color = new Color(newColour, newColour, newColour, 1f);
    }
}

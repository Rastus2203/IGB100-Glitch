using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunIconScript : MonoBehaviour
{
    playerScript player;
    float shotCoolDown;
    float lastShot;

    float fadeStartTime;
    bool isFading = false;

    public float colourModifier = 1f;
    public float fadeTime = 2f;
    public float shootingColour = 0.5f;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        image = GetComponent<Image>();
        shotCoolDown = player.shotCoolDown + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        float colourBase = 0;
        lastShot = player.lastShot;

        if (currentTime - lastShot > shotCoolDown && !isFading)
        {
            isFading = true;
            fadeStartTime = currentTime;
        }

        if (currentTime - lastShot < shotCoolDown)
        {
            isFading = false;
            colourBase = shootingColour;
        }

        if (isFading)
        {
            if (currentTime - lastShot < fadeTime)
            {
                colourBase = (currentTime - lastShot) / fadeTime;
            } else
            {
                colourBase = 1f;
            }
        }

        float newColour = (1 - colourModifier) + colourBase * colourModifier;

        image.color = new Color(newColour, newColour, newColour, 1f);
        
    }
}

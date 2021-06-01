using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doubleJumpIconScript : MonoBehaviour
{
    playerScript player;
    float doubleJumpCoolDown;
    float lastDoubleJump;

    public float colourModifier = 1f;
    float maxGreyPercent = 0.6f;

    Image image;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        image = GetComponent<Image>();
        doubleJumpCoolDown = player.doubleJumpCoolDown + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        float colourBase = 0;
        lastDoubleJump = player.lastDoubleJump;

        if (currentTime - lastDoubleJump < doubleJumpCoolDown)
        {
            colourBase = ((currentTime - lastDoubleJump) / doubleJumpCoolDown) * maxGreyPercent;
        }
        else
        {
            colourBase = 1f;
        }

        float newColour = colourBase;

        image.color = new Color(newColour, newColour, newColour, 1f);
    }

    public void enableIcon()
    {
        GetComponent<Image>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeExplosionScript : MonoBehaviour
{
    float startScale = 0.1f;
    float endScale = 0.8f;
    float scaleDiff;
    float startTime;
    float duration = 0.2f;
    float currentTime;

    float damage;

    playerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        startTime = currentTime;
        scaleDiff = endScale - startScale;
        transform.localScale = new Vector3(startScale, startScale, 0);


        playerScript = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        damage = playerScript.grenadeDamage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime = Time.time;
        float scalePercent = (currentTime - startTime) / duration;
        float newScale = startScale + scaleDiff * scalePercent;

        if (newScale > endScale)
        {
            Destroy(gameObject);
        }

        transform.localScale = new Vector3(newScale, newScale, 0);

    }

    void onHit(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "playerBullet")
        {
            other.gameObject.SendMessage("receiveDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        onHit(other);
    }


    public void init(Vector3 position)
    {
        transform.position = position;
    }
}

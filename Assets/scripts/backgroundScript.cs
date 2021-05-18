using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    //GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //camera = GameObject.FindWithTag("MainCamera");
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        
        transform.localScale = new Vector3(1, 1, 1);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        float width = sprite.sprite.bounds.size.x;
        float height = sprite.sprite.bounds.size.y;


        Vector3 newScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
        transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedastalAnimationScript : MonoBehaviour
{
    float currentTime;
    float timeScalar = 3f;
    float heightScalar = 0.4f;
    Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;


        float newHeight = Mathf.Sin(currentTime * timeScalar) * heightScalar;


        transform.position = new Vector3(startPosition.x, newHeight + startPosition.y, startPosition.z);


    }
}

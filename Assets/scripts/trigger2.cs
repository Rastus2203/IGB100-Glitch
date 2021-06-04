using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2 : MonoBehaviour
{
    bool triggered = false;

    public GameObject archRef;
    public GameObject meleeRef;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("WOWO");
        if (other.tag == "Player" && !triggered)
        {
            triggered = true;

            Vector3[] meleeCoords = {
                new Vector3(113.94f, 9.001f, 0),
                new Vector3(120.94f, 16.8f, 0),
                new Vector3(127.52f, 10.691f, 0),
                new Vector3(134.159f,19.076f,0),
                new Vector3(150.214462f,29.2165718f,0),
                new Vector3(158.85939f,31.231554f,0),
                new Vector3(152.554443f,36.6265106f,0),
                new Vector3(163.460678f,38.9363518f,0),
                new Vector3(159.184387f,26.4865932f,0),
                new Vector3(148.199524f,24.8616142f,0),
            };

            for (int i = 0; i < meleeCoords.Length; i++)
            {
                Instantiate(meleeRef, meleeCoords[i], Quaternion.identity);
            }


            Vector3[] rangedCoords = {
                new Vector3(108.93985f,6.98677015f,0),
                new Vector3(119.404762f,12.5117245f,0),
                new Vector3(123.760002f,6.65999985f,0),
                new Vector3(147.159485f,29.9965649f,0),
            };

            for (int i = 0; i < rangedCoords.Length; i++)
            {
                Instantiate(archRef, rangedCoords[i], Quaternion.identity);
            }

        }
    }

}

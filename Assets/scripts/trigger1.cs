using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger1 : MonoBehaviour
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

            Instantiate(archRef, new Vector3(64.73f, 3.6f, 0), Quaternion.identity);
            Instantiate(archRef, new Vector3(72.771f, 5.92f, 0), Quaternion.identity);
            Instantiate(archRef, new Vector3(75.95f, 3.14f, 0), Quaternion.identity);

            Instantiate(meleeRef, new Vector3(58.54f, 0.82f, 0), Quaternion.identity);
            Instantiate(meleeRef, new Vector3(58.54f, 0.82f, 0), Quaternion.identity);
            Instantiate(meleeRef, new Vector3(58.54f, 0.82f, 0), Quaternion.identity);
            Instantiate(meleeRef, new Vector3(58.54f, 0.82f, 0), Quaternion.identity);

            Instantiate(meleeRef, new Vector3(66.09f, 3.8f, 0), Quaternion.identity);
            Instantiate(meleeRef, new Vector3(71.254f, 6.28f, 0), Quaternion.identity);
            Instantiate(meleeRef, new Vector3(78.94f, 2.89f, 0), Quaternion.identity);


        }
    }



}

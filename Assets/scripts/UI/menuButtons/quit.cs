using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour
{
    sceneManager sceneMan;

    // Start is called before the first frame update
    void Start()
    {
        sceneMan = GameObject.FindWithTag("sceneManager").GetComponent<sceneManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onButton()
    {
        sceneMan.quitGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void gameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void helpScene()
    {
        SceneManager.LoadScene("help");
    }


    
    public void menuScene()
    {
        SceneManager.LoadScene("mainMenu");
    }



    public void diedScene()
    {
        SceneManager.LoadScene("death");
    }

    public void winScene()
    {
        SceneManager.LoadScene("win");
    }

    public void quitGame()
    {
        Application.Quit();
    }
    
}

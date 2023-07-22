using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StateButton : MonoBehaviour
{

    public void switchToMainGame()
    {
       
        SceneManager.LoadScene("Beta_Game");

        ChainVars.onTitle = false;

        ChainVars.isPaused = false;

        Time.timeScale = 1;
    }

    public void closeGame()
    {
        Application.Quit();
    }

}

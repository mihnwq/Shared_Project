using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class States : MonoBehaviour
{

    public Canvas optionCanvas ;

    public GameObject invCanvas;


    public SoundHandler sfx;

    public enum GameStates
    {
        PAUSED,
        ON_GAME,
        TITLE_SCREEN
    }

    public GameStates states = GameStates.TITLE_SCREEN;


    public void Start()
    {

        optionCanvas.gameObject.SetActive(!optionCanvas.gameObject.activeSelf);

   

     
    }

   

    void Update()
    {
        checkCurrentState();
    }

    public void checkCurrentState()
    {
        
        if(!ChainVars.onTitle && states == GameStates.TITLE_SCREEN)
        {
            states = GameStates.ON_GAME;
        }

        if (Input.GetKeyDown(optionsValues.pauseKey))
        {
            switch (states)
            {
                case GameStates.ON_GAME:

                    states = GameStates.PAUSED;

                    ChainVars.onInventory = false;

                    invCanvas.gameObject.SetActive(false);

                    switchGameState(true);



                    optionCanvas.gameObject.SetActive(!optionCanvas.gameObject.activeSelf);

                    break;

                case GameStates.PAUSED:

                    states = GameStates.ON_GAME;

                    switchGameState(false);

               

                    optionCanvas.gameObject.SetActive(!optionCanvas.gameObject.activeSelf);

                    break;
            }
        }

        ChainVars.isPaused = states == GameStates.PAUSED;

        sfx.soundUpdate();
    }

    public void switchGameState(bool isPaused)
    {
          Time.timeScale = isPaused ? 0 : 1;

        
    }

    public bool onGame()
    {
        return states == GameStates.ON_GAME;
    }

    public bool titleScreen()
    {
        return states == GameStates.TITLE_SCREEN;
    }

    public bool pauseScreen()
    {
        return states == GameStates.PAUSED;
    }

    public void setOnGame()
    {
        states = GameStates.ON_GAME;
    }

}

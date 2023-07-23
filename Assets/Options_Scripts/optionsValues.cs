using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsValues : MonoBehaviour
{
    public static float mouseSensitivity { get; set; }

    //keys for the player
    public static KeyCode forwardKey;

    public static KeyCode backwardsKey;

    public static KeyCode leftKey;

    public static KeyCode rightKey;

    public static KeyCode dashKey;

    public static KeyCode crouchKey;

    public static KeyCode slideKey;

    public static KeyCode pauseKey;

    public static KeyCode runKey;

    public static KeyCode jumpKey;

    public static KeyCode rangedAttacKey;

    public static KeyCode meleAttackKey;

    public void Awake()
    {
        //   getButtons();

        ChainVars.isPaused = true;
    }

    public void getButtons()
    {
        foreach (var button in GetKeybind.instance.buttons)
        {
            switch (button.ID)
            {
                case 0:
                    forwardKey = button.key;
                    break;
                case 1:
                    backwardsKey = button.key;
                    break;
                case 2:
                    leftKey = button.key;
                    break;
                case 3:
                    rightKey = button.key;
                    break;
                case 4:
                    dashKey = button.key;
                    break;
                case 5:
                    crouchKey = button.key;
                    break;
                case 6:
                    slideKey = button.key;
                    break;
                case 7:
                    pauseKey = button.key;
                    break;
                case 8:
                    runKey = button.key;
                    break;
                case 9:
                    jumpKey = button.key;
                    break;
                case 10:
                    rangedAttacKey = button.key;
                    break;
                case 11:
                    meleAttackKey = button.key;
                    break;

            }
        }
    }

    public void Start()
    {
        getButtons();

        
    }

    public void Update()
    {
        if(ChainVars.isPaused)
        {
            getButtons();
        }
    }

}

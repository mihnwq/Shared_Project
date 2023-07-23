using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CustomInputManager : MonoBehaviour
{

    public KeyCode horizontalPositiveKey = KeyCode.D;
    public KeyCode horizontalNegativeKey = KeyCode.A;

    public KeyCode verticalPositiveKey = KeyCode.W;
    public KeyCode verticalNegativeKey = KeyCode.S;

    private static float horizontalAxisValue = 0f;
    private static float verticalAxisValue = 0f;

    public void Start()
    {
        getWalkingButtons();
    }

    public void getWalkingButtons()
    {
        horizontalPositiveKey = optionsValues.rightKey;
        horizontalNegativeKey = optionsValues.leftKey;

        verticalPositiveKey = optionsValues.forwardKey;
        verticalNegativeKey = optionsValues.backwardsKey;
    }

    private void Update()
    {
        
        if (Input.GetKey(horizontalPositiveKey))
        {
            horizontalAxisValue = 1f;
        }
        else if (Input.GetKey(horizontalNegativeKey))
        {
            horizontalAxisValue = -1f;
        }
        else
        {
            horizontalAxisValue = 0f;
        }

       
        if (Input.GetKey(verticalPositiveKey))
        {
            verticalAxisValue = 1f;
        }
        else if (Input.GetKey(verticalNegativeKey))
        {
            verticalAxisValue = -1f;
        }
        else
        {
            verticalAxisValue = 0f;
        }

        if(ChainVars.isPaused)
        {
            getWalkingButtons();
        }
        
    }

    public static float GetAxisRaw(string axisName)
    {
        if (axisName == "Horizontal")
        {
            return horizontalAxisValue;
        }
        else if (axisName == "Vertical")
        {
            return verticalAxisValue;
        }

        return 0f;
    }



}


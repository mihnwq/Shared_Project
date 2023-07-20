using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class GetKeybind : MonoBehaviour
{
    public List<ButtonManager> buttons = new List<ButtonManager>();

    List<KeyCode> keyCodes = new List<KeyCode>();

    public static GetKeybind instance;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {


        foreach (var button in buttons)
        {

            button.active = false;
            button.currentButtonKeybind.gameObject.SetActive(true);
            button.keybindChooseText.gameObject.SetActive(false);

            keyCodes.Add(button.key);
        }
    }

   public bool isAlreadyOnAButton;

    ButtonManager buttonToManage;

    

    public void Update()
    {
        if(!isAlreadyOnAButton)
        {
            foreach (var button in buttons)
            {
                if (button.active)
                {
                    isAlreadyOnAButton = true;
                    button.currentButtonKeybind.gameObject.SetActive(false);
                    button.keybindChooseText.gameObject.SetActive(true);
                    buttonToManage = button;
                 
                }
            }
        }
        else
        {
            if(Input.anyKeyDown)
            {

                KeyCode latestKeyCode = KeyCode.None;
                
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                
                    if(Input.GetKeyDown(keyCode))
                    {
                        latestKeyCode = keyCode;
                    }

                }

            if (!keyCodes.Contains(latestKeyCode))
            {
               

                int index = keyCodes.IndexOf(latestKeyCode);

                if (index != -1)
                {
                    keyCodes[index] = latestKeyCode;

                }
                else
                {


                    keyCodes.Add(latestKeyCode);

                }

                if (Input.GetKeyDown(latestKeyCode))
                {
                    buttonToManage.key = latestKeyCode;
                    buttonToManage.currentButtonKeybind.text = latestKeyCode.ToString();
                }
            }

                isAlreadyOnAButton = false;

                buttonToManage.active = false;

                buttonToManage.currentButtonKeybind.gameObject.SetActive(true);
                buttonToManage.keybindChooseText.gameObject.SetActive(false);

            }
        }
       


    }

    
}

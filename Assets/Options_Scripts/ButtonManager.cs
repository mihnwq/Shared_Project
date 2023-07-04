using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public Button button;

    public Label buttonLabel;

    public int ID;

    public Text currentButtonKeybind;

    public Text keybindChooseText;

    public bool active;

    public KeyCode key;

    public void Click()
    {       
        active = true;
    }

}


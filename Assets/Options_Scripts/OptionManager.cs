using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OptionManager : MonoBehaviour
{

    public GetKeybind gk;

    public OptionScreenButtons om;

    public void Update()
    {
        if(ChainVars.isPaused)
        {
            if (!gk.gameObject.activeSelf)
                om.optionUpdate();
            else { gk.controlUpdate();  }

        }
        else
        {
            gk.gameObject.SetActive(false);
            om.gameObject.SetActive(false);
        }
    }


}


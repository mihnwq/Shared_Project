﻿using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButtonsManager : MonoBehaviour
{

    public int ID;

    public void Start()
    {
        SaveSystem.initializePaths();    
    }

   public void Click()
    {
        ChainVars.saveID = ID;

        
    }

}

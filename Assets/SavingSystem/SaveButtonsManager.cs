using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class SaveButtonsManager : MonoBehaviour
{

    public int ID;

    public void Start()
    {
        SaveSystem.initializePaths();    
    }

   public void ClickSave()
    {
        //   Player.instance.savePlayer();

        //  SaveSystem.Save();

        SaveSystem.save(ChainVars.saveID);
    }

    public void ClickLoad()
    {
        //ChainVars.saveID = ID;

        //   Player.instance.loadPlayer();


        if (Directory.Exists(SaveSystem.paths[ID]))
        {
            ChainVars.wantToPlayTutorial = true;
            SaveSystem.load(ChainVars.saveID);
        }
        else ChainVars.wantToPlayTutorial = false;

        SceneManager.LoadScene("Beta_Game");
    }
}


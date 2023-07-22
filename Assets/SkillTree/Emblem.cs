using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Emblem : MonoBehaviour
{

    public GameObject tree;

    public float minDistance;

    public float time;

    private bool timeEnded = true;

    public void Update()
    {
      if((transform.position - Player.instance.transform.position).magnitude < minDistance)
        {
            if (Input.GetKeyDown(KeyCode.Q) && timeEnded)
            {
                timeEnded = false;
                ChainVars.onSkillTree = !ChainVars.onSkillTree;
                if (ChainVars.onSkillTree)
                    Invoke(nameof(switchOnSkillTree), time);
                else { switchOnSkillTree(); timeEnded = true; }
            }
        }
           
    }

  

    public void switchOnSkillTree()
    {
        tree.SetActive(ChainVars.onSkillTree);

        Time.timeScale = ChainVars.onSkillTree ? 0 : 1;

        timeEnded = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCore : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    public GameObject[] spawner;
    public float waitTimer = 2f;

    private void Update()
    {
        if (popUpIndex < 12)
        {

            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[i].SetActive(true);
                }
                else
                {
                    popUps[i].SetActive(false);
                }
            }

            switch (popUpIndex)
            {
                case 0:

                    if (Player.instance.isPlayerMoving())
                        popUpIndex = 1;
                    break;
                case 1:
                    if (!Player.instance.grounded)
                        popUpIndex = 2;
                    break;
                case 2:
                    if (Player.instance.isCrrouching())
                        popUpIndex = 3;
                    break;
                case 3:
                    if (Player.instance.isRuunning())
                        popUpIndex = 4;
                    break;
                case 4:
                    if (Player.instance.sd.sliding)
                        popUpIndex = 5;
                    break;
                case 5:
                    if (Player.instance.dashing)
                        popUpIndex = 6;
                    break;
                case 6:
                    if (Player.instance.isAttacking)
                        popUpIndex = 7;
                    break;
                case 7:
                    if (ChainVars.onInventory)
                        popUpIndex = 8;
                    break;
                case 8:
                    if (Player.instance.pt.throwing)
                        popUpIndex = 9;
                        break;
                case 9:
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        Invoke(nameof(stoppedStunnin), 2f);
                    }
                        
                    break;
                case 10:
                    if (!spawner[0].gameObject.activeSelf)
                        popUpIndex = 11;
                    break;
                case 11:
                    if (!InventoryManager.instance.canUsePostion)
                        popUpIndex = 12;
                    break;
            }

            if (popUpIndex == 10)
            {
                if (oneMonster)
                {
                    oneMonster = false;
                    spawner[0].SetActive(true);
                }
            }
           

            if(popUpIndex == 7)
            {
                if (oneKnife)
                {
                    oneKnife = false;
                    spawner[1].SetActive(true);
                }
            }
           
            if(popUpIndex == 11)
            {
                if(onePotion)
                {
                    onePotion = false;
                    spawner[2].SetActive(true);
                }
            }

            if(popUpIndex == 12)
            {
                spawner[3].SetActive(false);
            }

        }
    }

    public void stoppedStunnin()
    {
        popUpIndex = 10;
    }

    public bool oneKnife = true;

    public bool oneMonster = true;

    public bool onePotion = true;
}

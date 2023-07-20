using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraMainBrain :MonoBehaviour
{

    CameraLockOn clo;

    MoveAroundPlayer map;

    public static CameraMainBrain instance;

   public Player pl;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        clo = GetComponent<CameraLockOn>();

        map = GetComponent<MoveAroundPlayer>();
    }

    public bool lockOn = false;

    public void checkLockOn()
    {
        lockOn = Input.GetKey(KeyCode.E);
    }

   

    public void Update()
    {


        if (!ChainVars.onInventory && !ChainVars.onTrade && !pl.hasDied)
        {

            checkLockOn();

            if (!lockOn)
            {
                map.updateNormalView();
            }
            else if (lockOn)
            {
                clo.lockOnUpdatepdate();
            }

            map.zoom();
        }
        else
        {
            map.getToPlayerPosition();
        }
    }



}

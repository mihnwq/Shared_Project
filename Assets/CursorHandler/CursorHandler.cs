using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    
    void Start()
    {
        Cursor.visible = true;
    }



    public bool checkLock()
    {
        return ChainVars.isPaused || ChainVars.onTrade || ChainVars.onInventory; 
    }
    
    void Update()
    {

        if(checkLock())
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    
    }
}

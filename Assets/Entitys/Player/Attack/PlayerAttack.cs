using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttack : EntityAttack
{

    public void Start()
    {
      
    }

    public override void Update()
    {
        if(Input.GetMouseButton(0) && !InventoryManager.instance.isOpen)
        {
            attack();
        }

      

        base.Update();
    }

}


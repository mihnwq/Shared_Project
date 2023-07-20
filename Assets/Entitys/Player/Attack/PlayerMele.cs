using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMele : Weapon
{

    public bool isAttacking = false;

    public PlayerAttack plAttack;

    public int meleIndex;

    public bool nowAttacking = false;

    public override void OnTriggerStay(Collider other)
    {
       
            if (isAttacking)
            {
                if (plAttack.hits[meleIndex])
                {
                //   Debug.Log("attacked with the index" + meleIndex);

               

                    base.OnTriggerStay(other);
                }
                else
                {
                
                }
            }
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        // base.OnTriggerEnter(other);
    }

    public void Update()
    {
        isAttacking = wielder.isAttacking;

        if (!isAttacking) onlyOnce = true;
    }

}






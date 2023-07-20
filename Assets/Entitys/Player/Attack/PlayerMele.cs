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

    public bool worksOnMeleIndex;

    public bool nowAttacking = false;

    public override void OnTriggerStay(Collider other)
    {

        if (isAttacking)
        {

            if (worksOnMeleIndex)
            {

                if (plAttack.hits[meleIndex])
                {
           
                    base.OnTriggerStay(other);
                }


            }
            else
            {
                if(plAttack.numberOfClicks <= 0)
                base.OnTriggerStay(other);
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






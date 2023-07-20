using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WormMele : Weapon
{
    bool isAttacking;

    public WormBoss worm;

    public int specificCycleToActivate = -1;

    public override void OnTriggerStay(Collider other)
    {
        if(specificCycleToActivate == -1)
        {
            base.OnTriggerStay(other);
        }
        else
        {
            if(worm.cycle == specificCycleToActivate)
            {
                base.OnTriggerStay(other);
            }
        }
    }

    public void Update()
    {
        isAttacking = wielder.isAttacking;

        if (!isAttacking) onlyOnce = true;
    }

}


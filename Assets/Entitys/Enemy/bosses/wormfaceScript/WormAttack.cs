using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WormAttack : EnemyAttack
{

    public WormBoss worm;

    public new void Update()
    {
      if(!worm.isAttacking && !worm.startedAttack && worm.nearPlayer)
        {
          worm.isAttacking = true;
        }
      else if(worm.currentCycleTimer <= 0)
        {
          worm.isAttacking = false;
        }

      attackDuration = worm.cycleDurations[worm.cycle];

        attackCooldown = 0;

        isAttacking = worm.isAttacking;
    }


}


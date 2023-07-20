using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WormBoss : Enemy
{

    public float extendedTargetDistance;

    public float originalTargetDistance;

    public float currentTargetDistance;

    ExtractDerivativeOf div;

    public float lastTimeVal;

    public int startingCycle;

    public new void Start()
    {
        base.Start();

        div = new ExtractDerivativeOf(lastTimeVal, 0.1f);

        cycle = startingCycle;

        lastCycle = cycle;
    }

    public new void Update()
    {
       
        

        if (bossActive && !stunned)
        {
            div.updateLastValues(PositionUsefull.getVectorSum(transform.position),0);

            PositionUsefull.setForwardTo(transform, targetPosition.position, -1);

            bossAI();
        }
            
    }

    public int cycle;

    public int maxCycle;

   public bool bossActive = false;

    public bool hasStopped = false;

    public void bossAI()
    {
      if(cycle == 3 || cycle == 5)
        {
            currentTargetDistance = originalTargetDistance;
        }
      else
        {
            currentTargetDistance = extendedTargetDistance;
        }

      //For either i want the boss to stop when attacking in some attacks or all, i don't fucking know bro.
       if((/*(cycle == 3 || cycle == 5) &&*/ nearPlayer) || cycle == 0)
        {
            targetTransform.position = transform.position;

            hasStopped = true;
        }
       else if(!hasStopped)
        {
            targetTransform.position = targetPosition.position;
        }

        nearPlayer = getDistanceNearPlayer(currentTargetDistance);

        if(!startedAttack && isAttacking)
        {
            startedAttack = true;
        }

        handleCycles();

        handleAnimations();

        
            aoeSpikes.gameObject.SetActive(cycle == 5 && isAttacking);
        
    }



    public GameObject aoeSpikes;

    public string[] animations;

    public void handleAnimations()
    {
        if(cycle != lastCycle)
        {
            animator.SetBool(animations[lastCycle], false);
        }

        if (isAttacking)
        {
            animator.SetBool(animations[cycle], true);
        }

        //Doing this because rb.velocity is a bitch in disguise
         animator.SetBool("idle", div.getDerivativeWithTime() == 0);
         animator.SetBool("walking", div.getDerivativeWithTime() != 0);

        
      
    }

    public int lastCycle;

    public float[] cycleDurations = new float[10];

    public float currentCycleTimer;

    public bool startedAttack = false;

    public void handleCycles()
    {
        if(getDistanceNearPlayer(nearTargetDistance))
        {
            cycle = 0;
        }
        else if(lastCycle != cycle && cycle == 0)
        {
            cycle = lastCycle;
        }
        else
        {
            lastCycle = cycle;


            if(currentCycleTimer <= 0)
            {
                

                if(cycle == maxCycle)
                {
                    cycle = 1;
                }
                else
                {
                    cycle++;
                }

            }
        }

        if (currentCycleTimer > 0 && startedAttack)
            currentCycleTimer -= Time.deltaTime;

        if(cycle != lastCycle && cycle != 0)
        {
            startedAttack = false;

            currentCycleTimer = cycleDurations[cycle];

            hasStopped = false;
        }
    }
}


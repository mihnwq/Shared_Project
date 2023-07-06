using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttack : EntityAttack
{

    public Animator animator;

    public float originalAttackDuration;

    public float originalAttackCooldown;

    public string[] attackAnimations = new string[4];

    public AnimationClip[] comboAnimations = new AnimationClip[4];

    //Int to count the current combo animation and the amount of clicks the player has done
    public int numberOfClicks = 0;

    //Floats for finding if the time between combos has expired
    public float expireTime;

    public float maxExpireTime = 2f;

    public float animationSpeed = 2f;

    public bool[] hits = new bool[4];


    public string attackString = "isAttacking";


    public void Start()
    {
        attackAnimations[0] = "stance";

        attackAnimations[1] = "hit1";

        attackAnimations[2] = "hit2";

        attackAnimations[3] = "hit3";

        originalAttackDuration = attackDuration;

        originalAttackCooldown = attackCooldown;

        expireTime = maxExpireTime;
    }

    public void setAllFalse()
    {
        animator.SetBool(attackAnimations[1], false);

        animator.SetBool(attackAnimations[2], false);

        animator.SetBool(attackAnimations[3], false);
    }

    bool canClick = true;

    public float lateAttackTime;

    public override void Update()
    {

        /*  if(Input.GetMouseButtonDown(0) && !isAttacking && numberOfClicks < 3)
          {
              initiateAttack();

              attack();


          }*/

        if (Input.GetMouseButtonDown(0) && !isAttacking && numberOfClicks < 3 && canClick && !onUiState())
        {
             if(numberOfClicks == 0)
             {
                animator.SetBool(attackAnimations[0], true);

                canClick = false;

                 Invoke(nameof(initiateAttack), lateAttackTime);
             }
             else
             {
                 initiateAttack();
             }

            

        }

        handleExpireTime();

        enableAnimation();

        base.Update();

        hits[0] = animator.GetBool(attackAnimations[0]);

     //   if(numberOfClicks == 0)
       // animator.SetBool(attackAnimations[numberOfClicks], true);
    }

    public bool onUiState()
    {
        return ChainVars.isPaused || ChainVars.onInventory || ChainVars.onTrade;
    }


    /* public void initiateAttack()
     {
      //   animator.SetBool(attackAnimations[0], true);

         numberOfClicks++;

         setAttackCooldown(0);

         expireTime = maxExpireTime;

         float animationTime = getAnimationTime();

         setAttackDuration(animationTime);
     }*/

    public void initiateAttack()
    {
        //   animator.SetBool(attackAnimations[0], true);

        numberOfClicks++;

        setAttackCooldown(0);

        expireTime = maxExpireTime;

        float animationTime = getAnimationTime();

        setAttackDuration(animationTime);

        attack();

        canClick = true;

        animator.SetBool(attackString, true);
    }

    public float getAnimationTime()
    {
        return comboAnimations[numberOfClicks].length / animationSpeed;
    }



    public void handleExpireTime()
    {
        if (expireTime > 0 && numberOfClicks != 0 && !isAttacking)
            expireTime -= Time.deltaTime;


        if (expireTime <= 0)
        {
            expireTime = maxExpireTime;

            setAllFalse();

            numberOfClicks = 0;

            setAttackCooldown(originalAttackCooldown);

            setAttackDuration(originalAttackDuration);

            animator.SetBool(attackAnimations[0], false);

            animator.SetBool(attackString, false);
        }
    }

    public void enableAnimation()
    {

       if(numberOfClicks > 0)
        {
            if(!isAttacking)
            {
                hits[numberOfClicks] = false;

                if(numberOfClicks < 3)
                animator.SetBool(attackAnimations[0], true);

                animator.SetBool(attackAnimations[numberOfClicks], false);

                
            }
            else
            {
                hits[numberOfClicks] = true;

                if(numberOfClicks > 1)
                animator.SetBool(attackAnimations[0], false);

                
                animator.SetBool(attackAnimations[numberOfClicks], true);
            }
        }

       if(numberOfClicks == 3 && !isAttacking)
        {
            animator.SetBool(attackAnimations[0], false);

            animator.SetBool(attackString, false);
        }
            

    }

}


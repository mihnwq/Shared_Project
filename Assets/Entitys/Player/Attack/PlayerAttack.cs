using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttack : EntityAttack
{

    public AnimationWeightHandler weight;

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

   public PlayerKnockBackAttack knockAttack;


    public string attackString = "isAttacking";

    public string toleranceString = "tolerance";


    public void Start()
    {
        attackAnimations[0] = "stance";

        attackAnimations[1] = "hit1";

        attackAnimations[2] = "hit2";

        attackAnimations[3] = "hit3";

        originalAttackDuration = attackDuration;

        originalAttackCooldown = attackCooldown;

         expireTime = maxExpireTime;

        maxKnockDuration = knockAttack.knowckAnimationDuration;

    }

    public void setAllFalse()
    {
        animator.SetBool(attackAnimations[1], false);

        animator.SetBool(attackAnimations[2], false);

        animator.SetBool(attackAnimations[3], false);
    }

    bool canClick = true;

    public float lateAttackTime;

    public  void comboUpdate()
    {


        if (Input.GetMouseButtonDown(0) && !isAttacking && numberOfClicks < 3 && canClick && !onUiState() && !ChainVars.isKnocking)
        {

                weight.layerLerp = weight.maxLayerWeight;

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

       /* if (useTolerance)
            handleTolerance(); */

        base.Update();

        hits[0] = animator.GetBool(attackAnimations[0]);

        if(Input.GetKeyDown(knockAttack.knockKey))
        {
            resetAttack();

            attack();

            setAttackDuration(maxKnockDuration);
        }


        
     //   if(numberOfClicks == 0)
       // animator.SetBool(attackAnimations[numberOfClicks], true);
    }

    public float maxKnockDuration;
    

    public bool onUiState()
    {
        return ChainVars.isPaused || ChainVars.onInventory || ChainVars.onTrade;
    }


    //I pretty much abandonde the idea, and how tf is abandon written

  /*  //used in case animations snap in place
    public bool useTolerance = false;

    public float toleranceValue;

    public bool enabledTolerance = false;

    public void handleTolerance()
    {
        if (!isAttacking && numberOfClicks != 3 && expireTime < toleranceValue)
        {
            animator.SetBool(toleranceString, true);
            animator.SetBool(attackAnimations[0], false);
        }

        enabledTolerance = animator.GetBool(toleranceString);

        if(enabledTolerance)
        Invoke(nameof(resetCombo), toleranceValue / 2);
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
           

            resetCombo();
        }
    }

    public void resetCombo()
    {
        expireTime = maxExpireTime;

        setAllFalse();

        numberOfClicks = 0;

        setAttackCooldown(originalAttackCooldown);

        setAttackDuration(originalAttackDuration);

        animator.SetBool(attackAnimations[0], false);

        animator.SetBool(attackString, false);
    }


  
    public void enableAnimation()
    {

       if(numberOfClicks > 0)
        {
            if(!isAttacking)
            {
                hits[numberOfClicks] = false;

                if(numberOfClicks < 3 /* && !enabledTolerance*/)
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


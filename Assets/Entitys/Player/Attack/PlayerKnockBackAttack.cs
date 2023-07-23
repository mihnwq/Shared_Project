using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerKnockBackAttack : MonoBehaviour
{

    public KnockbackEffect knockBack;

    public KeyCode knockKey;

    public bool isKnocking;

    public Animator animator;

    public string knockString;

    public float delayedKnowckBack;

    public float knowckAnimationDuration;
    public float currentKnockAnimationDuration;

    public PlayerAttack plAttack;

    public void knockUpdate()
    {
        if(Input.GetKeyDown(knockKey) && plAttack.numberOfClicks <= 0)
        {

            currentKnockAnimationDuration = knowckAnimationDuration;

            if(SkillKeeper.unlockedFuriousKick)
            Invoke(nameof(initiateKnockBack), delayedKnowckBack);
        }


        if (currentKnockAnimationDuration > 0)
            currentKnockAnimationDuration -= Time.deltaTime;


        setKnocking(currentKnockAnimationDuration > 0);

        ChainVars.isKnocking = isKnocking;
    }

    public void setKnocking(bool state)
    {
        isKnocking = state;

        animator.SetBool(knockString, state);
    }

    public void initiateKnockBack()
    {
        knockBack.initiateKnicking();
    }

}


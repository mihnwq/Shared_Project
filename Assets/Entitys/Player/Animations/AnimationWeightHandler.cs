using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationWeightHandler : MonoBehaviour
{

    ExtractDerivativeOf dv;

    public Animator playerAnimator;

    public Player player;

    public PlayerAttack playerAttack;

    public PlayerStun playerStun;

    public float maxLayerWeight = 1f;

    public float minLayerWeight = 0.3f;

    public string rightLeg = "rightLegFloat";

    public int lowerBodyLayer = 0;

    public int upperBodyLayer = 1;

    public float lerpingSpeed;

    public float layerLerp;


    public void Start()
    {
        dv = new ExtractDerivativeOf(playerAttack.expireTime, 0 , 0.2f);
    }

    bool lerped = false;

    public void Update()
    {
         dv.updateLastValues(playerAttack.expireTime, 0 );

        if (!onAttack() && useInventoryItem() && lerped && InventoryManager.instance.canUsePostion && !playerStun.isStunning && !player.pt.throwing)
        {
            layerLerp = maxLayerWeight;

            playerAnimator.SetLayerWeight(upperBodyLayer, minLayerWeight);
        }
      else
        {

            playerAnimator.SetLayerWeight(upperBodyLayer, maxLayerWeight);
        }

        if ((dv.getDerivativeWithTime(true) != 0 || playerAttack.hits[0]) && !onCombo())
        {
            lerped = false;

        //    layerLerp = Mathf.Lerp(layerLerp, minLayerWeight, lerpingSpeed * Time.deltaTime);

            layerLerp = Mathf.Lerp(layerLerp, minLayerWeight, lerpingSpeed * Time.deltaTime);

            playerAnimator.SetLayerWeight(upperBodyLayer, layerLerp);
        }
        else
        {
            lerped = true;
        }


        

    }

    public bool onCombo()
    {
        return playerAttack.hits[1] || playerAttack.hits[2] || playerAttack.hits[3];
     }

    public bool onAttack()
    {
        return playerAttack.hits[0] || playerAttack.hits[1] || playerAttack.hits[2] || playerAttack.hits[3] || playerAttack.isAttacking;
    }

    public bool LastAttackEnded()
    {
        return !playerAttack.hits[3] && playerAttack.hits[0];
    }

    public bool useInventoryItem()
    {
        return InventoryManager.instance.canUsePostion;
    }

}


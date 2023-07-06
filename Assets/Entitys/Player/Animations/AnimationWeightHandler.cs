using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationWeightHandler : MonoBehaviour
{

    public Animator playerAnimator;

    public Player player;

    public PlayerAttack playerAttack;

    public float maxLayerWeight = 1f;

    public float minLayerWeight = 0.3f;

    public string rightLeg = "rightLegFloat";

    public int lowerBodyLayer = 0;

    public int upperBodyLayer = 1;

    public void Update()
    {
      if(!onAttack())
        {
            playerAnimator.SetLayerWeight(upperBodyLayer , minLayerWeight);
        }
      else
        {
            playerAnimator.SetLayerWeight(upperBodyLayer, maxLayerWeight);
        }
    }

    public bool onAttack()
    {
        return playerAttack.hits[0] || playerAttack.hits[1] || playerAttack.hits[2] || playerAttack.hits[3] || playerAttack.isAttacking;
    }

}


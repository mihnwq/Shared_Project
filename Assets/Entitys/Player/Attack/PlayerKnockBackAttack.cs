using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerKnockBackAttack : EntityAttack
{

    public KnockbackEffect knockBack;

    public KeyCode knockKey;

    public bool isKnocking;

    public Animator animator;

    public string knockString;

    public void Update()
    {
        if(Input.GetKeyDown(knockKey))
        {
            attack();

            knockBack.initiateKnicking();
        }

        isKnocking = knockBack.isKnocking;

        animator.SetBool(knockString, knockBack.isKnocking);

        ChainVars.isKnocking = isKnocking;
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Mele : Weapon
{
    public bool isAttacking = false;

    public override void OnTriggerStay(Collider other)
    {
        if (isAttacking)
        {
            base.OnTriggerStay(other);
        //    onlyOnce = false;
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


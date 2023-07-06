using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    public bool knockingStopped = false;

    public string hitID;

    public Transform bodyFromToAplyForce;

    public float knockForce;

    public Animator animator;

    public string animationToAplyKnockback;

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == hitID && !knockingStopped)
        {
            GameObject entity = other.gameObject;

            Entity ent = entity.GetComponent<Entity>();

            knockBack(ent.rb);

            knockingStopped = true;
        }

        
    }

    public void Update()
    {
      
        bool apllyKnockback = animator.GetBool(animationToAplyKnockback);

        knockingStopped = !apllyKnockback;
    }

    public void knockBack(Rigidbody rb)
    {
        rb.AddForce(bodyFromToAplyForce.forward * knockForce, ForceMode.Impulse);
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    public bool isKnocking = false;

    public string hitID;

    public Entity wielder;

    public float knockForce;

    public bool onlyOnce = false;



    public void OnTriggerStay(Collider other)
    {
        if(other.tag == hitID && isKnocking && !onlyOnce)
        {
            GameObject entity = other.gameObject;

            Entity ent = entity.GetComponent<Entity>();

            knockBack(ent.rb);

            isKnocking = false;

            onlyOnce = true;
        }
        
    }


    public float knockingTime = 0;

    public float maxKnockingTime;

    public void Update()
    {
        //remember to put here a condition tho

        if (knockingTime > 0)
            knockingTime -= Time.deltaTime;

        isKnocking = knockingTime > 0;

        if (!isKnocking)
            onlyOnce = false;
    }

    public void initiateKnicking()
    {
        knockingTime = maxKnockingTime;
    }

    public void knockBack(Rigidbody rb)
    {
        rb.AddForce(wielder.entityObj.forward * knockForce, ForceMode.Impulse);
    }

}


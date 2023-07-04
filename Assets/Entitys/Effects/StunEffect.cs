using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StunEffect : MonoBehaviour
{
    public string entityID;

    public float stunDuration;
    public void OnTriggerStay(Collider other)
    {
        

        if(other.tag == entityID && isStunning)
        {
            GameObject entity = other.gameObject;

            Entity currentEntity = entity.GetComponent<Entity>();

            

            currentEntity.stun(stunDuration);
        }
    }


    public float stunAttackDuration = 1.5f;

    public bool isStunning = false;

    public float maxStunAttackDuration = 2f;
    public float attackDuration = 0f;

    public void Update()
    {
        if (attackDuration > 0)
            attackDuration -= Time.deltaTime;
    }

    public void stunEntity()
    {
        if (attackDuration > 0)
            return;
        else attackDuration = maxStunAttackDuration;

        isStunning = true;

        Invoke(nameof(stopStun), stunAttackDuration);
    }

    public void stopStun()
    {
        

        isStunning = false;
    }
}


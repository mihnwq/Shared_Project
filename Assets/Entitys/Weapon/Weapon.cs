using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//script MUST be used on the weapon
public class Weapon : MonoBehaviour
{

    public float weaponDamage;

    public string hitID;

    public void getDamage(float damage)
    {
        weaponDamage = damage;
    }

    public void getHitID(string ID)
    {
        hitID = ID;
    }

    public Entity wielder;

    public void setWielder(Entity entity)
    {
        wielder = entity;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == hitID)
        {
            GameObject entity = other.gameObject;

            Entity currentEntity = entity.GetComponent<Entity>();

            

            if (!currentEntity.hasEyeFrames)
            {
                currentEntity.health -= calculateDamage(weaponDamage, wielder.entityDamage, currentEntity.entityDefense);

                

                if (currentEntity.health <= 0)
                {
                    wielder.EXP += currentEntity.EXPgiven;
                }

            }

        }
    }

    public bool onlyOnce = false;

    public virtual void OnTriggerStay(Collider other)
    {

        

        if (other.tag == hitID && onlyOnce)
        {
            
                GameObject entity = other.gameObject;

                Entity currentEntity = entity.GetComponent<Entity>();


            
            if (!currentEntity.hasEyeFrames)
            {

                currentEntity.health -= calculateDamage(weaponDamage,wielder.entityDamage,currentEntity.entityDefense);

                Debug.Log(calculateDamage(weaponDamage, wielder.entityDamage, currentEntity.entityDefense));

            }

                onlyOnce = false;

            
        }
    }

    public float calculateDamage(float baseDamage, float attackerStrength, float targetDefense)
    {
        
        float damageModifier = 1f + (attackerStrength * 0.1f);

        
        float damage = Mathf.Max(0, baseDamage - targetDefense);

        
        damage = Mathf.RoundToInt(damage * damageModifier);

        return damage;
    }


}


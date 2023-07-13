using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ProjectileThrowing : MonoBehaviour
{

    public Transform attackPoint;
    public GameObject objectToThrow;

    public int totalThrows;
    public int throwCooldown;

    public float throwForce;
    public float throwUpwardForce;

    public Vector3 actualDirection;

    public bool readyToThrow = true;

    public float mouseSensitivity = 3.0f;

    public bool throwing = false;

    public float throwingCooldown = 1f;

    public Vector3 throwDirection;

    public bool extractFromChildren;

    public void init()
    {
        actualDirection = attackPoint.forward;

        throwDirection = actualDirection;
    }

    Rigidbody projectileRb;

    //only if needed, we added a multiplier
    public void Throw(Vector3 position_multiplier)
    {

        if (totalThrows != 0)
        {
            totalThrows--;

            if (readyToThrow && !throwing)
            {

                readyToThrow = false;

                throwing = true;


                GameObject projectile = Instantiate(objectToThrow, attackPoint.position + position_multiplier, attackPoint.rotation);

                if(!extractFromChildren)
                  projectileRb = projectile.GetComponent<Rigidbody>();
                else projectileRb = projectile.GetComponentInChildren<Rigidbody>();
               

                Projectile current_projectile = projectile.GetComponent<Projectile>();

             //   Vector3 foreceToAdd = attackPoint.forward * throwForce;

                Vector3 foreceToAdd = throwDirection * throwForce;

                projectileRb.AddForce(foreceToAdd, ForceMode.Impulse);

                totalThrows--;

                Invoke(nameof(notThrowingAnymore), throwingCooldown);

                Invoke(nameof(ResetThrow), throwCooldown);

            }
        }


        if (totalThrows < 0)
            totalThrows = 0;
    }

    public void ResetThrow()
    {
        readyToThrow = true;
    }

    public void notThrowingAnymore()
    {
        throwing = false;
    }

   
}

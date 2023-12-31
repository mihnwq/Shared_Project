﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Transform projectileTransform;

    public string targetTag;

   public Entity target;

    public SphereCollider colliderRadius;

    public float radiusSize;

    public float startingRadius;

    public void Start()
    {
        startingRadius = colliderRadius.radius;
    }
    
    

    public void updateSize(float radiusSize)
    {

        colliderRadius.radius = radiusSize * startingRadius;
    }

    public void OnTriggerStay(Collider other)
    {
        
            if(other.CompareTag(targetTag))
            {
                target = other.GetComponent<Entity>();
            }
        
    }

    public void OnTriggerEnter(Collider other)
    {
       // base.OnTriggerEnter(other);
    }

    public void home(float speed)
    {
        if(target != null)
        {
            PositionUsefull.setFullForwardTo(transform, target.centre.transform.position, -1);

            PositionUsefull.moveObjectForwad(transform, speed);
        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraOverTheShoulder : MonoBehaviour
{

    public Transform target;

    public string searchTag;

    public float searchRadius = 10f;

    public Transform player;

    public Transform playerObj;

    private Vector3 offset;

    public float smoothTime = 0.002f;

    void Start()
    {
        offset = transform.position - player.position;
    }

    public bool foundTarget = false;

    public void overTheShoulderUpdate()
    {

        overTheShoulder();
    }

  

    public float aimDistance = 3f;

    public float rotationSpeed = 20f;

    public float distanceFromPlayer;

    public void overTheShoulder()
    {
        

          transform.position =  (playerObj.position - transform.forward * distanceFromPlayer) + returnRightDirection(playerObj.right);

      
        Vector3 cameraOffset = transform.position - playerObj.right;

        playerRotation = PositionUsefull.returnForwardTo(player.position,cameraOffset,1);
        
        player.forward = Vector3.Lerp(player.forward, playerRotation.normalized, rotationSpeed );

        

    }

    private Vector3 playerRotation;

    public float tolerance;

    public float maxMovingDistance;

    private Vector3 directionToSave;

    public Vector3 returnRightDirection(Vector3 vector)
    {
        if (distanceFromPlayer < maxMovingDistance)
        {
            vector *= (distanceFromPlayer / tolerance);
            directionToSave = vector;
        }


        return directionToSave;
    }

   


    // Maybe i will use this in the future
    

   /* public void findClosestEntity()
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, searchRadius);
        Transform closestTransform = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(searchTag))
            {
                float distance = Vector3.Distance(player.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestTransform = collider.transform;
                    closestDistance = distance;
                }
            }
        }

        if (closestTransform != null)
        {
            target = closestTransform;

            foundTarget = true;

            Debug.Log("Closest transform is " + closestTransform.name);
        }
        else
        {
            foundTarget = false;
        }

        
    }
   */
}


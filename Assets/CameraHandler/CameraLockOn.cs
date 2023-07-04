using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraLockOn : MonoBehaviour
{

    public Transform target;

    public string searchTag;

    public float searchRadius = 10f;

    public Transform player;

    public Transform playerObj;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    public bool foundTarget = false;

    public void lockOnUpdatepdate()
    {
        
        lockOntoEnemy();
    }

  

    public float aimDistance = 3f;

    public float rotationSpeed = 20f;

    public void lockOntoEnemy()
    {
        rotate();

        //rotating the camera

     //   Vector3 newPosition = transform.position + transform.TransformDirection(Vector3.forward) * 5;

       // target.position = newPosition;

        transform.position =  (playerObj.position - transform.forward * 4f) + playerObj.right;


        //rotation the player

        //  playerRotation = Vector3.Lerp(player.forward, transform.forward, Time.deltaTime * rotationSpeed);

        //  playerRotation.y = player.position.y;

        Vector3 cameraOffset = transform.position - playerObj.right;

        playerRotation = (player.position) - new Vector3(cameraOffset.x, player.position.y, cameraOffset.z);
        
        player.forward = Vector3.Lerp(player.forward, playerRotation.normalized, rotationSpeed );

    }

    private float mouseSensitivity = 3.0f;

    private float rotationY;
    private float rotationX;

    //   private Vector3 currentRotation;

    private Vector3 currentRotationCamera;
    private Vector3 currentRotationPlayer;


    private Vector3 playerRotation;

    private Vector3 smoothVelocity = Vector3.zero;

    public void rotate()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;

        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, -40, 40);

        Vector3 nextRoationCamera = new Vector3(rotationX, rotationY);

        currentRotationCamera = Vector3.SmoothDamp(currentRotationCamera, nextRoationCamera, ref smoothVelocity, 0.02f);

        transform.localEulerAngles = currentRotationCamera;
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


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAroundPlayer : MonoBehaviour
{

    [SerializeField]
    private float mouseSensitivity = 3.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 5.0f;

    private Vector3 currentRoation;
    private Vector3 smoothVelocity  = Vector3.zero;

    [SerializeField]
    private float maxDistance = 100;
    
    [SerializeField]
    private float minDistance = 3;

    [SerializeField]
    private float smoothTime = 0.2f;

    private bool isColliding = false;

    PlayerOrbit po;

    public float flyForce = 10f;
    public float followForce = 5f;

    public Rigidbody rb;

    public void Start()
    {
        po = GetComponent<PlayerOrbit>();

    }

   public void updateNormalView()
    {
       moveAroundPlayer();
       po.OrbitUpdate();
    }

    public void moveAroundPlayer()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;

        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, -20, 40);

        Vector3 nextRoation = new Vector3(rotationX, rotationY);

        currentRoation = Vector3.SmoothDamp(currentRoation, nextRoation, ref smoothVelocity,smoothTime);

        transform.localEulerAngles = currentRoation;

        getToPlayerPosition();
        
    }

    public void getToPlayerPosition()
    {
        Vector3 nextPosition = target.position - transform.forward * distanceFromTarget;

        Vector3 offset = Vector3.up;

        //offset if needed :P
        offset.y -= offset.y / 2;

        Vector3 finalTarget = nextPosition + offset;

        transform.position = finalTarget;
    }

    public void zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
        {
            distanceFromTarget += 2;

            if (distanceFromTarget > maxDistance)
            {
                distanceFromTarget = maxDistance;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distanceFromTarget -= 2;

            if (distanceFromTarget < minDistance)
            {
                distanceFromTarget = minDistance;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

}

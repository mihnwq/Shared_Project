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
    public float distanceFromTarget = 5.0f;

    private Vector3 currentRoation;
    private Vector3 smoothVelocity  = Vector3.zero;

    [SerializeField]
    private float maxDistance = 100;
    
    [SerializeField]
    private float minDistance = 3;

    [SerializeField]
    public float mapSmoothTime = 0.2f;

    private bool isColliding = false;

    PlayerOrbit po;

    public float smallestClampDown = 0;

    public ExtractDerivativeOf dv;

    public Rigidbody rb;

    public void Start()
    {
        po = GetComponent<PlayerOrbit>();

        dv = new ExtractDerivativeOf(0.2f, 0);
    }

   public void updateNormalView()
    {
       moveAroundPlayer(mapSmoothTime,smallestClampDown);
       po.OrbitUpdate();
    }

    public float minClampDown = -20f;

    public void moveAroundPlayer(float smoothTime,float smallestClampDown)
    {

        dv.updateLastValues(rotationX, 0);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;

        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, getMin(rotationX,rotationY,minClampDown,smallestClampDown), 40);

        Vector3 nextRoation = new Vector3(rotationX, rotationY);

        currentRoation = Vector3.SmoothDamp(currentRoation, nextRoation, ref smoothVelocity,smoothTime);

        transform.localEulerAngles = currentRoation;

        getToPlayerPosition();

        
    }

    public float tolerance;

    public float getMin(float x,float y,float actualMin,float smallestClampDown)
    {
    //  Debug.Log( ( Mathf.Atan2(x, y) * Mathf.Rad2Deg ) / 2);

        //Checks if the camera is not close on being parralel with the terrian so it won't go into it.
        if (Mathf.Atan2(x,y) < tolerance)
        {
            return smallestClampDown;
        }     

        return actualMin;
    }

    private Vector3 finalRotation;

    public void getToPlayerPosition()
    {
        Vector3 nextPosition = target.position - transform.forward * distanceFromTarget;

        Vector3 offset = Vector3.up;

        //offset if needed :P
        offset.y -= offset.y / 2;

        Vector3 finalTarget = nextPosition + offset;

        

    
            transform.position = finalTarget;
    
    }

    public float zoom(float distanceFromTarget)
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

        return distanceFromTarget;
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

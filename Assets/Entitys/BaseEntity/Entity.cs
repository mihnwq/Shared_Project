using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Entity : MonoBehaviour
{ 
   public float health;
   public float speed;

   public float entityDamage;
   public float entityDefense;

   public float LVL;
   public float nextLVL;
   public float EXPgiven;
   public float EXP;

   public Rigidbody rb;

   public Transform centre;
   
   public float airMultiplier = 0.4f;

   public CapsuleCollider capsuleCollider;

    public BoxCollider boxCollider;

   //Entity direction and orientation
   public Vector3 moveDirection;

   public float maxHealth;

   public Transform downObject;
   
   public float maxSlopeAngle = 85;

   public float eyeFramesDuratin;

   public bool hasEyeFrames = false;

   public bool isAttacking = false;

   public RaycastHit slopeHit;

   public Transform attackPoint;

   public float horizontal;
   public float vertical;

   public Transform orientation;

   public Transform entityObj;

   public Transform collider_transform;

   public Transform GroundCheck;

    //grounded checks
    

    public float Height;
    public LayerMask whatIsGround;
    public bool grounded;

    //    States st;

    public bool stunned = false;

    public void stun(float duration)
    {
        stunned = true;

        Invoke(nameof(stopStun), duration);
    }
    public void stopStun()
    {
        stunned = false;
    }
   
   public virtual void Start()
   {

        /*
        */

        if (useGroundObject)
            groudObject = GroundCheck;
        else groudObject = transform;
    }

    public float lastHealth;

   public bool hasDied;

  
    

    // Update is called once per frame
   public virtual void Update()
   {
        if (health <= 0)
            hasDied = true;

       // updateAttackPoint();
        
    }

    public virtual void updateAttackPoint()
    {
        
    }

    public float groundDrag;

    public virtual void checkGrounded()
    {
        grounded = getGrounded();


        if (grounded)
            rb.drag = groundDrag;
        else rb.drag = 0;
    }

    public bool getGrounded()
    {
     //   checkRaycast();

        return Physics.Raycast(GroundCheck.position, Vector3.down, Height, whatIsGround); 
    }

    public void checkRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(GroundCheck.position, Vector3.down, out hit, 0.8f, whatIsGround))
        {
            Debug.DrawRay(GroundCheck.position, Vector3.down * hit.distance, Color.green);
            // Ground detected, do something
        }
        else
        {
            Debug.DrawRay(GroundCheck.position, Vector3.down * 0.8f, Color.red);
            // Ground not detected, do something else
        }
    }

    public bool useGroundObject;
   public Transform groudObject;


    public bool onSlope()
   {
       

       if (Physics.Raycast(groudObject.position, Vector3.down, out slopeHit, Height) && grounded)
       {

               float angle = PositionUsefull.extractRawAngle(Vector3.up, slopeHit.normal);

            

            return angle >= maxSlopeAngle;
        }

       return false;
   }

  public  RaycastHit ground;

    public void groundHit()
    {
        if (Physics.Raycast(groudObject.position, Vector3.down, out slopeHit, Height))
            ground = slopeHit;
    }


    public bool slope;

  public Vector3 getSlopeMovementDir(Vector3 direction)
   {
            return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
   }

   public void getSlopeForwardDir()
    {
        ChainVars.playerSlopeForwardDir = Vector3.ProjectOnPlane(entityObj.forward, slopeHit.normal).normalized;
    }


}

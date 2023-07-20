using UnityEngine;

public class Sliding
{
    public Transform orientation;

    public Transform playerObj;

    public Rigidbody rb;

    CapsuleCollider playerCollider;

    Rotate_Object ro;

    Collider_Usefull cu;

    public Transform downObject;

    public Sliding(Transform orientation, Transform playerObj, Rigidbody rb, CapsuleCollider playerCollider, Transform collider_transofrm, Transform downObject)
    {
        this.orientation = orientation;
        this.playerObj = playerObj;
        this.rb = rb;
        this.downObject = downObject;
        this.playerCollider = playerCollider;
        

        cu = new Collider_Usefull(playerCollider, collider_transofrm);

        ro = new Rotate_Object(playerObj);
    }

     public float maxSlideTime = 0.75f;

   // public float maxSlideTime = 10f;

    public float slideForce = 50f;
    public float sliderTimer;

    public float slideYScale = 0.5f;
    public float startYScale;
    
    public bool sliding;

    public bool firstDir = false;

    private Vector3 finalDir;

    public void startSliding()
    {
        sliding = true;

        firstDir = true;

        float angle = PositionUsefull.getTerrainAngle(downObject.position, Vector3.down) / 2;

        cu.rotateCollider(angle,playerObj,1f);

       // ro.rotate(angle * 2);

        sliderTimer = maxSlideTime;
    }

    Vector3 direction;

    Vector3 forwardDirection;

    public void slidingMovement()
    {

          float angle = PositionUsefull.getTerrainAngle(downObject.position, Vector3.down) / 2;

        Vector3 inputDir = playerObj.forward;

        forwardDirection = getDirection(inputDir);

        if (ChainVars.playerOnSlope)
        {

            

            direction = ChainVars.playerSlopeMovementDir;
        }
        else
        {
      

            direction = forwardDirection;

         
            sliderTimer -= Time.deltaTime;
            
        }


        playerObj.forward = forwardDirection;

        rb.AddForce(direction.normalized * slideForce, ForceMode.Force);

        cu.rotateCollider(angle, playerObj);


        if (sliderTimer <= 0)
            stopSlide();
    }

   
    public void stopSlide()
    {
       
        firstDir = true;

        cu.normalize();

   //     ro.normalize();

        sliding = false;
    }

    Vector3 getDirection(Vector3 dir)
    {
        if (firstDir)
        {
            
            finalDir = dir;
            firstDir = false;
        }

        return finalDir;
    }


   
}

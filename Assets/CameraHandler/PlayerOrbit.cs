using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrbit : MonoBehaviour
{
    
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float lockOnSpeed = 20f;

    public float rotationSpeed = 7f;

    Transform current_orientation;

    public bool isLocked = false;

    public bool start = true;

    public Player pl;

     
    public void checkOrientation()
    {

        if (start)
        {
            current_orientation = orientation;
            start = false;
        }
         
        if (Input.GetKeyDown(KeyCode.Tab) && !isLocked )
        {
           // current_orientation = playerObj;

            isLocked = true;
        }else if (Input.GetKeyDown(KeyCode.Tab) && isLocked)
        {
            //current_orientation = orientation;
            isLocked = false;
        }

        if (!ChainVars.playerSliding /* && !ChainVars.isKnocking*/)
        {
            if (isLocked)
            {
                current_orientation = playerObj;
            }
            else if (!isLocked)
            {
                current_orientation = orientation;
            }
        }

        else current_orientation = orientation;
    }

    double offset = 1.3;

    public Vector3 smoothVelocity = Vector3.zero;

   public void OrbitUpdate()
    {
        checkOrientation();

        Vector3 viewDir = PositionUsefull.returnForwardTo(player.position, transform.position, 1); // player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

        //in case the model's center breaks and it's urgent, you can offset the x and y :P
        // viewDir.x += (float)offset;


       

            if (!isLocked)
            {
            current_orientation.forward = viewDir; //viewDir.normalized;

               
                float horizontal = CustomInputManager.GetAxisRaw("Horizontal");
                float vertical = CustomInputManager.GetAxisRaw("Vertical");

                Vector3 inputDir = current_orientation.forward * vertical + current_orientation.right * horizontal;

                if (inputDir != Vector3.zero && !ChainVars.playerSliding/* && !ChainVars.isKnocking*/)
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
            else
            {
                current_orientation.forward = Vector3.Slerp(current_orientation.forward, viewDir/* viewDir.normalized*/, lockOnSpeed);
            }

        


        ChainVars.playerIsLocked = isLocked;
        
    }


}

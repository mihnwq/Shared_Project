using System;
using UnityEngine;


public class Dashing : MonoBehaviour
{
    
    public Transform playerObj;
    public Rigidbody rb;
    private Player pl;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        pl = GetComponent<Player>();

        
    }

    public float dashForce = 40f;

    public float dashDuration = 0.25f;
    
    public float dashCd = 1f;
    public float dashCdTimer = 0f;

    public float vertical;
    public float horizontal;



    public void dashUpdate()
    {
        if(Input.GetKeyDown(KeyCode.C))
            Dash();

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;

        vertical = CustomInputManager.GetAxisRaw("Vertical");

        horizontal = CustomInputManager.GetAxisRaw("Horizontal");

        
    }

    public void Dash()
    {
        if (dashCdTimer > 0)
        {
            return;
        }
        else
        {
            dashCdTimer = dashCd;
        }
        
        pl.dashing = true;
        pl.hasEyeFrames = true;

        Vector3 dashDirection;

        if(ChainVars.playerIsLocked)
        {
            Vector3 direction = playerObj.forward * vertical + playerObj.right * horizontal;

            if(direction != Vector3.zero)
            {
                dashDirection = direction;
            }
            else
            {
                dashDirection = playerObj.forward;
            }
        }
        else
        {
            dashDirection = playerObj.forward;
        }

        dashDirection *= dashForce;

        //did this because forward is a direction stored in a vector, and it times vertical suggests if the direction is negative or positive, too for right

        delayedForce = dashDirection;

        Invoke(nameof(addDelayedForce),0.025f);
        Invoke(nameof(ResetDash),dashDuration);
    }

   

    private Vector3 delayedForce;

    private void addDelayedForce()
    {
        rb.AddForce(delayedForce,ForceMode.Impulse);
    }

    public void ResetDash()
    {
        pl.dashing = false;
        pl.hasEyeFrames = false;
    }


}

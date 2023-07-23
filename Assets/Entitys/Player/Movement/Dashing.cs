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

        timeToRechargeDash = 0;
    }

    public float dashForce = 40f;

    public float dashDuration = 0.25f;
    
    public float dashCd = 1f;
    public float dashCdTimer = 0f;

    public float vertical;
    public float horizontal;

    public int maxDashCounter;
    public float maxDashTime;
    public int currentDashCount;
    public float timeToRechargeDash;
    public bool recharged;

    public void dashUpdate()
    {
        if(Input.GetKeyDown(optionsValues.dashKey) && currentDashCount > 0)
        {
            Dash();

            if(!SkillKeeper.gatheredEyeFrames)
            useDash();
        }

        //remember to use the axis from the player
        vertical = CustomInputManager.GetAxisRaw("Vertical");

        horizontal = CustomInputManager.GetAxisRaw("Horizontal");

        
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;

        if (!SkillKeeper.gatheredEyeFrames)
        {
            if (timeToRechargeDash > 0)
                timeToRechargeDash -= Time.deltaTime;


            if (currentDashCount < maxDashCounter)
                if (timeToRechargeDash <= 0)
                    rechargeDash();

            if (currentDashCount > maxDashCounter)
                currentDashCount = maxDashCounter;
        }
    }

    public void rechargeDash()
    {
        currentDashCount++;

        timeToRechargeDash = maxDashTime;
    }

    public void useDash()
    {
        currentDashCount--;

        timeToRechargeDash = maxDashTime;
    }

    Vector3 veryfinaldir;

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

        if(SkillKeeper.gatheredEyeFrames)
        pl.hasEyeFrames = true;

        Vector3 dashDirection;

        if(ChainVars.playerIsLocked)
        {
            Vector3 direction = PositionUsefull.getObjectNextDirection(playerObj, vertical, horizontal);
         


            if (direction != Vector3.zero)
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

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Entity
{

    public GameObject aimRender;

    public link linq;

    public int currency = 0;

    public Transform mainCamera;

    public Transform aimObject;
    Vector3 initialAimPosition;

    public float jumpCooldown;
    public bool readyToJump;

    public float moveSpeed = 2.5f;
    public float sprintSpeed = 5.5f;

    public float slideSpeed = 7f;

    public float dashSpeed = 10f;

    public float stamina;
    public float maxStamina;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed; 
    
    public float crouchSpeed = 1.5f;

    public float dashSpeedChangeFacotr = 50f;

    public static Player instance;

    public Animator anim;

    bool playerExitSlope = false;

    public int currentSkillPoints = 0;

    public Item knifeItem;

    Jumping jump;
    Crouching cr;
    public Sliding sd;
    Dashing dash;
    public ProjectileThrowing pt;
    public PlayerStun ps;
    public PlayerKnockBackAttack pka;


    public Projectile playerProjectile;

    //chargeable knifes
    public float maxChargeLevel = 4;
    public float chargeTime = 3f;

    //don't touch here
    public enum movementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        dash,
        air,
        idle,
    }

    public enum cameraStates
    {
        normal,
        locked,
        overTheShoulder
    } 

    //don't touch here
    public bool dashing = false;

    public bool isCrouching = false;

    public movementState state = movementState.idle;
    public cameraStates view = cameraStates.normal;

    public float distToGround;

    public string currentAttackAnimation;

    public Vector3 originalAttackPointPosition;

    public void Awake()
    {
        instance = this;
    }

    //don't touch here
    public override void Start()
    {
        base.Start();
        
        jump = new Jumping(transform, rb);
        cr = new Crouching(capsuleCollider , collider_transform);
        sd = new Sliding(orientation, entityObj, rb,capsuleCollider, collider_transform, GroundCheck);
        dash = GetComponent<Dashing>();

        maxHealth = 100;

        health = 100;

        speed = moveSpeed;

        stamina = maxStamina;


        entityDamage = 10;
        entityDefense = 2;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        capsuleCollider = GetComponent<CapsuleCollider>();
        collider_transform = GetComponent<Transform>();
         
        groundDrag = 5f;

        Height = 0.8f;

        initialAimPosition = aimObject.position;

        currentAnimation = "isIdle";

        previousAnimation = currentAnimation;

        sd.startYScale = transform.localScale.y;

        pt.init();

        base.Start();
    }

  


    //attack aim related stuff for the first 2 camera positions
     public override void updateAttackPoint()
      {
          if(state == movementState.crouching)
          {
              Vector3 updatedAttackPoint = originalAttackPointPosition;

              updatedAttackPoint.y -= 1f;

              attackPoint.position = updatedAttackPoint;
          }else
          {
              attackPoint.position = getAttackPosition() + entityObj.forward;
          }

          originalAttackPointPosition = getAttackPosition() + entityObj.forward;
      }

      public Vector3 getAttackPosition()
      {
          Vector3 point = entityObj.position;

          point.y += 2f;

          return point;
      }

  
    public override void Update()
    {
        base.Update();

        if (!hasDied )
        {
            if (!ChainVars.onSkillTree)
            {
                anim.SetBool("interactBrand", false);

                slope = onSlope();
                if (!ChainVars.isPaused)
                {

                    if (!ChainVars.onInventory && !ChainVars.onTrade)
                    {


                        checkGrounded();

                        if (!pka.isKnocking)
                            Move();

                        checkCommands();

                        base.Update();


                        updateStats();
                    }
                    else
                    {
                        getOnAnimation("isIdle");
                    }

                }

                ChainVars.UpdateOnSlope(onSlope());
                ChainVars.playerExitSlope = playerExitSlope;
                ChainVars.playerSliding = sd.sliding;

                gameOverScreen.SetActive(false);
               
            }
            else
            {
                getOnAnimation("interactBrand");
            }
        }
        else
        {
            getOnAnimation("ded");
           
            Invoke(nameof(atributeGameOverScreen), dyingDuration);
            
        }
        
    }

    public GameObject gameOverScreen;

    public void onTryAgainClick()
    {
        gameOverScreen.gameObject.SetActive(hasDied);

      

        getOnAnimation("isIdle");

        SaveSystem.load(ChainVars.saveID);
    }

    public void atributeGameOverScreen()
    {
        gameOverScreen.gameObject.SetActive(hasDied);
    }

    public float dyingDuration;

    public void getOnAnimation(string animation)
    {

        previousAnimation = currentAnimation;

        currentAnimation = animation;

        anim.SetBool(previousAnimation, false);

        anim.SetBool(currentAnimation, true);
    }

   

    //don't touch here
    public void Move()
    {
        updateAxis(CustomInputManager.GetAxisRaw("Horizontal"), CustomInputManager.GetAxisRaw("Vertical"));

        if (!dashing)
        {
            playerMovement();
        }
            

    }

    //don't touch here
    public void checkCommands()
    {
      
        stateHandler();

        viewHandler();

        checkJump();


        checkCrouch();

        if (stamina > 4.5f)
        {
            dash.dashUpdate();
            checkThrow();
            checkSlide();
        }

        
        addAnimation();

        
        updateAttackPoint();

        updateAimObject();

        handleThrows();
    }

    public void handleThrows()
    {
        pt.totalThrows = InventoryManager.instance.amount[knifeItem.id];
    }

    bool transition = true;

    Vector3 aimUpPosition;
    Vector3 aimDownPosition;

    float switchingSpeed = 8f;

    public void updateAimObject()
    {
        if(state == movementState.crouching || state == movementState.sliding)
        {

            aimUpPosition = new Vector3(entityObj.position.x, entityObj.position.y + 2f, entityObj.position.z);

                    aimObject.position = Vector3.Lerp(aimObject.position, aimUpPosition, Time.deltaTime * switchingSpeed);
         
        }
        else
        {
            aimDownPosition = new Vector3(entityObj.position.x, entityObj.position.y + 3f, entityObj.position.z);

                    aimObject.position = Vector3.Lerp(aimObject.position, aimDownPosition, Time.deltaTime * switchingSpeed);
       

        }
    }

    public float staminaRegenSpeed;
    public float staminaDrainSpeed;


    public float staminaRegenCooldown = 2f; // Cooldown time before stamina regen starts
    private float regenCooldownTimer = 0f; // Timer to track the cooldown

    public void updateStats()
    {
        staminaDrain();
        staminaRegen();
    }

    public void staminaRegen()
    {
        if (regenStates() && regenCooldownTimer <= 0f)
        {
            stamina = Mathf.Lerp(stamina, maxStamina, staminaRegenSpeed * Time.deltaTime);
        }
        else
        {
            regenCooldownTimer -= Time.deltaTime;
            if (regenCooldownTimer < 0f)
            {
                regenCooldownTimer = 0f;
            }
        }
    }

    public float lastStamina;

    public void staminaDrain()
    {
        if (drainStates())
        {
            if (dashing && SkillKeeper.gatheredEyeFrames)
            {
                stamina = Mathf.Lerp(stamina, 0, (staminaDrainSpeed * 4) * Time.deltaTime);
            }
            else if(state == movementState.sprinting)
            {
                stamina = Mathf.Lerp(stamina, 0, (staminaDrainSpeed / 2) * Time.deltaTime);
            }
            else
            {
                stamina = Mathf.Lerp(stamina, 0, staminaDrainSpeed * Time.deltaTime);
            }
            
            regenCooldownTimer = staminaRegenCooldown; // Start the cooldown after draining stamina
        }
     
    }


    public bool regenStates()
    {
        return state == movementState.walking || state == movementState.idle || state == movementState.crouching;
    }

    public bool drainStates()
    {
        return pt.throwing || isAttacking || (dashing && SkillKeeper.gatheredEyeFrames) || state == movementState.sprinting;
    }

    //don't touch here
    public void checkCrouch()
    {

      

        if (Input.GetKeyDown(optionsValues.crouchKey) && state != movementState.air)
        {          
                cr.crouch();                      
        }

        if (Input.GetKeyUp(optionsValues.crouchKey))
        {              
                cr.normalize();
        }
    }

    public override void checkGrounded()
    {
        //The height MUST be tuned

        grounded = getGrounded();


        if (addDrag())
            rb.drag = groundDrag;
        else rb.drag = 0;
    }

    //don't touch here


    public void checkThrow()
    {

        if (state != movementState.sliding && state != movementState.dash)
        {


            if (Input.GetKey(optionsValues.rangedAttacKey))
            {
              
                playerProjectile.charge(maxChargeLevel, chargeTime);
            }

            if(Input.GetKeyUp(optionsValues.rangedAttacKey))
            {
                    
                playerProjectile.execute();

                pt.Throw(Vector3.zero);

                if (InventoryManager.instance.amount[knifeItem.id] > 0)
                InventoryManager.instance.removeItemFromInventory(knifeItem);
            }
            

        }

    }

   

    //don't touch here
    public void checkJump()
    {
        if (Input.GetKeyDown(optionsValues.jumpKey) && readyToJump && canJump())
        {
                playerExitSlope = true;

                readyToJump = false;

                jump.Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    public bool canJump()
    {
        return grounded && state != movementState.crouching && state != movementState.sliding;
    }

    //don't touch here
    public void ResetJump()
    {
        readyToJump = true;

        playerExitSlope = false;
    }

    //don't touch here
    public void checkSlide()
    {
        if (Input.GetKeyDown(optionsValues.slideKey) && canSlide())
        {
            sd.startSliding();
        }
        
        if (Input.GetKeyUp(optionsValues.slideKey) && sd.sliding)
        {
            sd.stopSlide();
        }

        if (sd.sliding)
        {
            sd.slidingMovement();
        }
    }

    public bool canSlide()
    {
        return (state != movementState.air && state == movementState.sprinting) || goingDownOnSlope();
    }

    public bool goingDownOnSlope()
    {
        return rb.velocity.y < 0.1 && onSlope();
    }

    //don't touch here
    public IEnumerator smoothlyLerpSlopeSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed = moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            speed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime;
            yield return null;
        }

        speed = desiredMoveSpeed;
    }

    //don't touch here
    private float speedChangeFacotr;
    public IEnumerator smoothlyLerpDashSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed = moveSpeed);
        float startValue = moveSpeed;

        float boastFactor = speedChangeFacotr;

        while (time < difference)
        {
            speed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime * boastFactor;
            yield return null;
        }

        speed = desiredMoveSpeed;
        speedChangeFacotr = 1f;
        keepMomentum = false;

    }

    //don't touch here
    private bool keepMomentum = false;
    private movementState lastState;

    string currentAnimation;
    string previousAnimation;

    //don't touch here

    public string getDashAnimation()
    {
        if (horizontal == -vertical)
            if (horizontal > 0)
                return "dashingR";
            else if (horizontal < 0)
                return "dashingL";


        float sum = vertical + horizontal;

        if(sum == 1 || sum == -1)
        {
            if(vertical != 0)
            {
                if (vertical > 0)
                {
                    return "dashingF";
                }
                else return "dashingB";
 
            }else
            {
                if (horizontal > 0)
                {
                    return "dashingR";
                }
                else return "dashingL";
            }
        }

        if (sum == -2)
            return "dashingB";
        else if (sum == 2)
            return "dashingR";

        return "dashingF";
    }

    private void stateHandler()
    {
        previousAnimation = currentAnimation;

     
        if (dashing)
        {
            state = movementState.dash;
            desiredMoveSpeed = dashSpeed;
            speedChangeFacotr = dashSpeedChangeFacotr;

            if(!isLocked)
             currentAnimation = "dashingF";
            else currentAnimation = getDashAnimation();


        } else if (sd.sliding)
        {
            state = movementState.sliding;

            if (onSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;
            else
                desiredMoveSpeed = sprintSpeed;

            currentAnimation = "isSliding";
        } else if (Input.GetKey(KeyCode.LeftControl) && grounded)
        {
            state = movementState.crouching;
            desiredMoveSpeed = crouchSpeed;

            if (!isPlayerMoving())
                currentAnimation = "isCrouching";
            else currentAnimation = "isCrouchWalking";

        } else if (grounded && Input.GetKey(KeyCode.LeftShift) && stamina > 6)
        {
            state = movementState.sprinting;
            desiredMoveSpeed = sprintSpeed;

            if(isPlayerMoving())
            currentAnimation = "isRunning";
        }
        else if(grounded && !isPlayerMoving())
        {
            state = movementState.idle;
            desiredMoveSpeed = moveSpeed;

            currentAnimation = "isIdle";
        }
        else if (grounded)
        {
            state = movementState.walking;
            desiredMoveSpeed = moveSpeed;

            currentAnimation = "isWalking";
        }
        else
        {
            state = movementState.air;

            if (desiredMoveSpeed < sprintSpeed)
                desiredMoveSpeed = moveSpeed;
            else desiredMoveSpeed = sprintSpeed;

            currentAnimation = "isJumping";
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && speed != 0 && onSlope())
        {
            StopAllCoroutines();
            StartCoroutine(smoothlyLerpSlopeSpeed());
        }
        else
        {
            bool hasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
            if (lastState == movementState.dash) keepMomentum = true;


            if (hasChanged)
            {
                if (keepMomentum)
                {
                    StopAllCoroutines();
                    StartCoroutine(smoothlyLerpDashSpeed());
                }
                else
                {
                    StopAllCoroutines();
                    speed = desiredMoveSpeed;
                }
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

    }

    public bool isCrrouching()
    {
        return state == movementState.crouching;
    }

    public bool isRuunning()
    {
        return state == movementState.sprinting;
    }

    public Transform aim;

    public void viewHandler()
    {
        if(Input.GetKey(KeyCode.E))
        {
            view = cameraStates.overTheShoulder;
        }else if(isLocked)
        {
            view = cameraStates.locked;
        }
        else
        {
            view = cameraStates.normal;
        }


        if(view == cameraStates.overTheShoulder)
        {
            isLocked = true;
            
            pt.throwDirection = mainCamera.forward;
        }
        else
        {
            pt.init();
        }

        aimRender.gameObject.SetActive(view == cameraStates.overTheShoulder);
    }

    public bool addDrag()
    {
        return state == movementState.idle || state == movementState.walking || state == movementState.crouching || state == movementState.sprinting || state == movementState.sliding;
    }
    
    public void addAnimation()
    {

        if (!pka.isKnocking)
        {
            
            if (currentAnimation != previousAnimation)
            {
                anim.SetBool(previousAnimation, false);
            }

            anim.SetBool(currentAnimation, true);

            addAttackAnimation();
        }
        else
        {
            anim.SetBool(previousAnimation, false);
        }
    }

    private string throwingAnimation = "isThrowingKnife";

    private string stunningAnimation = "isUsingStun";

    public void addAttackAnimation()
    {
        if(pt.throwing && !isAttacking)
        {
            anim.SetBool(throwingAnimation, true);
            
        }else
        {
            anim.SetBool(throwingAnimation, false);
        }

      

        if(ps.isStunning)
        {
            anim.SetBool(stunningAnimation, true);
        }
        else
        {
            anim.SetBool(stunningAnimation, false);
        }
    }

    public bool isPlayerMoving()
    {
        return horizontal != 0 || vertical != 0;
    }


    public void updateAxis(float h, float v)
    {
        horizontal = h;
        vertical = v;

    }

    public bool isLocked = false;


    public void checkedLocked()
    {
        if (view != cameraStates.overTheShoulder)
        {
           

            isLocked = ChainVars.playerIsLocked;
        }
        else
        {
            ChainVars.playerIsLocked = isLocked;
        }
        
    }

    

    public Vector3 getMoveDirection()
    {
        checkedLocked();

        if (!sd.sliding)
        {

            if (isLocked)
            {
                return PositionUsefull.getObjectNextDirection(entityObj, vertical, horizontal); 
            }
        }

        return PositionUsefull.getObjectNextDirection(orientation, vertical, horizontal); 
    }

    public void playerMovement()
    {

        moveDirection = getMoveDirection();

            if (onSlope() && !playerExitSlope)
            {

                getSlopeForwardDir();

                Vector3 slopeDir = getSlopeMovementDir(moveDirection);

                rb.AddForce(sidewaysOnSlope(slopeDir , 1f) * speed * 7f, ForceMode.Force);

                ChainVars.UpdateDir(slopeDir);
              
                if (rb.velocity.y < 0.1f)
                {            
                    rb.AddForce(getSlopeDownDirection(35f), ForceMode.Force);
                }

            }
       
        if (grounded)
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

        if (!grounded)
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);

        speedControl();

         rb.useGravity = !onSlope();
    }

    public Vector3 getSlopeDownDirection( float force)
    {
        return Vector3.down * force;
    }

    public Vector3 sidewaysOnSlope(Vector3 direction, float force)
    {
        return direction * force;
    }

    public void speedControl()
    {

        if (onSlope() && !playerExitSlope)
        {     

            if (rb.velocity.magnitude > speed)
                rb.velocity = rb.velocity.normalized * speed;
        }
        else
        {         
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > speed)
            {
                Vector3 limitedVel = flatVel.normalized * speed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }

        }
    }

  


}


using Pathfinding;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;

public class Enemy : Entity
{

    public float rotX;
    public float rotZ;

    private GameObject actualObject;

    public GameObject emptyObject;

    public Transform targetTransform;

    //Ai related stuff

    public Transform targetPosition;

    Vector3 actualTarget;


    //Monster related stuff
    public float searchRadius;

    public EnemyBehaivour behaivour;

  //  public float maxDistanceFromTarget;

    public Animator animator;

    public bool nearPlayer = false;

    private Vector3 previousPosition;

    public FollowToPosition ftp;

  public AIDestinationSetter currentDestinationSetter;

    public void Start()
    {

        

        //Setting the object for the destination
        actualObject = GameObject.Instantiate(emptyObject, transform.position, transform.rotation);

        targetTransform = actualObject.GetComponent<Transform>();


        currentDestinationSetter.target = targetTransform;

        actualTarget = transform.position;


     
        currentAnimation = "isIdle";

        previousPosition = transform.position;

        

        
    }

    public Vector3 lastStart, lastEnd;


    public override void Update()
    {
        base.Update();

        if (!hasDied)
        {
            if (!stunned)
                enemyUpdate();
            else
            {
                switchOnAnimatiopn("isStunned");
                targetTransform.position = transform.position;
            }
        } else switchOnAnimatiopn("died");
    }

    public void enemyUpdate()
    {

       

          AI2();

        //   followPlayer();

      //  AI3();

        animate();

        nearPlayer = getDistanceNearPlayer(nearTargetDistance);

        previousPosition = transform.position;

        // rb.isKinematic = nearPlayer || onSlope();

      //  rb.isKinematic = true;
    }

    public string currentAnimation, previousAnimation;

    public void switchOnAnimatiopn(string animation)
    {
        previousAnimation = currentAnimation;

        animator.SetBool(previousAnimation, false);

        currentAnimation = animation;

        animator.SetBool(currentAnimation, true);
    }

    public void animate()
    {

        //doesn't need to be as accurate as the boss's one to feel smooth
        previousAnimation = currentAnimation;

        if (isAttacking)
        {
            currentAnimation = "isAttacking";
        }
        else if (transform.position != previousPosition)
        {
            currentAnimation = "isMoving";
        }
        else currentAnimation = "isIdle";


        if (previousAnimation != currentAnimation)
        {
            animator.SetBool(previousAnimation, false);
        }

        animator.SetBool(currentAnimation, true);


    }

    public float ground_offset = 1.5f;

    public float aiStoppingDistance = 0.8f;

    

    public void AI2()
    {
         Vector3 target = targetPosition.position;

          float randomX = Random.Range(-10f, 10f);
          float randomZ = Random.Range(-10f, 10f);

          target.y += ground_offset;

          Vector3 randomTarget = new Vector3(randomX, transform.position.y, randomZ);

        if (!nearPlayer)
        {
            if (isNearTarget())
            {
                
                actualTarget += randomTarget;
            }

            if (getAgro())
            {
                targetTransform.position = target;
            }
            else
            {
              

                targetTransform.position = actualTarget;
            }
        }
        else
        {
            targetTransform.position = transform.position;
        }

    }

    public float getDistanceFromPlayer()
    {
        return (transform.position - targetPosition.position).magnitude;
    }

    public float enemyAgroRadius;

    public bool getAgro()
    {
        return getDistanceFromPlayer() <= enemyAgroRadius;
    }
 

    public float nearTargetDistance = 3.5f;

    public bool getDistanceNearPlayer(float nearTargetDistance)
    {
        return getDistanceFromPlayer() < nearTargetDistance;
    }

    public bool isNearTarget()
    {
        Vector3 nearPosition = transform.position - targetTransform.position;

       
        //remeber to change this to be calculated not 0
        nearPosition.y = transform.position.normalized.y;

        return nearPosition.magnitude < nearTargetDistance;
    }



  
}


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

    public void Start()
    {

        

        //Setting the object for the destination
        actualObject = GameObject.Instantiate(emptyObject, transform.position, transform.rotation);

        targetTransform = actualObject.GetComponent<Transform>();

        AIDestinationSetter.instance.target = targetTransform;

        actualTarget = transform.position;


        //Shit needed for animation
        currentAnimation = "isIdle";

        previousPosition = transform.position;

        /*  EXP = 1;
          EXPgiven = 3;
          LVL = 1;
          nextLVL = 10;

          entityDamage = 4;
          entityDefense = 1.5f; */

        
    }

    public Vector3 lastStart, lastEnd;


    public override void Update()
    {


        if (!stunned)
            enemyUpdate();
        else
        {
            targetTransform.position = transform.position;
        }
    }

    public void enemyUpdate()
    {

        base.Update();

          AI2();

        //   followPlayer();

      //  AI3();

        animate();

        nearPlayer = getDistanceNearPlayer();

        previousPosition = transform.position;

        // rb.isKinematic = nearPlayer || onSlope();

      //  rb.isKinematic = true;
    }

    public string currentAnimation, previousAnimation;



    public void animate()
    {
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

        /*  if (!nearPlayer)
          {
              if (isNearTarget())
              {
                  actualTarget += randomTarget;
              }
          }*/

    /*      if (behaivour.getAgro() && !nearPlayer)
          {
          
            targetTransform.position = target;
          }
          else if (!nearPlayer)
          {
            
            targetTransform.position = actualTarget;
          }

         if (nearPlayer)
         {
           // agent.SetDestination(transform.position);
           targetTransform.position = transform.position;
         }*/

          if(!nearPlayer)
        {
            if (isNearTarget())
            {
                actualTarget += randomTarget;
            }

            if (behaivour.getAgro())
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

 

    public float nearTargetDistance = 3.5f;

    public bool getDistanceNearPlayer()
    {
        return (transform.position - targetPosition.position).magnitude < nearTargetDistance;
    }

    public bool isNearTarget()
    {
        Vector3 nearPosition = transform.position - targetTransform.position;

        return nearPosition.magnitude < 2f;
    }



  
}

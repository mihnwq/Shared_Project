using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBehaivour : MonoBehaviour
{

    public string playerTag = "player";

    FollowToPosition followToPosition;

    public Enemy enemy;



    public void Start()
    {
        followToPosition = new FollowToPosition(transform);
    }

    public bool getAgro()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,enemy.searchRadius);
        
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag(playerTag))
            {
                return true;
            }
        }

        return false;
    }

    public void follow(Vector3 target)
    {
        
            followToPosition.setTargetPosition(target);
            followToPosition.followPosition();
        
    }

   

}


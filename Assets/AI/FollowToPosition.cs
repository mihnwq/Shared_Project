using System;
using UnityEngine;


//expermimental class atm

public class FollowToPosition
{
    
    public Vector3 targetPosition;

    public Transform currentEntity;


    public FollowToPosition(Transform currentEntity)
    {
        this.currentEntity = currentEntity;
    }

    public void followPosition()
    {

        if (!pointReached())
        {
            float speed = Time.deltaTime * 2f;

            //   currentEntity.position = Vector3.MoveTowards(currentEntity.position, targetPosition, speed);

            //this method makes the entity move AND use physics
            currentEntity.LookAt(targetPosition);

             currentEntity.position += currentEntity.forward * speed;

            //currentEntity.position = Vector3.MoveTowards(currentEntity.position, targetPosition, speed);
        }

    }

    public bool pointReached()
    {
        if ((currentEntity.position - targetPosition).sqrMagnitude < 2f)
            return true;

        return false;
    }

    internal void setTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }
}

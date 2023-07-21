using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//Made this class for my own implementations of LookAt,MoveTwoards and more...
public struct PositionUsefull
{

    public static float extractRawAngle(Vector3 start, Vector3 end)
    {

        float dot = Vector3.Dot(start, end);

        float magnitudes = start.magnitude * end.magnitude;

     
        float cosine = dot / magnitudes;


        return Mathf.Acos(cosine) * Mathf.Rad2Deg;
    }

    public static float getTerrainAngle(Vector3 position, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, direction, out hit))
        {

            float angle = extractRawAngle(hit.normal, Vector3.down);

            return angle;
        }
        return 0f;
    }

    //has to set the position returned by this to the position of the object you want :P
    //A much better method than MoveTwoards because surprise, surprise IT USES PHYSICS,(ig i destroyed the word xd) :O.
    public static Vector3 getPositionTwoardsPosition(Transform start, Vector3 finish, RaycastHit ground, float speed)
    {
        setForwardTo(start, finish,ground ,-1);

        speed *= Time.deltaTime;

        Vector3 newForward = start.forward * speed;

        return Vector3.ProjectOnPlane(newForward,ground.normal);
    }

    public static void moveObjectTwoardsPosition(Transform start, Vector3 finish, RaycastHit ground, float speed)
    {
      start.position += getPositionTwoardsPosition( start,  finish,  ground, speed);
    }

    public static void moveObjectForwad(Transform obj, float speed)
    {
        obj.position += obj.forward * (speed * Time.deltaTime);
    }

    //Vector decreasiong (don't funcking know if i spelled that right), basically find the direction from Vector from  to Vector to
    public static void setForwardTo(Transform to, Vector3 from, float multiplier)
    {
        Vector3 forward = (to.position - new Vector3(from.x, to.position.y, from.z)) * multiplier;

        to.forward = forward.normalized;
    }

    public static Vector3 returnForwardTo(Vector3 to, Vector3 from, float multiplier)
    {
        return ((to - new Vector3(from.x, to.y, from.z)) * multiplier).normalized;
    }

    //some lil overloads for slopes
    public static void setForwardTo(Transform from, Vector3 to, RaycastHit ground, float multiplier)
    {
        Vector3 forward = (from.position - new Vector3(to.x, from.position.y, to.z)) * multiplier;

        from.forward = Vector3.ProjectOnPlane(forward.normalized, ground.normal);
    }

    public static Vector3 returnForwardTo(Vector3 to, Vector3 from, RaycastHit ground, float multiplier)
    {
        return Vector3.ProjectOnPlane(((to - new Vector3(from.x, to.y, from.z)) * multiplier).normalized, ground.normal);
    }

    //now without ignoring the Y axis

    public static void setFullForwardTo(Transform to, Vector3 from, float multiplier)
    {
        Vector3 forward = (to.position - new Vector3(from.x, from.y, from.z)) * multiplier;

        to.forward = forward.normalized;
    }

   
    public static Vector3 returnFullForwardTo(Vector3 to, Vector3 from, float multiplier)
    {
        return ((to - new Vector3(from.x, from.y, from.z)) * multiplier).normalized;
    }

    
   

    //Now for other transform related positions

    public static Vector3 getObjectNextDirection(Transform obj, float vm, float hm)
    {
        return obj.forward * vm + obj.right * hm;
    }

    //Extracts the absolute sum of the vector.
    public static float getVectorSum(Vector3 vector)
    {
        return  Mathf.Round(Mathf.Abs(vector.x) + Mathf.Abs(vector.y) + Mathf.Abs(vector.z));
    }
}


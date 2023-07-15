﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//Made this class for my own implementations of Vector3.LookAt,Vector3.MoveTwoards and more...
public struct PositionUsefull
{

    public static float extractRawAngle(Vector3 start, Vector3 end)
    {

        float dot = Vector3.Dot(start, end);

        float magnitudes = start.magnitude * end.magnitude;

        float cosine = dot / magnitudes;


        return Mathf.Acos(cosine) * Mathf.Rad2Deg;
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

    //Sets the forward of an object to the relative  X,Z position of an object making the opbject look in the same direction .In reverse, it will set the forward to the position, looking at each other.
    public static void setForwardTo(Transform from, Vector3 to , float multiplier)
    {
        Vector3 forward = (from.position - new Vector3(to.x, from.position.y, to.z)) * multiplier;

        from.forward = forward.normalized;
    }

    public static Vector3 returnForwardTo(Vector3 from, Vector3 to, float multiplier)
    {
        return ((from - new Vector3(to.x, from.y, to.z)) * multiplier).normalized;
    }

    //some lil overloads for slopes
    public static void setForwardTo(Transform from, Vector3 to, RaycastHit ground, float multiplier)
    {
        Vector3 forward = (from.position - new Vector3(to.x, from.position.y, to.z)) * multiplier;

        from.forward = Vector3.ProjectOnPlane(forward.normalized,ground.normal);
    }

    public static Vector3 returnForwardTo(Vector3 from, Vector3 to, RaycastHit ground, float multiplier)
    {
        return Vector3.ProjectOnPlane(((from - new Vector3(to.x, from.y, to.z)) * multiplier).normalized,ground.normal);
    }

    //now without ignoring the Y axis

    public static void setFullForwardTo(Transform from, Vector3 to, float multiplier)
    {
        Vector3 forward = (from.position - new Vector3(to.x, to.y, to.z)) * multiplier;

        from.forward = forward.normalized;
    }

   
    public static Vector3 returnFullForwardTo(Vector3 from, Vector3 to, float multiplier)
    {
        return ((from - new Vector3(to.x, to.y, to.z)) * multiplier).normalized;
    }

    //some lil overloads for slopes
    public static void setFullForwardTo(Transform from, Vector3 to, RaycastHit ground, float multiplier)
    {
        Vector3 forward = (from.position - new Vector3(to.x, to.y, to.z)) * multiplier;

        from.forward = Vector3.ProjectOnPlane(forward.normalized, ground.normal);
    }

    public static Vector3 returnFullForwardTo(Vector3 from, Vector3 to, RaycastHit ground, float multiplier)
    {
        return Vector3.ProjectOnPlane(((from - new Vector3(to.x, to.y, to.z)) * multiplier).normalized, ground.normal);
    }

    //Now for other transform related positions

    public static Vector3 getObjectNextDirection(Transform obj, float vm, float hm)
    {
        return obj.forward * vm + obj.right * hm;
    }
}


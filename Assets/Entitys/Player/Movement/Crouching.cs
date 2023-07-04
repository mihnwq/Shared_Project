using System.Collections;
using UnityEngine;

public class Crouching
{
    public CapsuleCollider playerCollider;

    Transform tf;

    Collider_Usefull cu;


     public Crouching(CapsuleCollider playerCollider,Transform tf)
     {

        this.playerCollider = playerCollider;
        this.tf = tf;

        cu = new Collider_Usefull(playerCollider, tf);
     }

    public void crouch()
    {       
        cu.cutFromTop(2);
       
    }


    public void normalize()
    {
        cu.normalize();
        
    }
        
}

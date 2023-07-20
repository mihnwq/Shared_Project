using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerStun : MonoBehaviour
{ 

   public StunEffect stun;

    public bool isStunning;

    public PlayerAttack plAttack;

    public void stunUpdate()
    {
        if (Input.GetKey(KeyCode.T) && plAttack.numberOfClicks <= 0)
            stun.stunEntity();

        isStunning = stun.isStunning;
    }
}


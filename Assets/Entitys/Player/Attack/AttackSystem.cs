using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class AttackSystem : MonoBehaviour
 {
    public PlayerAttack p_attack;
    public PlayerKnockBackAttack p_knock;
    public PlayerStun p_stun;

    public Player pl;

    public void Update()
    {
        if (!pl.hasDied)
        {
            p_attack.comboUpdate();
            p_knock.knockUpdate();
            p_stun.stunUpdate();
        }
    }


}


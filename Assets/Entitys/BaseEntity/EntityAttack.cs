using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{

    public bool isAttacking = false;

    public Entity currentEntity;

    public float attackDuration = 0.7f;

    public float attackCooldown = 1.5f;

    public float attackCooldownTimer = 0;

    public void setAttackDuration(float attackDuration)
    {
        this.attackDuration = attackDuration;
    }

    public void setAttackCooldown(float attackCooldown)
    {
        this.attackCooldown = attackCooldown;
    }


    public virtual void Update()
    {
        

        currentEntity.isAttacking = isAttacking;

        if (attackCooldownTimer > 0)
            attackCooldownTimer -= Time.deltaTime;
    }

    public virtual void attack()
    {
        if (attackCooldownTimer > 0)
        {
            return;
        }
        else
        {
            attackCooldownTimer = attackCooldown;
        }

        isAttacking = true;

        Invoke(nameof(resetAttack), attackDuration);
    }

    public virtual void resetAttack()
    {
        isAttacking = false;
    }




}


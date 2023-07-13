using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class link : Entity
{ 
    public Entity entity;

    public static link instance;

    public void Awake()
    {
        instance = this;
    }

    public override void Start()
    {
        health = entity.health;
    }

    public override void Update()
    {
        entity.health = health;
        hasEyeFrames = entity.hasEyeFrames;

        if (health > entity.maxHealth)
            health = entity.maxHealth;
    }

}


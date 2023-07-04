using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Health : MonoBehaviour
{
    public HealthBar bar;

    public Entity currentEntity;

    public bool rotation = false;

    public Transform camera;

    public void Start()
    {
       bar.setMaxHelath(currentEntity.health);
    }

    public void Update()
    {
       bar.setHealth(currentEntity.health);


        if (rotation)
        {
            bar.transform.LookAt(bar.transform.position + camera.forward);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Projectile : Weapon
{

    public float lifeTime = 2f;

    private float currentLifeTime;

    public float homingFactor = 1f;

    public float homingSpeed = 20f;

    public float originalHomingFactor;

    public float originalHomingSpeed;

    public float originalWeaponDamage;

    public bool canHome;

    public GameObject actualProjectile;

    public HomingProjectile homing;



    public void Start()
    {
        originalHomingSpeed = homingSpeed;
        originalWeaponDamage = weaponDamage;
        originalHomingFactor = homingFactor;

        currentLifeTime = lifeTime;
    }

    public void Update()
    {
        if(canHome)
        {
           //  homing.updateSize(homingFactor);

            //  homing.home(homingSpeed);

            home(homingSpeed);
        }

        if (currentLifeTime > 0)
            currentLifeTime -= Time.deltaTime;

       if (currentLifeTime < 0)
            Destroy(actualProjectile);
    }

    public void home(float speed)
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, homingFactor);

        Collider[] filteredColliders = colliders.Where(collider => collider.CompareTag(hitID)).ToArray();

     if (filteredColliders.Length > 0)
        {
            Collider monsterCollider = filteredColliders[0];

            GameObject entity = monsterCollider.gameObject;

            Entity currentEntity = entity.GetComponent<Entity>();

            //   transform.LookAt(currentEntity.centre.transform.position);
            //  transform.position = Vector3.MoveTowards(transform.position, currentEntity.centre.transform.position, speed * Time.deltaTime);

            PositionUsefull.setFullForwardTo(transform, currentEntity.centre.transform.position,-1);

            PositionUsefull.moveObjectForwad(transform,speed);
        }

    }
   
    public override void OnTriggerEnter(Collider other)
    {

            base.OnTriggerEnter(other);

      
       if(other.gameObject.name != "HomingCenter")
          Destroy(actualProjectile);

       // Debug.Log(other.gameObject.name);
    }

    public override void OnTriggerStay(Collider other)
    {
      //  base.OnTriggerStay(other);
    }

   

    

     public float chargeLevel = 0f;
      public float maxCharge;
      public float chargeSpeed;

      private Coroutine chargeCoroutine;

      private IEnumerator ChargeAndDecay()
      {
          while (chargeLevel < maxCharge)
          {
              chargeLevel = Mathf.Lerp(chargeLevel, maxCharge, chargeSpeed * Time.deltaTime);
              yield return null;
          }
      }

      public void charge(float maxCharge, float chargeSpeed)
      {
          if (chargeCoroutine != null)
          {
            weaponDamage = originalWeaponDamage;
            homingFactor = originalHomingFactor;
            homingSpeed = originalHomingSpeed;

            StopCoroutine(chargeCoroutine);
          }

          this.maxCharge = maxCharge;
          this.chargeSpeed = chargeSpeed;

          chargeCoroutine = StartCoroutine(ChargeAndDecay());
      }

      public void execute()
      {
          if (chargeCoroutine != null)
          {
              StopCoroutine(chargeCoroutine);
              chargeCoroutine = null;
          }

          float finalChargeLevel = chargeLevel;

          weaponDamage += finalChargeLevel;
          homingFactor += finalChargeLevel;
          homingSpeed += finalChargeLevel;

          chargeLevel = 0f;
      }
   

}


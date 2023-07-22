using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PreventCameraFromGoingIntoObjects : MonoBehaviour
{
    public Vector3 purifiedPosition;


    public bool hit = false;

    public void Update()
    {
      //  purifiedPosition = transform.position.normalized;

      //  Debug.Log(Mathf.Atan2(purifiedPosition.z + purifiedPosition.x, purifiedPosition.y));

    }

    public void OnTriggerStay(Collider other)
    {
       
    }

    public void OnTriggerExit(Collider other)
    {                                                                                                                                                               
    }

}


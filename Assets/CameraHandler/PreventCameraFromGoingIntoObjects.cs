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
      

    }

    public void OnTriggerStay(Collider other)
    {
       
    }

    public void OnTriggerExit(Collider other)
    {                                                                                                                                                               
    }

}


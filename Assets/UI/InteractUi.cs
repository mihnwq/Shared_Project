using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InteractUi : MonoBehaviour
{

    public Transform camera;

    

    public void Update()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}


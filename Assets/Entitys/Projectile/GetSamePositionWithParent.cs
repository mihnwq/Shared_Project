using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    public class GetSamePositionWithParent : MonoBehaviour
    {

    public Transform projectileOBJ;

    public void Update()
    {
        projectileOBJ.position = transform.position;
    }


}


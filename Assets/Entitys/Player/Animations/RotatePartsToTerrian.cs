using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class RotatePartsToTerrian : MonoBehaviour
{
    public Transform footL;

    public Transform footR;

    public Transform groundObj;

    public Player player;

    float angle;

    public void Update()
    {
        if(player.grounded)
        {
            angle = PositionUsefull.getTerrainAngle(groundObj.position, Vector3.down);

            

            if(angle != 0)
            {
                Vector3 newRotationL = new Vector3(-angle, footL.position.y, footL.position.z);

                Vector3 newRotationR = new Vector3(-angle, footR.position.y, footR.position.z);

                footL.localEulerAngles = newRotationL;

                footR.localEulerAngles = newRotationR;
            }
        }
    }

}


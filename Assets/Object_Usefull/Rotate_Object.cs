using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rotate_Object
{

    public Transform main_Object;

    public Quaternion original_rotation;

    public Rotate_Object(Transform main_object)
    {
        this.main_Object = main_object;

        original_rotation = main_object.rotation;
    }

    public void rotate(float angle)
    {
        Vector3 newRotation = new Vector3(main_Object.localEulerAngles.x, main_Object.localPosition.y, angle);

        main_Object.Rotate(newRotation, Space.World);
    }

    public void normalize()
    {
        main_Object.rotation = original_rotation;
    }
}


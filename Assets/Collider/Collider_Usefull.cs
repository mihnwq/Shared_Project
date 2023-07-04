using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Collider_Usefull
{


    public CapsuleCollider current_Collider;

    public Transform colider_Transform;

    public Quaternion originalRotation;

    public Vector3 originalPoint;

    public float originalY;

    public float originalSize;

    float currentOffset;

    public Collider_Usefull(CapsuleCollider curent_Collider, Transform colider_Transofrm)
    {
        this.current_Collider = curent_Collider;

        this.colider_Transform = colider_Transofrm;

        getOriginals();
    }

    public void getOriginals()
    {
      originalRotation = colider_Transform.rotation;

        originalPoint = current_Collider.center;

        originalSize = current_Collider.height;

        originalY = colider_Transform.position.y;
    }

    public void normalize()
    {
        colider_Transform.rotation = originalRotation;

        current_Collider.center = originalPoint;

        current_Collider.height = originalSize;

        Vector3 normalTransform = colider_Transform.position;

        normalTransform.y += currentOffset;

        colider_Transform.position = normalTransform;

        currentOffset = 0;
    }

    public void cutFromTop(float reduce)
    {
        float newSize = originalSize / reduce;

       
        float yOffset = newSize / 2;

        Vector3 newCenter = originalPoint;

        newCenter.y -= yOffset;

        float offset = 0.01f;
        Vector3 newPosition = colider_Transform.position;


        newPosition.y += offset;

        //tf.position = newPosition;

        current_Collider.center = newCenter;

        current_Collider.height = newSize;
    }

    public void cutFromBottom(float reduce)
    {
        float newSize = originalSize / reduce;


        float yOffset = newSize / 2;

        Vector3 newCenter = originalPoint;

        newCenter.y += yOffset;

        float offset = 0.01f;
        Vector3 newPosition = colider_Transform.position;


        newPosition.y += offset;

        //tf.position = newPosition;

        current_Collider.center = newCenter;

        current_Collider.height = newSize;

        offset = 0;
    }

    public void rotateCollider(float angle)
    {
        Vector3 newRotation = new Vector3(angle, 0f, 0f);

        //  colider_Transform.Rotate(newRotation, Space.World);

        colider_Transform.localEulerAngles = newRotation;
    }

    public void rotateCollider(float angle, Transform reference)
    {

        Vector3 newRotation = new Vector3(angle, reference.localEulerAngles.y, reference.localEulerAngles.z);

        //  colider_Transform.Rotate(newRotation, Space.World);

        colider_Transform.localEulerAngles = newRotation;
    }

    public void rotateCollider(float angle, Transform reference, float groundOffset)
    {

        Vector3 newRotation = new Vector3(angle, reference.localEulerAngles.y, reference.localEulerAngles.z);

        // colider_Transform.Rotate(newRotation, Space.World);

        colider_Transform.localEulerAngles = newRotation;

        Vector3 newY = colider_Transform.position;

        newY.y -= groundOffset;

        currentOffset = groundOffset;

        colider_Transform.position = newY;
    }
}


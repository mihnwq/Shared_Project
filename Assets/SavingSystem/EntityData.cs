using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData
{
    public float helath;
    public float[] position;

    public int[] inventoryIDS = new int[10];

    public EntityData(Entity entity)
    {

        helath = entity.health;

        position = new float[3];

        position[0] = entity.transform.position.x;
        position[1] = entity.transform.position.y;
        position[2] = entity.transform.position.z;

        inventoryIDS = InventoryManager.instance.amount;
    }
}

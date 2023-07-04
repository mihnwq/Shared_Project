using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Object : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject shackPrefab;
    public GameObject landPrefab;

   public void spawnTree(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject = Instantiate(treePrefab, position, rotation);
    }

  public  void spawnShack(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject = Instantiate(shackPrefab, position, rotation);
    }

   public void spawnLand(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject = Instantiate(landPrefab, position, rotation);
    }
}

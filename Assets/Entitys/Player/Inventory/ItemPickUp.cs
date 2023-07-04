using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    void pickUp()
    {
        
        InventoryManager.instance.add(item);
        Destroy(gameObject);
    }

    /* private void OnMouseDown()
     {
         pickUp();
     }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            pickUp();
        }
    }
  
}

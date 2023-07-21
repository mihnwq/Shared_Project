using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUseHandler : MonoBehaviour
{

    Item item;


    public static InventoryUseHandler instance;


    //when not using the inventory and on another scene please df to commennt the animator
    public void Awake()
    {
      //  animator = InventoryManager.instance.animator;

        instance = this;
    }

    public void removeItem(Item item)
    {
        if(InventoryManager.instance.amount[item.id] > 0)
        {
            if (InventoryManager.instance.amount[item.id] <= 1)
            {
                InventoryManager.instance.remove(item);

                Destroy(gameObject);
            }

            InventoryManager.instance.amount[item.id]--;
        }

      
    }

    public void addItem(Item newItem)
    {
        item = newItem;
    }

    public void useItem()
    {

            if (canUse())
            {
                switch (item.type)
                {

                    case Item.itemType.potion:
                    if(InventoryManager.instance.canUsePostion)
                    usePotion();
                        break;
                }
                removeItem(item);

            }
        
       
       
    }

    public void usePotion()
    {
        used = true;
        InventoryManager.instance.canUsePostion = false;
        InventoryManager.instance.initiateHeal();
    }


    public bool used = false;

    public bool canUse()
    {
        return item.type != Item.itemType.knife;
    }
}


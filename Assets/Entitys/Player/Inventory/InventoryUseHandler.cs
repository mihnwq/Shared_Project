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

    public void Awake()
    {
        instance = this;
    }

    public void removeItem()
    {
        if(InventoryManager.instance.amount[item.id] > 0)
        {
            if (InventoryManager.instance.amount[item.id] == 1)
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

        

        if(ChainVars.onInventory)
        {
            if (canUse())
            {
                switch (item.type)
                {

                    case Item.itemType.potion:
                        used = true;
                        link.instance.health += 10;
                        break;
                }
                removeItem();

            }
        }
       
       
    }

    public bool used = false;

    public bool canUse()
    {
        return item.type != Item.itemType.knife;
    }
}


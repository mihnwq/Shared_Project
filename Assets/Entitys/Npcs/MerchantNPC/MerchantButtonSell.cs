using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MerchantButtonSell : MonoBehaviour
 {

    public Item item;

    public void setItem(Item newItem)
    {
        item = newItem;
    }

    

    public void onMerchantSellClick()
    {
        if (InventoryManager.instance.amount[item.id] > 0)
        {
            Player.instance.currency += (item.price / 2);

            InventoryManager.instance.removeItemFromInventory(item);
        }
    }


    public void debug()
    {
        Debug.Log("works");
    }
}


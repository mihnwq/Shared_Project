using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class MerchantButtonBuy : MonoBehaviour
{
   public Item item;

    public void setItem(Item newItem)
    {
        item = newItem;
    }

    public void onMerchantBuyClick()
    {


            if(Player.instance.currency >= item.price)
            {
                InventoryManager.instance.add(item);

                Player.instance.currency -= item.price;
            }
        
    }

  

}


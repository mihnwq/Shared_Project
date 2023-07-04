using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantShop : MonoBehaviour
{

    public Item item;

    public List<Item> shopContent = new List<Item>();

    MerchantButton[] merchantItems;

    public Transform content;

    public GameObject mainItem;


   public void loadShopItems(List<Item> merchantInventory)
    {
        if(shopContent != merchantInventory)
        {
            currentIndex = 0;

            foreach(Item item in shopContent)
            {
                Destroy(item);
            }

            shopContent = merchantInventory;

            getShopItems();
        }
        else
        {
           
        }
        

        
    }

    public int currentIndex = 0;
   
    public void getShopItems()
    {
        foreach (Item item in shopContent)
        {
            GameObject obj = Instantiate(mainItem, content);


            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

            var itemImage = obj.transform.Find("ItemSprite").GetComponent<Image>();

            merchantItems = content.GetComponentsInChildren<MerchantButton>();

            merchantItems[currentIndex++].setItem(item); 

            itemName.text = item.itemName;
            itemImage.sprite = item.icon;
        }
    }

}


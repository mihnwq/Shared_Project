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

    public MerchantButtonBuy[] merchantItemsToBuy;

    public MerchantButtonSell[] merchantItemsToSell;

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



        
    }

    public int currentIndex = 0;
   
    public void getShopItems()
    {

        currentIndex = 0;

        foreach (Item item in shopContent)
        {
            GameObject obj = Instantiate(mainItem, content);

            var itemCost = obj.transform.Find("Cost").GetComponent<TextMeshProUGUI>();

            var itemImage = obj.transform.Find("ItemSprite").GetComponent<Image>();

            merchantItemsToBuy = content.GetComponentsInChildren<MerchantButtonBuy>();

            merchantItemsToBuy[currentIndex].setItem(item);

            merchantItemsToSell = content.GetComponentsInChildren<MerchantButtonSell>();

            merchantItemsToSell[currentIndex].setItem(item);

            currentIndex++;

            itemCost.text = item.price.ToString();
            itemImage.sprite = item.icon;
        }
    }

}


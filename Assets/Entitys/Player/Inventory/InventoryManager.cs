using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> items = new List<Item>();

    InventoryUseHandler[] inventory;

    public int[] amount = new int[10];

    public int currentItem = 0;

    public Transform itemContent;
    public GameObject inventoryItem;

    public Toggle enableRemove;

    public void Awake()
    {
        instance = this;
    }

    int currentIndex = 0;

    public void add(Item item)
    {

      

        if (amount[item.id] == 0)
        {
            items.Add(item);

            GameObject obj = Instantiate(inventoryItem, itemContent);


            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

            var itemImage = obj.transform.Find("ItemSprite").GetComponent<Image>();
            
             itemName.text = item.itemName;
             itemImage.sprite = item.icon;

         //   inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

           // inventory[currentIndex].addItem(items[currentIndex]);

            currentIndex++;
        }

        amount[item.id]++;

       

        setItems();
    }

   public GameObject inv;

    public bool isOpen = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            /*   if(isOpen)
               {

                   isOpen = false;

                   inv.SetActive(false);
               }
               else
               {

                   isOpen = true;

                   inv.SetActive(true);
               }*/

            isOpen = !isOpen;
            inv.SetActive(isOpen);
        }

        ChainVars.onInventory = isOpen;
    }

    public void setItems()
    {
        inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i].addItem(items[i]);
        }
    }

   public void remove(Item item)
    {
        items.Remove(item);
    }

  

   
}

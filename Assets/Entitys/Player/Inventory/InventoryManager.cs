using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public Player pl;

    public static InventoryManager instance;

    public List<Item> items = new List<Item>();

    public Item[] currentInGameItems;

   public InventoryUseHandler[] inventory;

    public int[] amount = new int[10];

    public int currentItem = 0;

    public Transform itemContent;
    public GameObject inventoryItem;

    public Item potionItem;

    public Animator animator;

    public link linkq;

    //  public string healingString;

    public string[] animationItem = new string[20];

    public float healingTime;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        animationItem[0] = "isDrinkingPotion";

      //  inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();
    }

   public int currentIndex = 0;

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

       
        }

        amount[item.id]++;



        setItems();
    }

    //Inefficcinet atm
    public void setInventoryItemsOnLoad(int[] amount)
    {
        if (amount != this.amount)
        {
            inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

            for (int i = 0; i < inventory.Length; i++)
            {


                inventory[i].destroyItem();
            }


            Array.Clear(inventory, 0, inventory.Length);

            inventory = Array.Empty<InventoryUseHandler>();

            items.Clear();

            Array.Clear(this.amount, 0, this.amount.Length);


            Debug.Log(inventory.Length);

            for (int i = 0; i < amount.Length; i++)
            {
                if (amount[i] > 0)
                {

                    add(currentInGameItems[i]);
                }
            }

            this.amount = amount;
        }
    }    

   public GameObject inv;

    public bool isOpen = false;

    public void Update()
    {
        

        if (!pl.hasDied && !ChainVars.isPaused)
        {

            if (Input.GetKeyDown(KeyCode.I))
            {
                isOpen = !isOpen;

                inv.SetActive(isOpen);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                healingHotKey();

            }

            ChainVars.onInventory = isOpen;

        }
       
    }

   public bool canUsePostion = true;

    int index;

    public void healingHotKey()
    {
        if (amount[potionItem.id] > 0 && canUsePostion)
        {

            canUsePostion = false;

            index = items.IndexOf(potionItem);

            inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

            inventory[index].removeItem(items[index]);

            initiateHeal();
        }
    }

    public void initiateHeal()
    {
        animator.SetBool(animationItem[0], true);

        Invoke(nameof(heal), healingTime);
    }

    public void heal()
    {

        useHotKey(potionItem);
      
        animator.SetBool(animationItem[0], false);

        canUsePostion = true;
    }

    public void useHotKey(Item item)
    {
        switch (item.type)
        {
            case Item.itemType.potion:
                linkq.health += 10;
                break;
        }
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

    public void removeItemFromInventory(Item item)
    {
        int  index = items.IndexOf(item);

        inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

        inventory[index].removeItem(items[index]);
    }
  

   
}

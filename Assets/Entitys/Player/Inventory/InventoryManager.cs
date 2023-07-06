using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> items = new List<Item>();

   public InventoryUseHandler[] inventory;

    public int[] amount = new int[10];

    public int currentItem = 0;

    public Transform itemContent;
    public GameObject inventoryItem;

    public Item potionItem;

    public Animator animator;

    public string healingString;

    public float healingTime;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
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

        //    inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

            //  inventory[currentIndex].addItem(items[currentIndex]);


           // inventory[currentIndex].addItem(item);

         //   currentIndex++;
        }

        amount[item.id]++;



        setItems();
    }

   public GameObject inv;

    public bool isOpen = false;

    public void Update()
    {
        //   inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

        

        if (Input.GetKeyDown(KeyCode.I))
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

        if(Input.GetKeyDown(KeyCode.H))
        {
         

            healingHotKey();

        }

        ChainVars.onInventory = isOpen;
    }

    bool canUsePostion = true;

    int index;

    public void healingHotKey()
    {
        if (amount[potionItem.id] > 0 && canUsePostion)
        {

            canUsePostion = false;

            index = items.IndexOf(potionItem);

            inventory = itemContent.GetComponentsInChildren<InventoryUseHandler>();

            inventory[index].removeItem(items[index]);

            animator.SetBool(healingString, true);

            Invoke(nameof(heal), healingTime);
        }
    }

    public void heal()
    {

        useHotKey(potionItem);
      
        animator.SetBool(healingString, false);

        canUsePostion = true;
    }

    public void useHotKey(Item item)
    {
        switch (item.type)
        {
            case Item.itemType.potion:
                link.instance.health += 10;
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

  

   
}

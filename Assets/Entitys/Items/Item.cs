using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;

    public string itemName;

    public int value;

    public Sprite icon;

    public int price;

    public itemType type;

    int index = 0;


    public enum itemType
    {
        knife,
        potion
    }
   
}

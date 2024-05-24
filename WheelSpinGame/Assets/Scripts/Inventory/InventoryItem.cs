using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public int id;
        public string itemName;
        public Sprite icon;
        public int amount;
        
        public InventoryItem(int id, string itemName, Sprite icon, int amount)
        {
            this.id = id;
            this.itemName = itemName;
            this.icon = icon;
            this.amount = amount;
        }
        
        public InventoryItem(Item.Item item)
        {
            id = item.id;
            itemName = item.itemName;
            icon = item.icon;
            amount = item.baseAmount;
        }
    }
}
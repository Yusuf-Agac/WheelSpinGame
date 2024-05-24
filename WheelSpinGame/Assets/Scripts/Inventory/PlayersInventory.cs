using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class PlayersInventory : MonoBehaviour
    {
        public List<InventoryItem> items = new();

        private void Awake()
        {
            LoadInventory();
        }

        public void AddItem(InventoryItem item)
        {
            if (items.Any(itemTemp => itemTemp.id == item.id))
            {
                items.Find(itemTemp => itemTemp.id == item.id).amount += item.amount;
            }
            else
            {
                items.Add(item);
            }
        }

        public void AddItem(Item.Item item)
        {
            var inventoryItem = new InventoryItem(item);
            AddItem(inventoryItem);
        }

        public void RemoveItem(InventoryItem item)
        {
            if (items.Any(itemTemp => itemTemp.id == item.id))
            {
                items.Find(itemTemp => itemTemp.id == item.id).amount -= item.amount;
                if (items.Find(itemTemp => itemTemp.id == item.id).amount <= 0)
                {
                    items.Remove(item);
                }
            }
        }
        
        public void SetItemAmount(InventoryItem item, int amount)
        {
            if (items.Any(itemTemp => itemTemp.id == item.id))
            {
                items.Find(itemTemp => itemTemp.id == item.id).amount = amount;
            }
            else
            {
                items.Add(item);
            }
        }
        
        public void SaveInventory()
        {
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
        }

        public void LoadInventory()
        {
            if (!File.Exists(Application.persistentDataPath + "/inventory.json")) return;
            
            var json = File.ReadAllText(Application.persistentDataPath + "/inventory.json");
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
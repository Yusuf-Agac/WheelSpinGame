using System;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WheelSpin.Wheel
{
    [Serializable]
    internal class WheelSlot
    {
        public Image image;
        public TMP_Text text;
        [Space(10)] 
        public InventoryItem item;

        public void SetSlot(InventoryItem item)
        {
            image.sprite = item.icon;
            text.text = "x" + item.amount;
            this.item = item;
        }

        public InventoryItem GetItem()
        {
            return item;   
        }
    }
}
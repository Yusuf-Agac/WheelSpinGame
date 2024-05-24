using System;
using Inventory;
using TMPro;
using UnityEngine.UI;

namespace UI.WheelSpin.Reward
{
    [Serializable]
    internal class WheelSpinRewardItem
    {
        public InventoryItem item;
        public Image image;
        public TMP_Text text;
        
        public WheelSpinRewardItem(InventoryItem item, Image image, TMP_Text text)
        {
            this.item = item;
            this.image = image;
            this.text = text;
        }
        
        public void SetItem(InventoryItem item)
        {
            this.item = item;
            image.sprite = item.icon;
            text.text = "0";
        }
        
        public void UpdateAmount()
        {
            text.text = item.amount.ToString();
        }
    }
}
using System.Collections.Generic;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WheelSpin.Wheel
{
    public class WheelContentManager : MonoBehaviour
    {
        [SerializeField] private List<WheelType> wheelTypes;
        [Space(10)]
        [SerializeField] private List<WheelSlot> slots;
        [Space(10)]
        [SerializeField] private ItemCollection itemCollection;
        [Space(10)]
        [SerializeField] private Image wheelImage;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Image frameImage;
        [SerializeField] private TMP_Text wheelName;
        
        private WheelSpinner _wheelSpinner;
        
        public WheelSlot GetSlot(int index)
        {
            return slots[Mathf.Clamp(index, 0, slots.Count - 1)];
        }
        
        public void SetupWheel(int index)
        {
            SetupWheelVisuals(wheelTypes[index]);
            SetupSlots(wheelTypes[index]);
        }

        private void SetupWheelVisuals(WheelType wheelType)
        {
            wheelImage.sprite = wheelType.wheelSprite;
            indicatorImage.sprite = wheelType.indicatorSprite;
            frameImage.color = wheelType.frameColor;
            wheelName.color = wheelType.textColor;
            wheelName.text = wheelType.name;
        }

        public void SetupSlots(WheelType wheelType)
        {
            var items = itemCollection.GetItemsByWheelType(wheelType);
            
            for (var i = 0; i < slots.Count; i++)
            {
                var item = items[i];
                slots[i].SetSlot(item);
            }
        }
    }
}
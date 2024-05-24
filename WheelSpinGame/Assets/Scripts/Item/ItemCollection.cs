using System.Collections.Generic;
using Inventory;
using UI.WheelSpin.Wheel;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewCollection", menuName = "Game/ItemCollection")]
    public class ItemCollection : ScriptableObject
    {
        public Item[] items;
        public Item bomb;

        public float chanceOfCommon;
        public float chanceOfUncommon;
        public float chanceOfRare;
        public float chanceOfEpic;
        public float chanceOfLegendary;

        private Item GetCumulativeRandomItem()
        {
            var random = Random.Range(0f, 1f);
            var total = chanceOfCommon + chanceOfUncommon + chanceOfRare + chanceOfEpic + chanceOfLegendary;
            var common = chanceOfCommon / total;
            var uncommon = chanceOfUncommon / total;
            var rare = chanceOfRare / total;
            var epic = chanceOfEpic / total;
            var legendary = chanceOfLegendary / total;

            if (random < common)
            {
                return GetItemByRarity(Rarity.Common);
            }

            if (random < common + uncommon)
            {
                return GetItemByRarity(Rarity.Uncommon);
            }
            if (random < common + uncommon + rare)
            {
                return GetItemByRarity(Rarity.Rare);
            }
            if (random < common + uncommon + rare + epic)
            {
                return GetItemByRarity(Rarity.Epic);
            }
            return GetItemByRarity(Rarity.Legendary);
        }

        private Item GetItemByRarity(Rarity rarity)
        {
            var itemsByRarity = System.Array.FindAll(items, item => item.rarity == rarity);
            if (itemsByRarity.Length == 0 && rarity != Rarity.Common) return GetItemByRarity(rarity - 1);
            return itemsByRarity[Random.Range(0, itemsByRarity.Length)];
        }

        public List<InventoryItem> GetItemsByWheelType(WheelType wheelType)
        {
            var items = new List<InventoryItem>();
            for (var i = 0; i < wheelType.slotCount; i++)
            {
                var item = new InventoryItem(GetCumulativeRandomItem());
                item.amount = (int)(item.amount * wheelType.amountMultiplier);
                items.Add(item);
            }
            AddSpecialItemsToWheel(wheelType, items);

            return items;
        }

        private void AddSpecialItemsToWheel(WheelType wheelType, List<InventoryItem> items)
        {
            if (wheelType.isBomb)
            {
                items[Random.Range(0, items.Count)] = new InventoryItem(bomb);
            }

            if (wheelType.isLegendary)
            {
                items[Random.Range(0, items.Count)] = new InventoryItem(GetItemByRarity(Rarity.Legendary));
            }
        }
    }
}
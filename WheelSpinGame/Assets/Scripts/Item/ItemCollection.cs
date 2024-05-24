using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewCollection", menuName = "Game/ItemCollection")]
    public class ItemCollection : ScriptableObject
    {
        public Item[] items;

        public float chanceOfCommon;
        public float chanceOfUncommon;
        public float chanceOfRare;
        public float chanceOfEpic;
        public float chanceOfLegendary;
        
        public Item GetCumulativeRandomItem()
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
            return itemsByRarity[Random.Range(0, itemsByRarity.Length)];
        }
    }
}
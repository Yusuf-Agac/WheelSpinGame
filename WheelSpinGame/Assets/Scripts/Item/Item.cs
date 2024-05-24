using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public Sprite icon;
        public string description;
        public Rarity rarity;
        public int baseAmount;
    }
}
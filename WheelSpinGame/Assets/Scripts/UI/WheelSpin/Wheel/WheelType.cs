using System;
using UnityEngine;

namespace UI.WheelSpin.Wheel
{
    [Serializable]
    public class WheelType
    {
        public Sprite wheelSprite;
        public Sprite indicatorSprite;
        public Color frameColor;
        public Color textColor;

        public string name;
        public int slotCount;
        public float amountMultiplier;
        public bool isBomb;
        public bool isLegendary;
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpin.Levels
{
    [Serializable]
    internal class LevelBarItem
    {
        public Image image;
        public TMP_Text text;
        
        public LevelBarItem(Image image, TMP_Text text)
        {
            this.image = image;
            this.text = text;
        }
    }
}
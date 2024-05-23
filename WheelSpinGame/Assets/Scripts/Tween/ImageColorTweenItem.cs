using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tween
{
    [Serializable]
    public class ImageColorTweenItem : TweenItem
    {
        [SerializeField] private Image item;
        [SerializeField] private Color outColor;
        
        private Color _inColor;
        
        public override void SaveInitialValues()
        {
            _inColor = item.color;
        }
        
        public override void ShowIn()
        {
            item.color = outColor;
            item.DOKill(false);
            item.DOColor(_inColor, duration).SetEase(easeType);
        }

        public override void ShowOut()
        {
            item.color = _inColor;
            item.DOKill(false);
            item.DOColor(outColor, duration).SetEase(easeType);
        }
    }
}
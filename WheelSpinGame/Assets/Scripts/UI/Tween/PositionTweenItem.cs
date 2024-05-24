using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Tween
{
    [Serializable]
    public class PositionTweenItem : TweenItem
    {
        [SerializeField] private RectTransform item;
        [SerializeField] private Vector2 outPosition;
        
        private Vector2 _inPosition;
        
        public override void SaveInitialValues()
        {
            _inPosition = item.anchoredPosition;
        }
        
        public override void ShowIn()
        {
            item.anchoredPosition = outPosition;
            item.DOKill(false);
            item.DOAnchorPos(_inPosition, duration).SetEase(easeType);
        }

        public override void ShowOut()
        {
            item.anchoredPosition = _inPosition;
            item.DOKill(false);
            item.DOAnchorPos(outPosition, duration).SetEase(easeType);
        }
    }
}
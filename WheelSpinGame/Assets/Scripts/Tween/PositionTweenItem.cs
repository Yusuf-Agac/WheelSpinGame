using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tween
{
    [Serializable]
    public class PositionTweenItem : ITweenItem
    {
        [SerializeField] private RectTransform item;
        [SerializeField] private Ease easeType;
        [SerializeField] private float duration;
        [SerializeField] private Vector3 outPosition;
        
        private Vector3 _inPosition;
        
        public void SaveInitialValues()
        {
            _inPosition = item.position;
        }
        
        public void ShowIn()
        {
            item.DOMove(_inPosition, duration).SetEase(easeType);
        }

        public void ShowOut()
        {
            item.DOMove(outPosition, duration).SetEase(easeType);
        }
    }
}
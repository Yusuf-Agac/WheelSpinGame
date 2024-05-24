using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Tween
{
    [Serializable]
    public abstract class TweenItem
    {
        [SerializeField] protected Ease easeType;
        [SerializeField] protected float duration;
        
        public abstract void SaveInitialValues();
        public abstract void ShowIn();
        public abstract void ShowOut();
    }
}
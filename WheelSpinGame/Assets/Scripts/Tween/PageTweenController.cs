using UnityEngine;

namespace Tween
{
    public class PageTweenController : MonoBehaviour
    {
        [SerializeReference, SubclassPicker] private TweenItem[] _tweenItems;
        
        private void Awake()
        {
            foreach (var tweenItem in _tweenItems) tweenItem.SaveInitialValues();
        }
        
        public void ShowIn()
        {
            
            foreach (var tweenItem in _tweenItems) tweenItem.ShowIn();
        }
        
        public void ShowOut()
        {
            foreach (var tweenItem in _tweenItems) tweenItem.ShowOut();
        }
    }
}
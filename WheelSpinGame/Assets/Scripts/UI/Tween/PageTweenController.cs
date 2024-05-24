using General.CustomEditor;
using UnityEngine;

namespace UI.Tween
{
    public class PageTweenController : MonoBehaviour
    {
        [SerializeReference, SubclassPicker] private TweenItem[] tweenItems;
        
        private void Awake()
        {
            foreach (var tweenItem in tweenItems) tweenItem.SaveInitialValues();
        }
        
        public void ShowIn()
        {
            
            foreach (var tweenItem in tweenItems) tweenItem.ShowIn();
        }
        
        public void ShowOut()
        {
            foreach (var tweenItem in tweenItems) tweenItem.ShowOut();
        }
    }
}
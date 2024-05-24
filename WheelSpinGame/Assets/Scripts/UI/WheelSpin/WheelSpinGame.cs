using UI.Tween;
using UnityEngine;

namespace UI.WheelSpin
{
    public class WheelSpinGame : MonoBehaviour
    {
        [SerializeField] private PageTweenController playgroundPageTweenController;
        [SerializeField] private PageTweenController losePageTweenController;

        public void Spin()
        {
        
        }

        public void Exit()
        {
            playgroundPageTweenController.ShowOut();
        }

        public void Bomb()
        {
            playgroundPageTweenController.ShowOut();
            losePageTweenController.ShowIn();
        }
    }
}

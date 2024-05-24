using Inventory;
using UI.Tween;
using UI.WheelSpin.Levels;
using UI.WheelSpin.Reward;
using UI.WheelSpin.Wheel;
using UnityEngine;

namespace UI.WheelSpin.General
{
    public class WheelSpinGame : MonoBehaviour
    {
        [SerializeField] private PageTweenController mainPageTweenController;
        [SerializeField] private PageTweenController playgroundPageTweenController;
        [SerializeField] private PageTweenController losePageTweenController;
        [Space(10)]
        [SerializeField] private WheelSpinRewards wheelSpinRewards;
        [SerializeField] private WheelContentManager wheelContentManager;
        [SerializeField] private LevelBarShifter levelBarShifter;
        [Space(10)]
        [SerializeField] private PlayersInventory playersInventory;

        private void Awake()
        {
            SetupWheel();
        }
        
        public void SpinEnd(int slotIndex)
        {
            var slot = wheelContentManager.GetSlot(slotIndex);
            var win = slot.item.itemName != "bomb";
            if (win) Reward(slotIndex);
            else Bomb();
            SetupWheel();
        }

        private void SetupWheel()
        {
            var zone = levelBarShifter.GetZone();
            wheelContentManager.SetupWheel(zone);
        }
        
        public void Exit()
        {
            playgroundPageTweenController.ShowOut();
            levelBarShifter.Reset();
            wheelSpinRewards.TransferRewardsToInventory();
            playersInventory.SaveInventory();
            mainPageTweenController.gameObject.SetActive(true);
            mainPageTweenController.ShowIn();
        }
        
        public void Revive()
        {
            losePageTweenController.ShowOut();
            playgroundPageTweenController.ShowIn();
        }
        
        public void Lose()
        {
            losePageTweenController.ShowOut();
            levelBarShifter.Reset();
            wheelSpinRewards.ClearRewards();
            mainPageTweenController.gameObject.SetActive(true);
            mainPageTweenController.ShowIn();
        }

        private void Reward(int slotIndex)
        {
            var slot = wheelContentManager.GetSlot(slotIndex);
            wheelSpinRewards.AddReward(slot.item, slot.image.GetComponent<RectTransform>());
            levelBarShifter.Shift();
        }

        private void Bomb()
        {
            playgroundPageTweenController.ShowOut();
            losePageTweenController.gameObject.SetActive(true);
            losePageTweenController.ShowIn();
        }
    }
}

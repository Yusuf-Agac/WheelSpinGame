using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using General.Pool;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.WheelSpin.Reward
{
    public class WheelSpinRewards : MonoBehaviour
    {
        [SerializeField] private GameObject rewardItemPrefab;
        [SerializeField] private Image floatingIconPrefab;
        [SerializeField] private WheelSpinRewardItem[] rewardItems;

        [SerializeField] private Transform rewardItemsContainer;
        [SerializeField] private PlayersInventory playersInventory;

        private PrefabPool<Transform> _rewardItemPool;
        private PrefabPool<Image> _floatingIconPool;

        private void Awake()
        {
            _rewardItemPool = new PrefabPool<Transform>(rewardItemPrefab.transform, 5, rewardItemsContainer);
            _floatingIconPool = new PrefabPool<Image>(floatingIconPrefab, 5, transform.parent.parent);
        }

        public void AddReward(Inventory.InventoryItem item, RectTransform startTransform)
        {
            var existingRewardItem = rewardItems.FirstOrDefault(rewardItem => rewardItem.item.id == item.id);

            if (existingRewardItem != null)
            {
                existingRewardItem.item.amount += item.amount;
                AddFloatingIcons(item, existingRewardItem, startTransform);
            }
            else
            {
                CreateNewRewardItem(item, startTransform);
            }
        }

        private void AddFloatingIcons(Inventory.InventoryItem item, WheelSpinRewardItem rewardItem, RectTransform startTransform)
        {
            var floatingIcons = CreateFloatingIcons(item.amount, item.icon, startTransform);
            
            floatingIcons.ForEach(floatingIcon =>
            {
                Vector3 circularRandom = Random.insideUnitCircle * 200;
                RectTransform floatingRect = floatingIcon.GetComponent<RectTransform>();
                RectTransform rewardRect = rewardItem.image.GetComponent<RectTransform>();

                floatingRect.DOMove(startTransform.position + circularRandom, Random.Range(0.3f, 1f)).OnComplete(() =>
                {
                    floatingRect.DOMove(rewardRect.position, Random.Range(0.3f, 1f)).OnComplete(() =>
                    {
                        _floatingIconPool.ReturnToPool(floatingIcon);
                        rewardItem.UpdateAmount();
                    });
                });
            });
        }

        private void CreateNewRewardItem(Inventory.InventoryItem item, RectTransform startTransform)
        {
            var rewardItemPrefab = _rewardItemPool.Get();
            var rewardItem = new WheelSpinRewardItem(item, rewardItemPrefab.GetComponentInChildren<Image>(), rewardItemPrefab.GetComponentInChildren<TMP_Text>());
            rewardItem.SetItem(item);

            rewardItems = rewardItems.Append(rewardItem).ToArray();

            AddFloatingIcons(item, rewardItem, startTransform);
        }

        private List<Image> CreateFloatingIcons(int amount, Sprite icon, RectTransform startTransform)
        {
            var floatingIcons = new List<Image>();

            for (int i = 0; i < Mathf.Clamp(amount, 1, 30); i++)
            {
                var floatingIcon = _floatingIconPool.Get();
                floatingIcon.GetComponent<RectTransform>().position = startTransform.position;
                floatingIcon.sprite = icon;
                floatingIcons.Add(floatingIcon);
            }

            return floatingIcons;
        }

        public void TransferRewardsToInventory()
        {
            foreach (var rewardItem in rewardItems)
            {
                playersInventory.AddItem(rewardItem.item);
            }
            ClearRewards();
        }

        public void ClearRewards()
        {
            foreach (var rewardItem in rewardItems)
            {
                _rewardItemPool.ReturnToPool(rewardItem.image.transform.parent);
            }
            rewardItems = Array.Empty<WheelSpinRewardItem>();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using General.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

namespace UI.WheelSpin.Levels
{
    public class LevelBarShifter : MonoBehaviour
    {
        [SerializeField] private GameObject otherLevelItemPrefab;
        [SerializeField] private GameObject currentLevelItemPrefab;
        [Space(10)]
        [SerializeField] private RectTransform otherLevelContainer;
        [SerializeField] private RectTransform currentLevelContainer;
        [Space(10)]
        [SerializeField] private int otherLevelItemCount = 20;
        [SerializeField] private int currentLevelItemCount = 2;
        [Space(10)]
        [SerializeField] private float shiftDuration = 1f;
        [SerializeField] private Ease shiftEase = Ease.InOutBack;
        [Space(10)]
        [SerializeField] private Color currentDefaultZoneTextColor;
        [SerializeField] private Color currentDefaultZoneImageColor;
        [Space(10)]
        [SerializeField] private Color currentSafeZoneTextColor;
        [SerializeField] private Color currentSafeZoneImageColor;
        [Space(10)]
        [SerializeField] private Color currentSuperZoneTextColor;
        [SerializeField] private Color currentSuperZoneImageColor;
        [Space(10)]
        [SerializeField] private Color otherDefaultZoneTextColor;
        [SerializeField] private Color otherSafeZoneTextColor;
        [SerializeField] private Color otherSuperZoneTextColor;
        
        private readonly List<LevelBarItem> _otherLevelBarItems = new();
        private readonly List<LevelBarItem> _currentLevelBarItems = new();
        
        private PrefabPool<Transform> _otherLevelBarPool;
        private PrefabPool<Transform> _currentLevelBarPool;

        private int _currentLevelIndex = 0;
        private const int ItemWidth = 100;

        private void Awake()
        { 
            _otherLevelBarPool = new PrefabPool<Transform>(otherLevelItemPrefab.transform, otherLevelItemCount, otherLevelContainer);
            _currentLevelBarPool = new PrefabPool<Transform>(currentLevelItemPrefab.transform, currentLevelItemCount, currentLevelContainer);
            
            FillBars();
        }

        private void FillBars()
        {
            FillOtherBar();
            FillCurrentBar();
        }

        private void FillOtherBar()
        {
            FillBar(_otherLevelBarPool, _otherLevelBarItems, otherLevelItemCount, false);
        }
        
        private void FillCurrentBar()
        {
            FillBar(_currentLevelBarPool, _currentLevelBarItems, currentLevelItemCount, true);
        }
        
        
        private void FillBar(PrefabPool<Transform> pool, ICollection<LevelBarItem> barItems, int itemCount, bool isCurrent)
        {
            for (var counter = 0; counter < itemCount; counter++)
            {
                var item = pool.Get();
                var levelBarItem = new LevelBarItem(item.GetComponentInChildren<Image>(), item.GetComponentInChildren<TMP_Text>());
                levelBarItem.text.text = (counter + 1).ToString();
                barItems.Add(levelBarItem);
                UpdateBarItemColors(isCurrent, levelBarItem, counter + 1);
            }
        }
        
        private void Shift()
        {
            _currentLevelIndex++;
            ShiftOtherBar();
            ShiftCurrentBar();
        }
        
        private void ShiftOtherBar()
        {
            ShiftBar(otherLevelContainer, _otherLevelBarItems, otherLevelItemCount / 2, false);
        }
        
        private void ShiftCurrentBar()
        {
            ShiftBar(currentLevelContainer, _currentLevelBarItems, currentLevelItemCount / 2, true);
        }
        
        private void ShiftBar(RectTransform container, List<LevelBarItem> barItems, int clampMax, bool isCurrent)
        {
            container.DOAnchorPosX(-ItemWidth * Mathf.Clamp(_currentLevelIndex, 0, clampMax), shiftDuration).SetEase(shiftEase).onComplete += () =>
            {
                if (_currentLevelIndex >= clampMax)
                {
                    foreach (var item in barItems)
                    {
                        var level = int.Parse(item.text.text);
                        item.text.text = (level + 1).ToString();
                        
                        UpdateBarItemColors(isCurrent, item, level + 1);
                    }
                }

                container.anchoredPosition = new Vector2(-ItemWidth * Mathf.Clamp(_currentLevelIndex, 0, clampMax - 1), 0);
            };
        }

        private void UpdateBarItemColors(bool isCurrent, LevelBarItem item, int level)
        {
            if (isCurrent)
            {
                item.text.color = level % 30 == 0 ? currentSuperZoneTextColor 
                    : level % 5 == 0 ? currentSafeZoneTextColor 
                    : currentDefaultZoneTextColor;
                item.image.color = level % 30 == 0 ? currentSuperZoneImageColor
                    : level % 5 == 0 ? currentSafeZoneImageColor
                    : currentDefaultZoneImageColor;
            }
            else
            {
                item.text.color = level % 30 == 0 ? otherSuperZoneTextColor 
                    : level % 5 == 0 ? otherSafeZoneTextColor 
                    : otherDefaultZoneTextColor;
            }
        }
    }
}
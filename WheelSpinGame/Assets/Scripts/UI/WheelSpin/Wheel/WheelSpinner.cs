using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WheelSpin.Wheel
{
    public class WheelSpinner : MonoBehaviour
    {
        [SerializeField] private RectTransform wheel;
        [SerializeField] private RectTransform indicator;
        [Space(10)]
        [SerializeField] private Button spinButton;
        [SerializeField] private Button exitButton;
        [Space(10)]
        [SerializeField] private int slotsCount;
        [SerializeField] private float maxDegreeOfIndicator = 60f;
        [Space(10)]
        [SerializeField] private Vector2 startForceBounds;
        [SerializeField] private float friction;
        
        private Coroutine _spinCoroutine;
        
        private float _speed;
        
        public void StartSpin()
        {
            var startForce = Random.Range(startForceBounds.x, startForceBounds.y);
            _spinCoroutine ??= StartCoroutine(Spin(startForce));
        }
        
        private IEnumerator Spin(float startForce)
        {
            SetButtonInteractables(false);
            _speed = startForce;
            while (_speed > 0)
            {
                CalculateWheelMomentum();
                CalculateIndicatorRotation();
                yield return null;
            }
            _spinCoroutine = null;
            SetButtonInteractables(true);
        }

        private void SetButtonInteractables(bool interactable)
        {
            spinButton.interactable = interactable;
            exitButton.interactable = interactable;
        }
        
        private void CalculateWheelMomentum()
        {
            _speed -= friction * Time.deltaTime;
            wheel.Rotate(Vector3.forward, _speed * Time.deltaTime);
        }

        private void CalculateIndicatorRotation()
        {
            var slotArea = 360f / slotsCount;
            var indicatorOffset = slotArea / 2;
            var indicatorRotation = -((wheel.rotation.eulerAngles.z + indicatorOffset) % slotArea);
            var normalizedIndicatorRotation = indicatorRotation / slotArea;
            indicator.rotation = Quaternion.Euler(0, 0, normalizedIndicatorRotation * maxDegreeOfIndicator);
        }
    }
}
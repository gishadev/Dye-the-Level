using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSampleUI : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<IDyeSample> DyeSampleUIInteracted;

        private TMP_Text _countText;
        private IDyeSample _dyeSample;

        private void Awake()
        {
            _countText = GetComponentInChildren<TMP_Text>();
        }

        public void InitializeSample(IDyeSample dyeSample, int count)
        {
            _dyeSample = dyeSample;
            UpdateCount(count);

            _dyeSample.DyeCountChanged += UpdateCount;
        }

        private void OnDisable()
        {
            _dyeSample.DyeCountChanged -= UpdateCount;
        }


        private void UpdateCount(int newCount)
        {
            _countText.text = newCount.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                DyeSampleUIInteracted?.Invoke(_dyeSample);
        }
    }
}
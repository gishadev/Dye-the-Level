using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSampleUI : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<int> DyeSampleUIInteracted;

        private TMP_Text _countText;
        private int _dyeIndex;

        private void Awake()
        {
            _countText = GetComponentInChildren<TMP_Text>();
        }

        public void InitializeSample(int index, int count)
        {
            _dyeIndex = index;
            UpdateCount(count);
        }

        public void UpdateCount(int newCount)
        {
            _countText.text = newCount.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                DyeSampleUIInteracted?.Invoke(_dyeIndex);
        }
    }
}
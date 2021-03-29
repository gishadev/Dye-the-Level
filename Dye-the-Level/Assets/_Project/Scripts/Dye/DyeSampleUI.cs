using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSampleUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMP_Text countText;

        public int DyeIndex { get; private set; }

        public void InitializeSample(int index, int count)
        {
            DyeIndex = index;
            UpdateCount(count);
        }

        public void UpdateCount(int newCount)
        {
            countText.text = newCount.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            MaterialManager.ChangeDyeSample(DyeIndex);
        }
    }
}

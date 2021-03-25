using UnityEngine;
using UnityEngine.EventSystems;

namespace Gisha.DyeTheWorld
{
    public class DyeSample : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Material sampleMaterial;

        public void OnPointerDown(PointerEventData eventData)
        {
            MaterialManager.ChangeDyeMaterial(sampleMaterial);
        }
    }
}

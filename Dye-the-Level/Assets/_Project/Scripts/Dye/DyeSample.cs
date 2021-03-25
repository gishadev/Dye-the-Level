using UnityEngine;
using UnityEngine.EventSystems;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSample : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Material sampleMaterial;

        public void SetSampleMaterial(Material newMat) => sampleMaterial = newMat;

        public void OnPointerDown(PointerEventData eventData)
        {
            MaterialManager.ChangeDyeMaterial(sampleMaterial);
        }
    }
}

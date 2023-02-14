using Gisha.DyeTheLevel.Core;
using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public class Colorable : MonoBehaviour, IColorable
    {
        public IDyeSample CurrentDye { get; private set; }

        private MeshRenderer _mr;

        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
        }

        public void ApplyDye(IDyeSample newSample)
        {
            CurrentDye = newSample;
            TryChangeMaterial(newSample.DyeMaterial);
        }

        public void RemoveDye()
        {
            CurrentDye = null;
            TryChangeMaterial(ResourceLoader.GetGameData().DiscolorMaterial);
        }

        private void TryChangeMaterial(Material materialToChange)
        {
            if (_mr == null)
                return;

            _mr.material = materialToChange;
        }
    }
}
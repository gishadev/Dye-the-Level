using Gisha.DyeTheLevel.Core;
using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public class Colorable : MonoBehaviour, IColorable
    {
        public IDyeSample CurrentDye { get; private set; }

        private MeshRenderer _mr;
        private GameData _gameData;

        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
            _gameData = ResourceLoader.GetGameData();
        }

        public void ApplyDye(IDyeSample newSample)
        {
            CurrentDye = newSample;
            TryChangeMaterial(newSample.DyeMaterial);
        }

        public void RemoveDye()
        {
            CurrentDye = null;
            TryChangeMaterial(_gameData.DiscolorMaterial);
        }

        private void TryChangeMaterial(Material materialToChange)
        {
            if (_mr == null)
                return;

            _mr.material = materialToChange;
        }
    }
}
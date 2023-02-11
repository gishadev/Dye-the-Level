using Gisha.DyeTheLevel.Core;
using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeChanger
    {
        private readonly DyeManager _dyeManager;
        private readonly GameData _gameData;

        public DyeChanger(DyeManager dyeManager, GameData gameData)
        {
            _dyeManager = dyeManager;
            _gameData = gameData;
        }

        public bool ContainsDyeSample(MeshRenderer mr, out DyeSample ds)
        {
            ds = null;

            foreach (var dyeSample in _dyeManager.Samples)
            {
                if (dyeSample.DyeMaterial == mr.sharedMaterials[0])
                {
                    ds = dyeSample;
                    return true;
                }
            }

            return false;
        }

        public void Color(MeshRenderer meshRenderer, DyeSample newSample, DyeSample oldSample)
        {
            if (oldSample != null)
            {
                oldSample.DyeCount++;
                oldSample.SampleUI.UpdateCount(oldSample.DyeCount);
            }

            meshRenderer.material = newSample.DyeMaterial;
            newSample.DyeCount--;
            newSample.SampleUI.UpdateCount(newSample.DyeCount);
        }

        public void Discolor(MeshRenderer meshRenderer, DyeSample oldSample)
        {
            if (oldSample != null)
            {
                oldSample.DyeCount++;
                oldSample.SampleUI.UpdateCount(oldSample.DyeCount);
            }

            meshRenderer.material = _gameData.DiscolorMaterial;
        }
    }
}
using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSample
    {
        public int DyeCount { get; set; }

        public DyeSampleUI SampleUI { get; }
        public Material DyeMaterial { get; }

        
        public DyeSample(DyeSampleUI sampleUI, Material dyeMaterial, int dyeCount, GameObject renderTextureObject, RenderTexture renderTexture)
        {
            SampleUI = sampleUI;
            DyeMaterial = dyeMaterial;
            DyeCount = dyeCount;
        }
    }
}
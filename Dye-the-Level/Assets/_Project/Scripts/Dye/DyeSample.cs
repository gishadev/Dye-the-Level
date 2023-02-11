using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSample
    {
        public int DyeCount { get; set; }

        public DyeSampleUI SampleUI { get; private set; }
        public Material DyeMaterial { get; private set; }

        public GameObject RenderTextureObject { get; private set; }
        public RenderTexture RenderTexture { get; private set; }

        public DyeSample(DyeSampleUI sampleUI, Material dyeMaterial, int dyeCount, GameObject renderTextureObject, RenderTexture renderTexture)
        {
            SampleUI = sampleUI;
            DyeMaterial = dyeMaterial;
            DyeCount = dyeCount;
            RenderTextureObject = renderTextureObject;
            RenderTexture = renderTexture;
        }
    }
}
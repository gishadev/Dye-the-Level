using Gisha.DyeTheLevel.Dye;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Create
    {
        public static IDyeManager DyeManager()
        {
            return Substitute.For<IDyeManager>();
        }

        public static IDyeSample DyeSample(int dyeCount = 10, string shaderName = "Specular")
        {
            return new DyeSample(new Material(Shader.Find(shaderName)), dyeCount);
        }

        public static Colorable Colorable()
        {
            return new GameObject().AddComponent<Colorable>();
        }
    }
}
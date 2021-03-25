using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public static class MaterialManager
    {
        public static Material DyeMaterial { private set; get; }

        public static void ChangeDyeMaterial(Material newMaterial)
        {
            DyeMaterial = newMaterial;
            Debug.Log("Dye Material was changed!");
        }
    }
}

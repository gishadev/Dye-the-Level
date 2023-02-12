using UnityEngine;
using System;


namespace Gisha.DyeTheLevel.Dye
{
    public class DyeSample : IDyeSample
    {
        public int DyeCount { get; private set; }
        public event Action<int> DyeCountChanged;

        public Material DyeMaterial { get; }

        public DyeSample(Material dyeMaterial, int dyeCount)
        {
            DyeMaterial = dyeMaterial;
            DyeCount = dyeCount;
        }

        public void UpdateCount(int newCount)
        {
            DyeCount = newCount;

            DyeCountChanged?.Invoke(newCount);
        }

        public void AddCount(int addCount)
        {
            UpdateCount(DyeCount + addCount);
        }
    }
}
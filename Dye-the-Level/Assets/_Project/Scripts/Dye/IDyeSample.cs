using UnityEngine;
using System;

namespace Gisha.DyeTheLevel.Dye
{
    public interface IDyeSample
    {
        int DyeCount { get; }
        Material DyeMaterial { get; }
        
        public event Action<int> DyeCountChanged;
        
        void UpdateCount(int newCount);
        void AddCount(int addCount);
    }
}
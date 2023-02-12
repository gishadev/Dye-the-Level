using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public interface IColorable
    {
        IDyeSample CurrentDye { get; }
        void ApplyDye(IDyeSample newSample);
        void RemoveDye();
    }
}
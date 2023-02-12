using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public interface IDyeChanger
    {
        bool ContainsDyeSample(IColorable colorable, out IDyeSample ds);
        void Color(IColorable colorable, IDyeSample newSample, IDyeSample oldSample);
        void Discolor(IColorable colorable, IDyeSample oldSample);
    }
}
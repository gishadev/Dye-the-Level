using System.Collections.Generic;

namespace Gisha.DyeTheLevel.Dye
{
    public interface IDyeManager
    {
        IDyeSample DyeSample { get; }
        List<IDyeSample> Samples { get; }
    }
}
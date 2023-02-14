using Gisha.DyeTheLevel.Dye;
using NSubstitute;

namespace Tests
{
    public class Setup
    {
        public static IDyeManager DyeManager(IDyeSample selectedSample)
        {
            var dyeManager = Create.DyeManager();
            dyeManager.DyeSample.Returns(selectedSample);
            return dyeManager;
        }
    }
}
using Gisha.DyeTheLevel.Core;
using Gisha.DyeTheLevel.Dye;
using NUnit.Framework;

namespace Tests
{
    public class BasicTests
    {
        [Test]
        public void WhenGettingGameData_ThenItNotNull()
        {
            // Arrange.
            GameData gameData = ResourceLoader.GetGameData();

            // Assert.
            Assert.AreNotEqual(null, gameData);
        }

        [Test]
        public void WhenDyeColoring_AndDyeIsAssigned_ThenColorableDyeEqualsAssigned()
        {
            // Arrange.
            var oldSample = Create.DyeSample();
            var selectedSample = Create.DyeSample();

            var dyeManager = Setup.DyeManager(selectedSample);
            var dyeChanger = new DyeChanger(dyeManager);
            var colorable = Create.Colorable();

            // Act.
            dyeChanger.Color(colorable, dyeManager.DyeSample, oldSample);

            // Assert.
            Assert.AreEqual(dyeManager.DyeSample, colorable.CurrentDye);
        }

        [Test]
        public void WhenDyeColoring_AndDyeIsAssigned_ThenColorableDyeMaterialNotEqualsNull()
        {
            // Arrange.
            var oldSample = Create.DyeSample();
            var selectedSample = Create.DyeSample();

            var dyeManager = Setup.DyeManager(selectedSample);
            var dyeChanger = new DyeChanger(dyeManager);
            var colorable = Create.Colorable();

            // Act.
            dyeChanger.Color(colorable, dyeManager.DyeSample, oldSample);

            // Assert.
            Assert.AreNotEqual(colorable.CurrentDye.DyeMaterial, null);
        }

        [Test]
        public void WhenDyeColoring_AndSelectedDyeCountIs10_ThenSelectedDyeCountEquals9()
        {
            // Arrange.
            var oldSample = Create.DyeSample();
            var selectedSample = Create.DyeSample(10);

            var dyeManager = Setup.DyeManager(selectedSample);
            var dyeChanger = new DyeChanger(dyeManager);
            var colorable = Create.Colorable();

            // Act.
            dyeChanger.Color(colorable, dyeManager.DyeSample, oldSample);
            // Assert.

            Assert.AreEqual(9, colorable.CurrentDye.DyeCount);
        }
        
        [Test]
        public void WhenDyeColoring_AndOldDyeCountIs0_ThenPreviousDyeCountEquals1()
        {
            // Arrange.
            var oldSample = Create.DyeSample(0);
            var selectedSample = Create.DyeSample(10);

            var dyeManager = Setup.DyeManager(selectedSample);
            var dyeChanger = new DyeChanger(dyeManager);
            var colorable = Create.Colorable();

            // Act.
            dyeChanger.Color(colorable, dyeManager.DyeSample, oldSample);
            // Assert.

            Assert.AreEqual(1, oldSample.DyeCount);
        }
        
        [Test]
        public void WhenDyeDiscoloring_AndSelectedCountIs0_ThenPreviousDyeCountEquals1()
        {
            // Arrange.
            var oldSample = Create.DyeSample(0);
            var selectedSample = Create.DyeSample(0);

            var dyeManager = Setup.DyeManager(selectedSample);
            var dyeChanger = new DyeChanger(dyeManager);
            var colorable = Create.Colorable();

            // Act.
            dyeChanger.Discolor(colorable, oldSample);
            // Assert.

            Assert.AreEqual(1, oldSample.DyeCount);
        }
    }
}
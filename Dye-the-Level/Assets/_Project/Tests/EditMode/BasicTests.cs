using Gisha.DyeTheLevel.Core;
using Gisha.DyeTheLevel.Dye;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

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
        public void WhenColorableDyeChanging_AndDyeIsAssigned_ThenColorableDyeShouldEqualAssigned()
        {
            // Arrange.
            var dyeManager = Substitute.For<IDyeManager>();
            var selectedSample = Substitute.For<IDyeSample>();
            dyeManager.DyeSample.Returns(selectedSample);

            DyeChanger dyeChanger = new DyeChanger(dyeManager);

            var colorableObj = new GameObject();
            IColorable colorable = colorableObj.AddComponent<Colorable>();

            IDyeSample oldSample = Substitute.For<IDyeSample>();
            
            // Act.
            dyeChanger.Color(colorable, dyeManager.DyeSample, oldSample);

            // Assert.
            Assert.AreSame(dyeManager.DyeSample, colorable.CurrentDye);
        }
    }
}
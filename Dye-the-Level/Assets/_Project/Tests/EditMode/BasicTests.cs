using Gisha.DyeTheLevel.Core;
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
    }
}
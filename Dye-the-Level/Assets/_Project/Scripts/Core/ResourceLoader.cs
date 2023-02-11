using UnityEngine;

namespace Gisha.DyeTheLevel.Core
{
    public static class ResourceLoader
    {
        private const string GAME_DATA_PATH = "GameData";

        public static GameData GetGameData()
        {
            return Resources.Load<GameData>(GAME_DATA_PATH);
        }
    }
}
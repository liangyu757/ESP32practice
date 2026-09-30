namespace DungeonDefense.Core
{
    /// <summary>Scene names as registered in Build Settings. Boot must be build index 0.</summary>
    public static class SceneNames
    {
        public const string Boot = "Boot";
        public const string MainMenu = "MainMenu";
        public const string Town = "Town";
        public const string Battle = "Battle";

        /// <summary>Build order. Used by the editor setup tool.</summary>
        public static readonly string[] All = { Boot, MainMenu, Town, Battle };
    }
}

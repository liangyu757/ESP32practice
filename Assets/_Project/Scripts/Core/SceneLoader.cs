using UnityEngine.SceneManagement;

namespace DungeonDefense.Core
{
    /// <summary>Maps game states to scenes and loads them.</summary>
    public static class SceneLoader
    {
        /// <summary>
        /// Returns the scene a state lives in. Pause, Reward and GameOver are overlays on the
        /// current scene, so they have no scene of their own.
        /// </summary>
        public static bool TryGetSceneFor(GameState state, out string sceneName)
        {
            switch (state)
            {
                case GameState.MainMenu: sceneName = SceneNames.MainMenu; return true;
                case GameState.Town:     sceneName = SceneNames.Town;     return true;
                case GameState.Battle:   sceneName = SceneNames.Battle;   return true;
                default:                 sceneName = null;                return false;
            }
        }

        /// <summary>Loads the scene unless it is already active (e.g. resuming from Pause to Battle).</summary>
        public static void Load(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                return;
            }

            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}

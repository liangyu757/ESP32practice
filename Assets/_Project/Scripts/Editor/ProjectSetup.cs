using System.IO;
using System.Linq;
using DungeonDefense.Core;
using DungeonDefense.DevTools;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DungeonDefense.EditorTools
{
    /// <summary>
    /// Creates the Phase 0 scenes and registers them in Build Settings with Boot first.
    /// Safe to re-run: existing scenes are left untouched.
    /// </summary>
    public static class ProjectSetup
    {
        private const string SceneFolder = "Assets/_Project/Scenes";

        [MenuItem("Dungeon Defense/Setup/Create Core Scenes")]
        public static void CreateCoreScenes()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }

            Directory.CreateDirectory(SceneFolder);

            foreach (var sceneName in SceneNames.All)
            {
                var path = ScenePath(sceneName);
                if (File.Exists(path))
                {
                    continue;
                }

                EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

                if (sceneName == SceneNames.Boot)
                {
                    var managerObject = new GameObject(nameof(GameManager));
                    managerObject.AddComponent<GameManager>();
                    managerObject.AddComponent<GameStateDebugPanel>();
                }

                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);
            }

            EditorBuildSettings.scenes = SceneNames.All
                .Select(name => new EditorBuildSettingsScene(ScenePath(name), true))
                .ToArray();

            EditorSceneManager.OpenScene(ScenePath(SceneNames.Boot));
            Debug.Log("[ProjectSetup] Core scenes created and added to Build Settings.");
        }

        public static string ScenePath(string sceneName)
        {
            return $"{SceneFolder}/{sceneName}.unity";
        }
    }

    /// <summary>
    /// Makes Play Mode always start from the Boot scene so the GameManager exists,
    /// whichever scene is open in the editor.
    /// </summary>
    [InitializeOnLoad]
    public static class BootScenePlayModeStarter
    {
        static BootScenePlayModeStarter()
        {
            EditorSceneManager.playModeStartScene =
                AssetDatabase.LoadAssetAtPath<SceneAsset>(ProjectSetup.ScenePath(SceneNames.Boot));
        }
    }
}

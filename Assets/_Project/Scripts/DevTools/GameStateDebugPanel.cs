using DungeonDefense.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonDefense.DevTools
{
    /// <summary>
    /// Development-only overlay: shows the current state and scene, with one button per allowed
    /// transition. Disabled automatically in release builds. Not part of the game UI.
    /// </summary>
    public sealed class GameStateDebugPanel : MonoBehaviour
    {
        private const float ReferenceHeight = 720f;
        private const float ButtonHeight = 40f;

        private void Awake()
        {
            if (!Debug.isDebugBuild)
            {
                enabled = false;
            }
        }

        private void OnGUI()
        {
            var manager = GameManager.Instance;
            if (manager == null)
            {
                return;
            }

            // Scale so the panel stays readable on high-DPI phone screens.
            float scale = Screen.height / ReferenceHeight;
            GUI.matrix = Matrix4x4.Scale(new Vector3(scale, scale, 1f));

            GUILayout.BeginArea(new Rect(10f, 10f, 260f, ReferenceHeight - 20f), GUI.skin.box);
            GUILayout.Label($"State: {manager.CurrentState}");
            GUILayout.Label($"Scene: {SceneManager.GetActiveScene().name}");
            GUILayout.Label($"Time Scale: {Time.timeScale}");

            foreach (var next in manager.StateMachine.AllowedTransitions)
            {
                if (GUILayout.Button($"-> {next}", GUILayout.Height(ButtonHeight)))
                {
                    manager.ChangeState(next);
                }
            }

            GUILayout.EndArea();
        }
    }
}

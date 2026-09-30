using UnityEngine;

namespace DungeonDefense.Core
{
    /// <summary>
    /// Persistent entry point created in the Boot scene. Owns the game state machine and reacts to
    /// state changes (scene loading, time scale). Gameplay systems do not belong here.
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameStateMachine StateMachine { get; private set; }

        public GameState CurrentState => StateMachine.Current;

        // Keeps the singleton correct when "Enter Play Mode Options" disables domain reload.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            Instance = null;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            StateMachine = new GameStateMachine(GameState.Boot);
            StateMachine.StateChanged += OnStateChanged;
        }

        private void Start()
        {
            ChangeState(GameState.MainMenu);
        }

        private void OnDestroy()
        {
            if (Instance != this)
            {
                return;
            }

            StateMachine.StateChanged -= OnStateChanged;
            Instance = null;
        }

        public bool ChangeState(GameState next)
        {
            if (StateMachine.TryTransitionTo(next))
            {
                return true;
            }

            Debug.LogWarning($"[GameManager] Transition not allowed: {StateMachine.Current} -> {next}");
            return false;
        }

        private void OnStateChanged(GameState from, GameState to)
        {
            Debug.Log($"[GameManager] {from} -> {to}");

            Time.timeScale = to == GameState.Pause ? 0f : 1f;

            if (SceneLoader.TryGetSceneFor(to, out var sceneName))
            {
                SceneLoader.Load(sceneName);
            }
        }
    }
}

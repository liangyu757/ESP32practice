using System;
using System.Collections.Generic;

namespace DungeonDefense.Core
{
    /// <summary>
    /// Plain C# state machine for <see cref="GameState"/>. Has no Unity dependency so it can be unit tested.
    /// </summary>
    public sealed class GameStateMachine
    {
        // Single source of truth for which state may follow which.
        private static readonly Dictionary<GameState, GameState[]> Transitions = new Dictionary<GameState, GameState[]>
        {
            { GameState.Boot,     new[] { GameState.MainMenu } },
            { GameState.MainMenu, new[] { GameState.Town } },
            { GameState.Town,     new[] { GameState.Battle, GameState.MainMenu } },
            { GameState.Battle,   new[] { GameState.Reward, GameState.Pause, GameState.GameOver } },
            { GameState.Pause,    new[] { GameState.Battle, GameState.Town } },
            { GameState.Reward,   new[] { GameState.Town } },
            { GameState.GameOver, new[] { GameState.Town, GameState.MainMenu } },
        };

        /// <summary>Raised after a successful transition. Arguments: (from, to).</summary>
        public event Action<GameState, GameState> StateChanged;

        public GameState Current { get; private set; }

        public GameStateMachine(GameState initialState = GameState.Boot)
        {
            Current = initialState;
        }

        public IReadOnlyList<GameState> AllowedTransitions =>
            Transitions.TryGetValue(Current, out var next) ? next : Array.Empty<GameState>();

        public bool CanTransitionTo(GameState next)
        {
            return Transitions.TryGetValue(Current, out var allowed) && Array.IndexOf(allowed, next) >= 0;
        }

        /// <summary>Changes state if the transition is allowed. Returns false (and changes nothing) otherwise.</summary>
        public bool TryTransitionTo(GameState next)
        {
            if (!CanTransitionTo(next))
            {
                return false;
            }

            var previous = Current;
            Current = next;
            StateChanged?.Invoke(previous, next);
            return true;
        }
    }
}

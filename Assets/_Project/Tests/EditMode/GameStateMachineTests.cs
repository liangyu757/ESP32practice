using System;
using System.Collections.Generic;
using DungeonDefense.Core;
using NUnit.Framework;

namespace DungeonDefense.Tests
{
    public class GameStateMachineTests
    {
        private static GameStateMachine MachineAt(params GameState[] path)
        {
            var machine = new GameStateMachine();
            foreach (var state in path)
            {
                Assert.IsTrue(machine.TryTransitionTo(state), $"Setup failed at {machine.Current} -> {state}");
            }
            return machine;
        }

        [Test]
        public void StartsInBoot()
        {
            Assert.AreEqual(GameState.Boot, new GameStateMachine().Current);
        }

        [Test]
        public void CoreLoop_TownBattleRewardTown_Succeeds()
        {
            var machine = MachineAt(GameState.MainMenu, GameState.Town, GameState.Battle, GameState.Reward, GameState.Town);
            Assert.AreEqual(GameState.Town, machine.Current);
        }

        [Test]
        public void PauseAndResume_ReturnsToBattle()
        {
            var machine = MachineAt(GameState.MainMenu, GameState.Town, GameState.Battle, GameState.Pause);
            Assert.IsTrue(machine.TryTransitionTo(GameState.Battle));
            Assert.AreEqual(GameState.Battle, machine.Current);
        }

        [Test]
        public void GameOver_CanReturnToTown()
        {
            var machine = MachineAt(GameState.MainMenu, GameState.Town, GameState.Battle, GameState.GameOver);
            Assert.IsTrue(machine.TryTransitionTo(GameState.Town));
        }

        [Test]
        public void InvalidTransition_IsRejectedAndStateUnchanged()
        {
            var machine = new GameStateMachine();
            var raised = false;
            machine.StateChanged += (_, __) => raised = true;

            Assert.IsFalse(machine.TryTransitionTo(GameState.Battle));
            Assert.AreEqual(GameState.Boot, machine.Current);
            Assert.IsFalse(raised);
        }

        [Test]
        public void SelfTransition_IsRejected()
        {
            var machine = MachineAt(GameState.MainMenu, GameState.Town);
            Assert.IsFalse(machine.TryTransitionTo(GameState.Town));
        }

        [Test]
        public void StateChanged_ReportsFromAndTo()
        {
            var machine = new GameStateMachine();
            var events = new List<(GameState from, GameState to)>();
            machine.StateChanged += (from, to) => events.Add((from, to));

            machine.TryTransitionTo(GameState.MainMenu);

            CollectionAssert.AreEqual(new[] { (GameState.Boot, GameState.MainMenu) }, events);
        }

        [Test]
        public void EveryState_HasAtLeastOneExit()
        {
            // Guards against adding a GameState and forgetting its transitions (a dead end).
            foreach (GameState state in Enum.GetValues(typeof(GameState)))
            {
                Assert.IsNotEmpty(new GameStateMachine(state).AllowedTransitions, $"{state} has no transitions");
            }
        }
    }
}

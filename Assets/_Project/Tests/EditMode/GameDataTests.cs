using DungeonDefense.Data;
using NUnit.Framework;
using UnityEngine;

namespace DungeonDefense.Tests
{
    public class GameDataTests
    {
        [Test]
        public void AllDataTypes_CanBeCreated()
        {
            AssertCreatable<HeroData>();
            AssertCreatable<TowerData>();
            AssertCreatable<EnemyData>();
            AssertCreatable<SkillData>();
            AssertCreatable<MapData>();
        }

        [Test]
        public void HeroData_SkillsDefaultsToEmpty()
        {
            var hero = ScriptableObject.CreateInstance<HeroData>();
            Assert.IsNotNull(hero.Skills);
            Assert.IsEmpty(hero.Skills);
            Object.DestroyImmediate(hero);
        }

        private static void AssertCreatable<T>() where T : GameData
        {
            var data = ScriptableObject.CreateInstance<T>();
            Assert.IsNotNull(data, typeof(T).Name);
            Object.DestroyImmediate(data);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace DungeonDefense.Data
{
    [CreateAssetMenu(menuName = "Dungeon Defense/Hero Data", fileName = "HeroData_New")]
    public sealed class HeroData : GameData
    {
        [field: SerializeField, Min(0f)] public float BaseHP { get; private set; }
        [field: SerializeField, Min(0f)] public float BaseAttack { get; private set; }
        [field: SerializeField, Min(0f), Tooltip("Attacks per second.")] public float AttackSpeed { get; private set; }
        [field: SerializeField, Min(0f)] public float MoveSpeed { get; private set; }

        [SerializeField] private List<SkillData> skills = new List<SkillData>();
        public IReadOnlyList<SkillData> Skills => skills;
    }
}

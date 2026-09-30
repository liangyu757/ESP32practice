using UnityEngine;

namespace DungeonDefense.Data
{
    /// <summary>
    /// Active hero skill data. Effects are implemented in a later phase.
    /// </summary>
    [CreateAssetMenu(menuName = "Dungeon Defense/Skill Data", fileName = "SkillData_New")]
    public sealed class SkillData : GameData
    {
        [field: SerializeField, Min(0f), Tooltip("Cooldown in seconds.")] public float Cooldown { get; private set; }
    }
}

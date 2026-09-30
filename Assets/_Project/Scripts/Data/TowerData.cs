using UnityEngine;

namespace DungeonDefense.Data
{
    [CreateAssetMenu(menuName = "Dungeon Defense/Tower Data", fileName = "TowerData_New")]
    public sealed class TowerData : GameData
    {
        [field: SerializeField] public TowerType TowerType { get; private set; }
        [field: SerializeField, Min(0), Tooltip("Gold cost to build.")] public int Cost { get; private set; }
        [field: SerializeField, Min(0f)] public float Damage { get; private set; }
        [field: SerializeField, Min(0f), Tooltip("Attacks per second.")] public float AttackSpeed { get; private set; }
        [field: SerializeField, Min(0f), Tooltip("Attack range in world units.")] public float Range { get; private set; }
    }
}

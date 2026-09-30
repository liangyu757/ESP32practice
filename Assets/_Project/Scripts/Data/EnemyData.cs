using UnityEngine;

namespace DungeonDefense.Data
{
    [CreateAssetMenu(menuName = "Dungeon Defense/Enemy Data", fileName = "EnemyData_New")]
    public sealed class EnemyData : GameData
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField, Min(0f)] public float MaxHP { get; private set; }
        [field: SerializeField, Min(0f)] public float MoveSpeed { get; private set; }
        [field: SerializeField, Min(0), Tooltip("Gold granted on death.")] public int Reward { get; private set; }
    }
}

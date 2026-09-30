using UnityEngine;

namespace DungeonDefense.Data
{
    /// <summary>
    /// Shared identity fields for all game data assets. Data assets hold values only; behaviour lives
    /// in controller components that read them. Values are read-only at runtime so shared assets are
    /// never modified during play.
    /// </summary>
    public abstract class GameData : ScriptableObject
    {
        [field: SerializeField, Tooltip("Unique, stable ID used by code and save data.")]
        public string Id { get; private set; }

        [field: SerializeField]
        public string DisplayName { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }
    }
}

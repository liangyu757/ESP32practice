using UnityEngine;

namespace DungeonDefense.Data
{
    /// <summary>
    /// Battle map data. How the layout is referenced (prefab or scene), path and waves are
    /// added in Phase 1 when the first map is built.
    /// </summary>
    [CreateAssetMenu(menuName = "Dungeon Defense/Map Data", fileName = "MapData_New")]
    public sealed class MapData : GameData
    {
    }
}

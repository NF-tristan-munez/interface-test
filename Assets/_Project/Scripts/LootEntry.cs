using UnityEngine;

namespace _Project.Scripts
{
    [System.Serializable]
    public struct LootEntry
    {
        public GameObject Prefab;
        [Range(0f, 1f)] public float DropChance;
        public int MinAmount;
        public int MaxAmount;
    }
}
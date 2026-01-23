using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Breakable
{
    public class BreakableObjectWithLoot : BreakableObject
    {
        [TabGroup("Loot Drop")] [SerializeField] private LootEntry[] _lootTable;
        [TabGroup("Loot Drop")] [SerializeField] private Transform _lootSpawnPoint;
        [TabGroup("Loot Drop")] [SerializeField] private float _spawnRadius = 3f;

        protected override void OnBreak()
        {
            SpawnLoot();
            base.OnBreak();
        }
        
        private void SpawnLoot()
        {
            if (_lootTable == null || _lootTable.Length == 0)
                return;

            foreach (var loot in _lootTable)
            {
                if (Random.value > loot.DropChance)
                    continue;

                int amount = Random.Range(loot.MinAmount, loot.MaxAmount + 1);

                for (int i = 0; i < amount; i++)
                {
                    Vector3 offset = Random.insideUnitSphere * _spawnRadius;
                    offset.y = 0.5f;
                    
                    Vector3 start = _lootSpawnPoint.position;
                    Vector3 end = start + offset;
                    
                    GameObject lootDrop = Instantiate(
                        loot.Prefab,
                        start,
                        Quaternion.identity);
                    
                    LootFlyOut flyOut = lootDrop.AddComponent<LootFlyOut>();
                    flyOut.Play(start, end);
                }
            }
        }
    }
}
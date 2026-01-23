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
        [TabGroup("Loot Drop")] [SerializeField] private float _spawnRadius = 0.75f;

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
                    Vector3 spawnPos = Random.insideUnitSphere * _spawnRadius;
                    spawnPos.y = 0.5f;
                    
                    Instantiate(
                        loot.Prefab,
                        _lootSpawnPoint.position + spawnPos,
                        Quaternion.identity);
                }
            }
        }
    }
}
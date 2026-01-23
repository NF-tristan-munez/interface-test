using UnityEngine;

namespace _Project.Scripts.Gameplay.Collectible
{
    public abstract class CollectibleEffect : ScriptableObject
    {
        public abstract void Apply(GameObject collector);
    }
}
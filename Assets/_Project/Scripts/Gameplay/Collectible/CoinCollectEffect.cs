using _Project.Scripts.Gameplay.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Gameplay.Collectible
{
    [CreateAssetMenu(
        fileName = "Coin Collect",
        menuName = "ScriptableObjects/Collectibles/Coin"
    )]
    public class CoinCollectEffect : CollectibleEffect
    {
        [SerializeField] private Currency _currency;
        [SerializeField] private int _amount = 1;

        public override void Apply(GameObject collector)
        {
            if (collector.TryGetComponent(out PlayerInventory inventory))
                return;
            
            Debug.Log($"Adding {_amount} coin to inventory");
            inventory.Add(_currency, _amount);
        }
    }
}
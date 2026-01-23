using System.Collections.Generic;
using _Project.Scripts.Gameplay.Collectible;
using Sirenix.OdinInspector;
using UniRx;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerInventory : MonoExt
    {
        private readonly Dictionary<Currency, int> _balance = new();

        public readonly Subject<(Currency currency, int amount)> OnCurrencyChange = new();
                
        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            OnSubscriptionSet();
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }
    
        public override void OnSubscriptionSet()
        {
            base.OnSubscriptionSet();
        }

        public int GetBalance(Currency currency)
        {
            return _balance.TryGetValue(currency, out var value)
                ? value
                : 0;
        }

        public void Add(Currency currency, int amount)
        {
            if (!_balance.ContainsKey(currency))
                _balance[currency] = 0;
            
            _balance[currency] += amount;
            OnCurrencyChange.OnNext((currency, _balance[currency]));
        }

        public bool Spend(Currency currency, int amount)
        {
            if (GetBalance(currency) < amount) return false;
            
            _balance[currency] -= amount;
            OnCurrencyChange.OnNext((currency, _balance[currency]));
            return true;
        }
    }
}
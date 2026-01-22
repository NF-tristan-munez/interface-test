using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Collectible
{
    public class BaseCollectible : MonoExt, ICollectible
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            OnSubscriptionSet();
        }

        protected virtual void OnCollect()
        {
            
        }

        public void Collect()
        {
            if (!)
        }
    }
}
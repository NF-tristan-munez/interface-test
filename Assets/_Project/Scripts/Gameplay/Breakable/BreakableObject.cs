using UnityEngine;

namespace _Project.Scripts.Gameplay.Breakable
{
    public class BreakableObject : MonoExt, IBreakable
    {
        [SerializeField] private GameObject _breakableObject;
        
        private void Awake()
        {
            //Initialize mono extension
            Initialize();
        }
        private void Start()
        {
            //Events
            OnSubscriptionSet();
        }

        protected virtual void OnBreak()
        {
            _breakableObject.SetActive(false);
        }
        
        public void Break()
        {
            OnBreak();
        }
    }
}
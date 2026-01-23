using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Collectible
{
    [RequireComponent(typeof(SphereCollider))]
    public class BaseCollectible : MonoExt, ICollectible
    {
        [TabGroup("Pickup")] [SerializeField] private LayerMask _collectorLayer;
        [TabGroup("Pickup")] [SerializeField] private float _pickupRadius = 2f;
        [TabGroup("Pickup")] [SerializeField] private float _collectDistance = 0.25f;
        
        [TabGroup("Magnet Effect")] [SerializeField] private float _magnetSpeed = 8f;
        [TabGroup("Magnet Effect")] [SerializeField] private float _acceleration = 12f;
        
        [TabGroup("Animation")] [SerializeField] private float _floatAmplitude = 0.2f;
        [TabGroup("Animation")] [SerializeField] private float _floatFrequency = 2f;
        [TabGroup("Animation")] [SerializeField] private Vector3 spinAxis = Vector3.up;
        [TabGroup("Animation")] [SerializeField] private float idleSpinSpeed = 45f;
        [TabGroup("Animation")] [SerializeField] private float magnetSpinSpeed = 180f;

        [TabGroup("Effect")] [SerializeField] private CollectibleEffect _effect;
        
        private Transform _target;
        private SphereCollider _trigger;
        
        private Vector3 _startPosition;
        private float _currentSpeed;

        private bool _isAttracted;

        private void Awake()
        {
            Initialize();
            
            _startPosition = transform.position;
            
            _trigger = GetComponent<SphereCollider>();
            _trigger.isTrigger = true;
            _trigger.radius = _pickupRadius;
        }

        private void Start()
        {
            OnSubscriptionSet();
        }

        private void Update()
        {
            HandleFloating();
            HandleSpin();

            if (_isAttracted && _target != null)
                HandleMagnet();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    
        public override void OnSubscriptionSet()
        {
            base.OnSubscriptionSet();
        }

        private void HandleFloating()
        {
            if (_isAttracted)
                return;
            
            float yOffset =
                Mathf.Sin(Time.time * _floatFrequency) * _floatAmplitude;
            
            transform.position = _startPosition + new Vector3(0, yOffset, 0);
        }
        
        private void HandleSpin()
        {
            float spinSpeed = _isAttracted
                ? magnetSpinSpeed
                : idleSpinSpeed;

            transform.Rotate(
                spinAxis,
                spinSpeed * Time.deltaTime,
                Space.World
            );
        }

        private void HandleMagnet()
        {
            _currentSpeed = Mathf.MoveTowards(
                _currentSpeed,
                _magnetSpeed,
                Time.deltaTime * _acceleration
            );
            
            transform.position = Vector3.MoveTowards(
                transform.position,
                _target.position,
                _currentSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, _target.position) <= _collectDistance)
            {
                Collect(_target.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isAttracted)
                return;
            
            if (((1 << other.gameObject.layer) & _collectorLayer) == 0)
                return;

            _target = other.transform;
            _isAttracted = true;
        }
        
        public void Collect(GameObject collector)
        {
            if(_effect != null)
                _effect.Apply(collector);
            
            Dispose();
            Destroy(gameObject);
        }
    }
}
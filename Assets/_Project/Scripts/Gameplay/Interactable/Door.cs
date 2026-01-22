using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Interactable
{
    public class Door : MonoExt, IInteractable
    {
        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _isInteractable;
        [SerializeField] private GameObject _door;
        
        [SerializeField] private float _openDoorRotation;
        [SerializeField] private float _closeDoorRotation;
        [SerializeField] private float _openDoorTime;
        
        
        private void Awake()
        {
            Initialize();
            SnapToInitialState();
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

        private async UniTask OpenDoor()
        {
            _isInteractable = false;
            float remainingTime = 0;
            float rotationAlpha;
            float currentRotation;
            
            while (remainingTime < _openDoorTime)
            {
                remainingTime += Time.deltaTime;
                rotationAlpha = remainingTime / _openDoorTime;
                currentRotation = Mathf.Lerp(_closeDoorRotation, _openDoorRotation, rotationAlpha);
                _door.transform.localRotation = Quaternion.Euler(0, currentRotation, 0);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            _isOpen = true;
            _isInteractable = true;
        }

        private async UniTask CloseDoor()
        {
            _isInteractable = false;
            float remainingTime = 0;
            float rotationAlpha;
            float currentRotation;
            
            while (remainingTime < _openDoorTime)
            {
                remainingTime += Time.deltaTime;
                rotationAlpha = remainingTime / _openDoorTime;
                currentRotation = Mathf.Lerp(_openDoorRotation, _closeDoorRotation, rotationAlpha);
                _door.transform.localRotation = Quaternion.Euler(0, currentRotation, 0);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            _isOpen = false;
            _isInteractable = true;
        }

        public void ToggleDoor()
        {
            if(_isOpen)
                CloseDoor().Forget();
            else
                OpenDoor().Forget();
        }
        
        private void SnapToInitialState()
        {
            float rotation = _isOpen ? _openDoorRotation : _closeDoorRotation;
            _door.transform.localRotation = Quaternion.Euler(0, rotation, 0);
            _isInteractable = true;
        }

        public void Interact(GameObject interactor)
        {
            Debug.Log($"Interact | isOpen={_isOpen} isInteractable={_isInteractable}");
            
            if (!_isInteractable)
                return;
            
            ToggleDoor();
        }
    }
}
using System;
using _Project.Scripts.Gameplay.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : BaseCharacterController, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Camera _camera;

    [TabGroup("Character Type")] [SerializeField] private Archetype _archetype;
    [TabGroup("Stats")] [SerializeField] private float _interactRange = 4f;
    
    private Vector2 _movementInput = Vector2.zero;
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
    
    public override void Initialize()
    {
        base.Initialize();
        _playerInput.EnablePlayerActions();
        _archetype.AbilityList.InitializeAbilities();
        _archetype.AbilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Ability, OnAbilityCast);
        AddEvent(_playerInput.Interact, _ => TryInteract());
    }

    public void FixedUpdate()
    {
        HandleMovement();
    }

    //Handles movement and rotation
    private void HandleMovement()
    {
        if (!_canCharacterMove)
            return;
        
        Vector3 normalizedDirection = Utility.CalculateCameraDirection(_camera, _movementInput);
        
        Rotate(normalizedDirection, _movementStats);
        Move(normalizedDirection, _movementStats);
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
       _archetype. AbilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _archetype.AbilityParameterHandler);
    }

    private void TryInteract()
    {
        if (!_canCharacterMove)
            return;
        
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * _interactRange, Color.red, 1f);

        if (Physics.Raycast(
                transform.position + Vector3.up,
                transform.forward,
                out RaycastHit hit,
                _interactRange))
        {
            if (hit.collider.GetComponentInParent<IInteractable>() is IInteractable interactable)
            {
                interactable.Interact(gameObject);
            }
        }
    }
}
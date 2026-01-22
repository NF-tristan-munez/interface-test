using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : BaseCharacterController, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Camera _camera;

    [TabGroup("Ability")] [SerializeField] private AbilityList _abilityList;
    [TabGroup("Ability")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    
    
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
        _abilityList.InitializeAbilities();
        _abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Ability, OnAbilityCast);
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
        _abilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _abilityParameterHandler);
    }
}
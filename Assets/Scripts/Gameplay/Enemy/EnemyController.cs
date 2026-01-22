using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyController : BaseCharacterController, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private EnemyAI _enemyAI;

    [TabGroup("CharacterType")] [SerializeField] private EnemyType _enemyType;
    
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
        _enemyType._abilityList.InitializeAbilities();
        _enemyType._abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_enemyAI.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_enemyAI.Ability, OnAbilityCast);
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
        
        if (_movementInput.sqrMagnitude < 0.01f)
            return;
        
        Rotate(_movementInput, _movementStats);
        Move(_movementInput, _movementStats);
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        Debug.Log($"Enemy cast ability: {abilityEnum.name}");
        _enemyType._abilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _enemyType._abilityParameterHandler);
    }
}
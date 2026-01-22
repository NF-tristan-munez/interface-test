using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : BaseCharacterController, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private EnemyAI _enemyAI;
    
    [TabGroup("CharacterType")] [SerializeField] private Archetype _archetype;
    
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
        _archetype.AbilityList.InitializeAbilities();
        _archetype.AbilityParameterHandler.Initialize();
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
        _archetype.AbilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _archetype.AbilityParameterHandler);
    }
}
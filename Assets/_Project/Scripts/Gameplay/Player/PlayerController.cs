using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Interactable;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : BaseCharacterController, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Animator _animator;
    [TabGroup("References")] [SerializeField] private Camera _camera;

    [TabGroup("Character Type")] [SerializeField] private Archetype _archetype;
    [TabGroup("Debug")] [SerializeField] private float _interactRange = 4f;
    
    // State
    private StateMachine _stateMachine;
    public PlayerState PlayerState {get; private set;}
    
    private bool _ability1Requested;
    private bool _ability2Requested;
    private bool _interactRequested;
    
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
        
        SetupStateMachine();
    }

    private void SetupStateMachine()
    {
        // State Machine
        _stateMachine = new StateMachine();

        // Declaring States
        var idleState = new PlayerIdleState(this, _animator);
        var moveState = new PlayerMoveState(this, _animator);
        var ability1State = new PlayerAbility1State(this, _animator);
        var ability2State = new PlayerAbility2State(this, _animator);
        var interactState = new PlayerInteractState(this, _animator);
        
        // Define Transitions
        // _stateMachine.AddTransition(
        //     idleState,
        //     moveState,
        //     new FuncPredicate(() => _movementInput != Vector2.zero)
        // );
        //
        // _stateMachine.AddTransition(
        //     moveState,
        //     idleState,
        //     new FuncPredicate(() => _movementInput == Vector2.zero)
        // );
        
        // Movement
        At(idleState, moveState, new FuncPredicate(() => _movementInput != Vector2.zero));
        At(moveState, idleState, new FuncPredicate(() => _movementInput == Vector2.zero));

        // Abilities (interrupt anything)
        Any(ability1State, new FuncPredicate(() => _ability1Requested));
        Any(ability2State, new FuncPredicate(() => _ability2Requested));
        Any(interactState, new FuncPredicate(() => _interactRequested));
        
        At(
            ability1State,
            idleState,
            new FuncPredicate(() => IsAnimationFinished())
        );
        At(
            ability2State,
            idleState,
            new FuncPredicate(() => IsAnimationFinished())
        );
        At(
            interactState,
            idleState,
            new FuncPredicate(() => IsAnimationFinished()));
        
        // Set Initial State
        _stateMachine.SetState(idleState);
    }
    
    private bool IsAnimationFinished()
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1f && !_animator.IsInTransition(0);
    }

     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Ability, OnAbilityCast);
        AddEvent(_playerInput.Interact, _ => TryInteract());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _ability1Requested = true;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _ability2Requested = true;
        if (Input.GetKeyDown(KeyCode.F))
            _interactRequested = true;

        _stateMachine?.Update();

        // Clear requests AFTER state machine reads them
        _ability1Requested = false;
        _ability2Requested = false;
        _interactRequested = false;
    }

    public void FixedUpdate()
    {
        _stateMachine?.Update();
        HandleMovement();
    }

    //Handles movement and rotation
    public void HandleMovement()
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

    public void TryInteract()
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
    
    public void LockMovement()
    {
        _canCharacterMove = false;
    }

    public void UnlockMovement()
    {
        _canCharacterMove = true;
    }

    
    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
}
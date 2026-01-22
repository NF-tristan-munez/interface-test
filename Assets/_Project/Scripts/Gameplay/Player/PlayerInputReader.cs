using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObjects/Player/InputReader")]
public class PlayerInputReader : SerializedScriptableObject, InputSystem_Actions.IPlayerActions
{
    [SerializeField] [OdinSerialize] private Dictionary<int, AbilityExtendableEnum> AbilityDictionary = new Dictionary<int, AbilityExtendableEnum>();
    
    public Subject<Vector2> Movement {get; private set;}
    public Subject<AbilityExtendableEnum> Ability {get; private set;}

    private InputSystem_Actions _inputActions;

    private void OnEnable()
    {
        if (_inputActions != null)
            return;

        Initialize();
    }

    private void Initialize()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);
        
        Movement = new Subject<Vector2>();
        Ability = new Subject<AbilityExtendableEnum>();
    }

    public void EnablePlayerActions()
    {
        _inputActions.Enable();
        _inputActions.Player.Enable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Movement.OnNext(context.ReadValue<Vector2>());
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        if(context.performed)
            Ability.OnNext(AbilityDictionary[1]);
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        if(context.performed)
            Ability.OnNext(AbilityDictionary[2]);
    }

    public void OnAbility3(InputAction.CallbackContext context)
    {
        if(context.performed)
            Ability.OnNext(AbilityDictionary[3]);
    }
    
    public void OnAbility4(InputAction.CallbackContext context)
    {
        if(context.performed)
            Ability.OnNext(AbilityDictionary[4]);
    }

}


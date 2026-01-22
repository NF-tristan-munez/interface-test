using System;
using UniRx;
using UnityEngine;

public class EnemyAI : MonoExt
{
    public Subject<Vector3> Movement { get; private set; }
    public Subject<AbilityExtendableEnum> Ability { get; private set; }

    [SerializeField] private Transform target;
    [SerializeField] private AbilityExtendableEnum ability;
    [SerializeField] private float abilityTriggerDistance = 3f;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();

        Movement = new Subject<Vector3>();
        Ability = new Subject<AbilityExtendableEnum>();
    }
    
    private void FixedUpdate()
    {
        EmitMovement();
        EmitAbility();
    }
    
    private void EmitMovement()
    {
        if (Movement == null)
            return;

        if (target == null)
        {
            Movement.OnNext(Vector3.zero);
            return;
        }

        Vector3 direction =
            (target.position - transform.position).normalized;

        Movement.OnNext(direction);
    }

    private void EmitAbility()
    {
        if (Ability == null || target == null || ability == null)
            return;

        if (Vector3.Distance(transform.position, target.position) <= abilityTriggerDistance)
        {
            Ability.OnNext(ability);
        }
    }

    public override void Dispose()
    {
        Movement?.OnCompleted();
        Movement?.Dispose();
        Ability?.OnCompleted();
        Ability?.Dispose();

        base.Dispose();
    }
}
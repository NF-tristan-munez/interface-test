using System;
using UniRx;
using UnityEngine;

public class EnemyAI : MonoExt
{
    public readonly Subject<Vector3> Movement = new();
    public readonly Subject<AbilityExtendableEnum> Ability = new();

    [SerializeField] private Transform target;
    
    [SerializeField] private AbilityExtendableEnum ability;

    private void FixedUpdate()
    {
        if (target == null)
        {
            Movement.OnNext(Vector3.zero);
            return;
        }
        
        Vector3 direction =
            (target.position - transform.position).normalized;
        
        Movement.OnNext(direction);

        if (Vector3.Distance(transform.position, target.position) < 3f)
            Ability.OnNext(ability);
    }
}
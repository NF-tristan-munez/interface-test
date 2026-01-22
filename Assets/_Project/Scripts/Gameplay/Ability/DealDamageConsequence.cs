using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deal Damage Consequence", menuName = "ScriptableObjects/Ability/Deal Damage Consequence")]
public class DealDamageConsequence : Consequence
{
    public Stat Damage;
    
    public AbilityParameterExtendableEnum EnemyListParameterKey;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        List<GameObject> targetList = abilityParameters.GetParameter<List<GameObject>>(EnemyListParameterKey);
        if (targetList.Count <= 0)
        {
            abilityParameters.RemoveParameter(EnemyListParameterKey);
            return;
        }

        foreach (GameObject target in targetList.ToList())
        {
            if (!target.TryGetComponent<Health>(out var targetHealth))
            {
                targetList.Remove(target);
                continue;
            }
            targetHealth.ApplyDamage(Damage.Value);
        }
    }
}
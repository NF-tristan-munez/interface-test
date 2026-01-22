using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Apply Healing Consequence", menuName = "ScriptableObjects/Ability/Apply Healing Consequence")]
public class ApplyHealingConsequence : Consequence
{
    public Stat Healing;
    
    public AbilityParameterExtendableEnum AllyListParameterKey;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        List<GameObject> targetList = abilityParameters.GetParameter<List<GameObject>>(AllyListParameterKey);
        if (targetList.Count <= 0)
        {
            abilityParameters.RemoveParameter(AllyListParameterKey);
            return;
        }

        foreach (GameObject target in targetList.ToList())
        {
            if (!target.TryGetComponent<Health>(out var targetHealth))
            {
                targetList.Remove(target);
                continue;
            }
            targetHealth.ApplyHealing(Healing.Value);
        }
    }
}
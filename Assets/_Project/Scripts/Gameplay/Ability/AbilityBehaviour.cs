using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Behaviour", menuName = "ScriptableObjects/Ability/New Ability Behaviour")]
public class AbilityBehaviour : SerializedScriptableObject
{
    public List<AbilityPhase> Phases;

    public async UniTask ExecuteBehaviour(AbilityParameterHandler abilityParameters)
    {
        foreach (var phase in Phases)
        {
            await phase.ExecutePhase(abilityParameters);
        }
    }
}
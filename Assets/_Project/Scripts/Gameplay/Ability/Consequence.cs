using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Consequence : SerializedScriptableObject
{ 
    public Consequence NextConsequence;

    public virtual async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
    }

    protected async UniTask ExecuteNextConsequence(AbilityParameterHandler abilityParameters)
    {
        if (NextConsequence == null)
            return;
        
        await NextConsequence.ExecuteConsequence(abilityParameters);
    }
}
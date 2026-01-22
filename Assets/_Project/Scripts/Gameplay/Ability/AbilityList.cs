using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability List", menuName = "ScriptableObjects/Ability/Ability List")]
public class AbilityList : SerializedScriptableObject
{
    [SerializeField][OdinSerialize] public Dictionary<AbilityExtendableEnum, Ability> AbilityDictionary = new Dictionary<AbilityExtendableEnum, Ability>();

    public void InitializeAbilities()
    {
        foreach (var (abilityType, ability) in AbilityDictionary)
        {
            ability.ResetCooldown();
        }
    }
}

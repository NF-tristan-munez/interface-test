using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IActivateable
{
    UniTask OnTriggerAbility(GameObject obj, AbilityParameterHandler abilityParameters);
}
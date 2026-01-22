using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="EnemyType", menuName = "ScriptableObjects/CharacterType/EnemyType")]
[Serializable]
public class EnemyType : SerializedScriptableObject
{
    [TabGroup("Ability")] [SerializeField] public AbilityList _abilityList;
    [TabGroup("Ability")] [SerializeField] public AbilityParameterHandler _abilityParameterHandler;
    
    [TabGroup("Stats")]
    [SerializeField] public Health health;
    
    [TabGroup("Identifier")]
    [SerializeField] public string _className;
    [SerializeField] public string _classID;
}
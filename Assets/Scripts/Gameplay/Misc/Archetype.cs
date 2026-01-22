using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName="Archetype", menuName = "ScriptableObjects/Misc/Archetype")]
[Serializable]
public class Archetype : SerializedScriptableObject
{
    [TabGroup("Ability")] [SerializeField] public AbilityList AbilityList;
    [TabGroup("Ability")] [SerializeField] public AbilityParameterHandler AbilityParameterHandler;
    
    [TabGroup("Stats")]
    [SerializeField] public Stat MaxHealth;
    
    [TabGroup("Identifier")] [SerializeField] public string ArchetypeName;
    [TabGroup("Identifier")] [SerializeField] public ArchetypeExtendableEnum ArchetypeEnum;
}
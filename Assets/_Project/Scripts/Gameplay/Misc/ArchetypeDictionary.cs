using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ArchetypeDictionary", menuName = "ScriptableObjects/Misc/ArchetypeDictionary")]
public class BaseArchetypeDictionary : SerializedScriptableObject
{
    public Dictionary<ArchetypeExtendableEnum, Archetype> ArchetypeDictionary = new Dictionary<ArchetypeExtendableEnum, Archetype>();
}
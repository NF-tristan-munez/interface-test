using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Sphere Overlap Consequence", menuName = "ScriptableObjects/Ability/Sphere Overlap Consequence")]
public class SphereOverlapConsequence : Consequence
{
    public Stat Radius;
    public Stat FrontOffset;
    
    public AbilityParameterExtendableEnum CenterParameterKey;
    public AbilityParameterExtendableEnum TargetListParameterKey;
    public AbilityParameterExtendableEnum TargetTag;
    
    public bool IsVisualized = false;
    [ShowIf("IsVisualized")]
    public SphereOverlapConsequenceVisualizer VisualizerPrefab;
    
    public override async UniTask ExecuteConsequence(AbilityParameterHandler abilityParameters)
    {
        Transform centerTransform = abilityParameters.GetParameter<GameObject>(CenterParameterKey).transform;
        
        Vector3 center = centerTransform.position + centerTransform.forward * FrontOffset.Value;
        Quaternion boxRotation = Quaternion.LookRotation(centerTransform.forward);
        
        if(IsVisualized)
            SpawnVisualizer(center, Radius.Value, boxRotation);
        
        Collider[] colliders = Physics.OverlapSphere(center, Radius.Value);
        List<GameObject> targets = new();
        
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag(TargetTag.name))
                continue;
            
            targets.Add(collider.gameObject);
        }
        
        abilityParameters.SetParameter(TargetListParameterKey, targets.ToList());
        await ExecuteNextConsequence(abilityParameters);
    }
    
    public void SpawnVisualizer( Vector3 center, float radius, Quaternion boxRotation)
    {
        SphereOverlapConsequenceVisualizer visualizer = Instantiate(VisualizerPrefab, center, boxRotation);
        visualizer.Initialize(center, radius, boxRotation);
    }
}
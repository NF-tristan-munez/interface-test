using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Phase", menuName = "ScriptableObjects/Ability/New Ability Phase")]
public class AbilityPhase: SerializedScriptableObject
{
    public Stat Duration;
    [OdinSerialize] public Dictionary<float,List<Consequence>> ConsequenceTimingDictionary = new Dictionary<float,List<Consequence>>();

    public async UniTask ExecutePhase(AbilityParameterHandler abilityParameters)
    {
        float timeElapsed = 0f;
        List<float> timings = new List<float>(ConsequenceTimingDictionary.Keys);
        timings.Sort();
        
        HashSet<float> triggeredTimings = new HashSet<float>();

        while (timeElapsed < Duration.Value)
        {
            float normalizedTime = timeElapsed / Duration.Value;

            foreach (float timing in timings)
            {
                if (triggeredTimings.Contains(timing) || normalizedTime < timing)
                    continue;

                triggeredTimings.Add(timing);
                    
                foreach (var consequence in ConsequenceTimingDictionary[timing])
                {
                    consequence.ExecuteConsequence(abilityParameters).Forget();
                }
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
            timeElapsed += Time.deltaTime;
        }
    }
}
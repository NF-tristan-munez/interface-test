using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoExt
{
    [TabGroup("UI")] [SerializeField][OdinSerialize] public Dictionary<AbilityExtendableEnum, Image> CooldownImageDictionary = new Dictionary<AbilityExtendableEnum, Image>();
    [TabGroup("UI")] [SerializeField] private GameObject _errorText;
    [TabGroup("References")] [SerializeField] private AbilityList _abilityList;
    [TabGroup("References")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
        _abilityParameterHandler.AbilityStarted = new Subject<AbilityExtendableEnum>();
        _abilityParameterHandler.AbilityStillExecuting = new Subject<bool>();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        AddEvent(_abilityParameterHandler.AbilityStarted, OnAbilityStarted);
        AddEvent(_abilityParameterHandler.AbilityStillExecuting, OnAbilityStillExecuting);
    }

    private void OnAbilityStillExecuting(bool isAbilityStillExecuting)
    {
        _errorText.gameObject.SetActive(isAbilityStillExecuting);
    }

    private void OnAbilityStarted(AbilityExtendableEnum abilityEnum)
    {
        StartCooldownUI(abilityEnum).Forget();
    }

    private async UniTask StartCooldownUI(AbilityExtendableEnum abilityEnum)
    {
        Image image = CooldownImageDictionary[abilityEnum];
        image.gameObject.SetActive(true);

        Ability ability = _abilityList.AbilityDictionary[abilityEnum];

        image.fillAmount = 1f;
        while (ability.GetNormalizedRemainingTime() >= 0)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, ability.GetNormalizedRemainingTime(), 0.1f);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        image.gameObject.SetActive(false);
    }
}

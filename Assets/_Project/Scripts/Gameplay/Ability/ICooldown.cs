using Cysharp.Threading.Tasks;

public interface ICooldown
{
    UniTask RunCooldownTimer(float duration);
    void ResetCooldown();
    float GetNormalizedRemainingTime();
}
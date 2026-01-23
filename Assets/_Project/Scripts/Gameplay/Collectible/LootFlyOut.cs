using System.Collections;
using UnityEngine;

public class LootFlyOut : MonoExt
{
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private float height = 0.5f;
    [SerializeField] private AnimationCurve easing =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        OnSubscriptionSet();
    }
        
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }
    
    public void Play(Vector3 start, Vector3 end)
    {
        StopAllCoroutines();
        StartCoroutine(FlyRoutine(start, end));
    }

    private IEnumerator FlyRoutine(Vector3 start, Vector3 end)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = easing.Evaluate(time / duration);

            // Horizontal interpolation
            Vector3 pos = Vector3.Lerp(start, end, t);

            // Vertical arc
            pos.y += Mathf.Sin(t * Mathf.PI) * height;

            transform.position = pos;
            yield return null;
        }

        transform.position = end;
    }
}
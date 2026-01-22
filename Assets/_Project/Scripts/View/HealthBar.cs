using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoExt
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothTime = 0.5f;
    private void LateUpdate()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _health.HP / _health.MaxHP, _smoothTime * Time.deltaTime);
    }
}

using _Project.Scripts.Gameplay.Breakable;
using UnityEngine;

public class ObjectHealth : Health
{
    private IBreakable _breakable;

    protected void Awake()
    {
        base.Awake();
        _breakable = GetComponent<IBreakable>();
    }

    protected override void OnDeath()
    {
        if (_breakable != null)
            _breakable.Break();
    }
}
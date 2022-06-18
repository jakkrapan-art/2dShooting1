using UnityEngine;

public enum StatusEffectTriggerType
{
    OnAttacked = 0,
    OnDamageTaken = 1,
    OnHealed = 2,
    OnDied = 3,
    EveryTime = 4,
    OnApplied = 5,
    OnRemoved = 6,
}

public enum StatusEffectType
{
    Instant = 0,
    Duration = 1
}

[System.Serializable]
public class StatusEffect
{
    protected StatusEffectType _effectType;
    public StatusEffectType GetEffectType => _effectType;

    protected StatusEffectTriggerType _triggerType;
    public StatusEffectTriggerType GetTriggerType => _triggerType;

    protected IStatusEffectAppliable _target;
    protected GameObject _targetAppliedStatus;

    protected float _appliedTime;
    protected float _duration;

    public StatusEffect(float duration)
    {
        _duration = duration;
    }

    public void SetAppliedTarget(IStatusEffectAppliable target)
    {
        _target = target;
        _targetAppliedStatus = target.GetGameObject;
        _appliedTime = Time.time;
    }

    public void CheckStatusEffectTime()
    {
        if (Time.time - _appliedTime >= _duration)
        {
            _target.RemoveStatusEffect(this);
        }
    }

    public virtual void Effect()
    {

    }
}

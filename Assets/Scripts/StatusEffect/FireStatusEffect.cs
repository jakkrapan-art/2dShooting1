using UnityEngine;

[System.Serializable]
public class FireStatusEffect : StatusEffect
{
    [SerializeField] private int _damage = 0;
    [SerializeField] private float _lastTriggerTime = 0f;
    [SerializeField] private float _damageInterval = 0f;


    public FireStatusEffect(int damageAmount, float duration, float damageInterval) : base(duration)
    {
        _damage = damageAmount;
        _damageInterval = damageInterval;
        _triggerType = StatusEffectTriggerType.EveryTime;
        _effectType = StatusEffectType.Duration;
    }
    public FireStatusEffect(FireStatusEffect template) : base(template._duration)
    {
        _damage = template._damage;
        _damageInterval = template._damageInterval;
        _triggerType = template._triggerType;
        _effectType = template._effectType;
    }

    public override void Execute()
    {
        base.Execute();
        if (Time.time - _lastTriggerTime >= _damageInterval)
        {
            _lastTriggerTime = Time.time;
            try
            {
                var entity = _targetAppliedStatus.GetComponent<Entity>();

                if (entity)
                {
                    entity.TakeDamage(_damage);
                }
            }
            catch { }
        }
    }
}

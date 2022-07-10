using UnityEngine;

public class RegenerationEffect : StatusEffect
{
    private int _healAmount = 0;
    private float _lastTriggerTime = 0f;
    private float _healInterval = 0f;

    public RegenerationEffect(int healAmount, float duration, float healInterval) : base(duration)
    {
        _healAmount = healAmount;
        _healInterval = healAmount;
        _triggerType = StatusEffectTriggerType.EveryTime;
        _effectType = StatusEffectType.Duration;
    }

    public override void Execute()
    {
        base.Execute();

        if (Time.time - _lastTriggerTime >= _healInterval)
        {
            _lastTriggerTime = Time.time;
            try
            {
                var entity = _targetAppliedStatus.GetComponent<Entity>();

                if (entity)
                {
                    entity.Heal(_healAmount);
                }
            }
            catch { }
        }
    }
}

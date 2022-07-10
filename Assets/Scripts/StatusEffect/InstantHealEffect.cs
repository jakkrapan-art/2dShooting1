using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealEffect : StatusEffect
{
  private int _healAmount;

  public InstantHealEffect(int healAmount, float duration) : base(duration)
  {
    _healAmount = healAmount;
    _triggerType = StatusEffectTriggerType.OnApplied;
    _effectType = StatusEffectType.Instant;
  }

  public override void Execute()
  {
    base.Execute();

    var entity = _targetAppliedStatus?.GetComponent<Entity>();

    if (entity)
    {
      entity.Heal(_healAmount);
    }

    _appliedTarget.RemoveStatusEffect(this);
  }
}

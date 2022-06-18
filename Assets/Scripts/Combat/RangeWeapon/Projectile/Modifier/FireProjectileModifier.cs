using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireProjectileModifier : ProjectileModifier
{
  private FireStatusEffect _statusEffect;

  private int _damagePerTick;
  private float _duration;
  private float _damageInterval;

  public FireProjectileModifier(int damagePerTick, float duration, float damageInterval)
  {
    _triggerType = ProjectileModifierTriggerType.OnDamageDealt;
    _statusEffect = new FireStatusEffect(damagePerTick, duration, damageInterval);

    _damagePerTick = damagePerTick;
    _duration = duration;
    _damageInterval = damageInterval;
  }

  public override ProjectileModifier CreateInstance()
  {
    return new FireProjectileModifier(_damagePerTick, _duration, _damageInterval);
  }

  public override void Execute()
  {
    Collider2D coll2D = _projectile.transform.GetComponent<Collider2D>();
    List<Collider2D> hittedTarget = new List<Collider2D>();
    coll2D.OverlapCollider(new ContactFilter2D(), hittedTarget);

    foreach (var hit in hittedTarget)
    {
      var statusEffectAppliable = hit.GetComponent<IStatusEffectAppliable>();
      if (statusEffectAppliable != null)
      {
        statusEffectAppliable.ApplyStatusEffect(new FireStatusEffect(_statusEffect));
      }
    }
  }
}

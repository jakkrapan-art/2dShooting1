using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileModifierTriggerType
{
  OnHittedTarget = 0,
  OnDamageDealt = 1,
  EveryTime = 2,
}

[System.Serializable]
public class ProjectileModifier
{
  protected Projectile _projectile;

  protected ProjectileModifierTriggerType _triggerType;
  public ProjectileModifierTriggerType GetTriggerType => _triggerType;

  public void ApplyTo(Projectile projectile)
  {
    projectile.AddModifier(this);
    _projectile = projectile;
  }

  public virtual ProjectileModifier CreateInstance()
  {
    return new ProjectileModifier();
  }

  public void Remove()
  {
    _projectile.RemoveModifier(this);
  }

  public virtual void Execute() { }
}

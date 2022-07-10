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
  private string _description;
  protected ProjectileModifierTriggerType _triggerType;
  public string GetDescription => _description;
  public ProjectileModifierTriggerType GetTriggerType => _triggerType;

  protected void SetDescription(string description)
  {
    _description = $"<color=#ffd429>{description}</color>";
  }

  public void SetAppliedProjectile(Projectile projectile)
  {
    _projectile = projectile;
  }

  public virtual ProjectileModifier CreateInstance()
  {
    return new ProjectileModifier();
  }

  public virtual void SetupModifyValue(float modifyValue)
  {

  }

  public virtual void SetupModifyValue(float minModifyValue, float maxModifyValue)
  {

  }

  public void Remove()
  {
    _projectile.RemoveModifier(this);
  }

  public virtual void Execute() { }
}

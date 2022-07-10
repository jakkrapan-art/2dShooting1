using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponModifier : WeaponModifier
{
  private ProjectileModifier[] _projectileModifiers;

  public ProjectileModifier[] GetProjectileModifiers => _projectileModifiers;

  public override void Setup()
  {
    base.Setup();
  }
}

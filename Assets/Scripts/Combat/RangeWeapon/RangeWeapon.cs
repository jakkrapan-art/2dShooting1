using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class RangeWeapon : MonoBehaviour
{
  private IAttackable holder = null;
  [SerializeField] private Projectile _projectile = null;
  [SerializeField] private Transform _firePoint = null;

  private Dictionary<string, IStat> _stats = new Dictionary<string, IStat>();
  private List<StatModifier> _statModifiers = new List<StatModifier>();
  private List<ProjectileModifier> _projectileModifiers = new List<ProjectileModifier>();

  private float _lastShootTime = 0f;
  private void Awake()
  {
    InitialStatDict();
  }

  void Update()
  {
    if (Input.GetMouseButton(0) && Time.time - _lastShootTime >= _stats[RangeWeaponStatNames.FIRE_RATE].GetValue)
    {
      Shoot();
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      //AddStatModifier(new StatModifier(RangeWeaponStatNames.BULLET_COUNT, 2, StatModifierType.Flat));
      AddProjecttileModifier(new BounceProjectile(2));
    }
    else if (Input.GetKeyDown(KeyCode.T))
    {
      if (_statModifiers.Count > 0)
      {
        //RemoveStatModifier(_statModifiers[0]);
        RemoveProjectileModifier(_projectileModifiers[0]);
      }
    }
  }

  private void InitialStatDict()
  {
    var stats = RangeWeaponStats.GenerateAllRangeWeaponStats(0.5f, 5, 100);

    foreach (var stat in stats)
    {
      _stats.Add(stat.GetStatName, stat);
    }
  }

  public void AddStatModifier(StatModifier modifier)
  {
    if (modifier == null) return;

    _statModifiers.Add(modifier);
    if (_stats.ContainsKey(modifier.GetStatName))
    {
      _stats[modifier.GetStatName].AddModifier(modifier);
    }
  }

  public void RemoveStatModifier(StatModifier modifier)
  {
    if (!_statModifiers.Contains(modifier)) return;

    _statModifiers.Remove(modifier);
    _stats[modifier.GetStatName].RemoveModifier(modifier);
  }

  public void AddProjecttileModifier(ProjectileModifier modifier)
  {
    if (modifier == null) return;

    _projectileModifiers.Add(modifier);
  }

  public void RemoveProjectileModifier(ProjectileModifier modifier)
  {
    if (modifier == null || !_projectileModifiers.Contains(modifier)) return;

    _projectileModifiers.Remove(modifier);
  }

  private void Shoot()
  {
    var bulletCount = _stats[RangeWeaponStatNames.BULLET_COUNT].GetValue;
    _lastShootTime = Time.time;

    for (int i = 0; i < bulletCount; i++)
    {
      Vector2 spawnPos = _firePoint.position;
      int maxOffset = 70;
      int minOffset = 15;
      int offset = minOffset + i;
      var accuracy = _stats[RangeWeaponStatNames.ACCURACY].GetValue;
      var rotationOffset = offset - (minOffset * (accuracy) / 100f);
      float minRotation = _firePoint.rotation.z - Mathf.Clamp(rotationOffset, 0, maxOffset);
      float maxRotation = _firePoint.rotation.z + Mathf.Clamp(rotationOffset, 0, maxOffset);

      Quaternion bulletRotation;

      int randomedRotation = (int)Random.Range(minRotation, maxRotation);
      bulletRotation = _firePoint.rotation * Quaternion.Euler(0, 0, randomedRotation);

      var instantiatedProjectile = Instantiate(_projectile, spawnPos, bulletRotation);
      instantiatedProjectile.InitialSetup(holder, new AttackDamage(100, 0, Vector2.zero), 350, _projectileModifiers.ToArray());
    }
  }
}

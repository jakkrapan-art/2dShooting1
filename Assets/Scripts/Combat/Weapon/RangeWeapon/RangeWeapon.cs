using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class RangeWeapon : Weapon
{
  [SerializeField] private Projectile _projectile = null;
  [SerializeField] private Transform _firePoint = null;

  private List<ProjectileModifier> _projectileModifiers = new List<ProjectileModifier>();

  private int currentAmmo = 0;
  private bool reloading = false;

  private float _lastShootTime = 0f;

  public RangeWeaponModifier mod = default;
  public override string GetModifyDesciption
  {
    get
    {
      string description = base.GetModifyDesciption;
      int projectileModsCount = _projectileModifiers.Count;

      if (projectileModsCount > 0)
      {
        description += "\n";
        for (int i = 0; i < projectileModsCount; i++)
        {
          ProjectileModifier mod = _projectileModifiers[i];
          description += mod.GetDescription;
          if (i < projectileModsCount - 1) description += "\n";
        }
      }

      return description;
    }
  }

  protected override void Awake()
  {
    base.Awake();

    currentAmmo = (int)_weaponStats[WeaponStats.AMMO_CAP].GetValue;
  }

  protected override void Update()
  {
    base.Update();

    if (Input.GetKeyDown(KeyCode.R))
    {
      Reload();
    }
    else if (Input.GetKeyDown(KeyCode.I))
    {
      EquipModifier(0, mod, ()=> Debug.Log("Equip mod"));
    }
    else if (Input.GetKeyDown(KeyCode.O))
    {
      UnequipModifier(0, () => Debug.Log("Unequip mod"));
    }
    else if (Input.GetKeyDown(KeyCode.P))
    {
      Debug.Log("Modify description:\n" + GetModifyDesciption);
    }
    else if (Input.GetKeyDown(KeyCode.M))
    {
      Debug.Log(
        ModifierGenerator.GetRandomStatModifier(_weaponRarity.ToString()).GetDescription
      );
    }
  }

  protected override void InitialStatDict()
  {
    List<Stat> stats = WeaponStats.NewRangeWeaponStats((RangeWeaponData)_weaponData);

    foreach (var s in stats)
    {
      AddStatToDict(s);
    }
  }

  private void AddStatToDict(Stat stat)
  {
    _weaponStats.Add(stat.GetStatName, stat);
  }

  public override bool EquipModifier(int slotIndex, WeaponModifier mod, Action onEquipMod = null)
  {
    if (!mod || (mod is RangeWeaponModifier) == false) return false;

    RangeWeaponModifier rangeMod = mod as RangeWeaponModifier;
    var projectileMods = rangeMod.GetProjectileModifiers;
    foreach (var m in projectileMods)
    {
      AddProjecttileModifier(m);
    }
    onEquipMod?.Invoke();

    return base.EquipModifier(slotIndex, mod);
  }

  public override WeaponModifier UnequipModifier(int slotIndex, Action onUnequipMod = null)
  {
    RangeWeaponModifier mod = _weaponMods[slotIndex] as RangeWeaponModifier;

    foreach (var m in mod.GetProjectileModifiers)
    {
      RemoveProjectileModifier(m);
    }

    return base.UnequipModifier(slotIndex, () =>
    {
      onUnequipMod?.Invoke();
      currentAmmo = (int)_weaponStats[WeaponStats.AMMO_CAP].GetValue;
    });
  }

  public override void AddStatModifier(StatModifier modifier)
  {
    if (modifier == null) return;

    _statMods.Add(modifier);
    if (_weaponStats.ContainsKey(modifier.GetStatName))
    {
      _weaponStats[modifier.GetStatName].AddModifier(modifier);
    }
  }

  public override void RemoveStatModifier(StatModifier modifier)
  {
    if (!_statMods.Contains(modifier)) return;

    _statMods.Remove(modifier);
    _weaponStats[modifier.GetStatName].RemoveModifier(modifier);
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

  public override void Attack()
  {
    if (reloading) return;
    else if (currentAmmo == 0)
    {
      Reload();
      return;
    }
    else if (Time.time - _lastShootTime < _weaponStats[WeaponStats.FIRE_RATE].GetValue) return;

    int bulletCount = (int)_weaponStats[WeaponStats.PROJECTILE_COUNT].GetValue;
    int ammoConsume = (int)_weaponStats[WeaponStats.AMMO_CONSUME].GetValue;

    if (currentAmmo >= ammoConsume)
    {
      currentAmmo -= ammoConsume;
    }
    else
    {
      bulletCount -= ammoConsume - currentAmmo;
      currentAmmo = 0;
    }

    _lastShootTime = Time.time;
    SpawnBullet(bulletCount);
  }

  private void Reload()
  {
    if (reloading) return;
    StartCoroutine(ReloadCoroutine());
  }

  private IEnumerator ReloadCoroutine()
  {
    reloading = true;
    float reloadTime = _weaponStats[WeaponStats.RELOAD_TIME].GetValue;
    int ammoCap = (int)_weaponStats[WeaponStats.AMMO_CAP].GetValue;

    yield return new WaitForSeconds(reloadTime);

    currentAmmo = ammoCap;
    reloading = false;
  }

  protected virtual void SpawnBullet(int bulletCount)
  {
    float acc = _weaponStats[WeaponStats.ACCURACY].GetValue;
    float maxSpread = WeaponStats.MAX_SPREAD;
    float spread = maxSpread - (maxSpread * acc / 100f);

    float minAngle = _firePoint.localRotation.z - (spread / 2);
    float maxAngle = _firePoint.localRotation.z + (spread / 2);

    for (int i = 0; i < bulletCount; i++)
    {
      Vector2 spawnPos = _firePoint.position;
      float angle = Random.Range(minAngle, maxAngle);
      Quaternion bulletRotation;
      bulletRotation = _firePoint.rotation * Quaternion.Euler(0, 0, angle);

      var instantiatedProjectile = Instantiate(_projectile, spawnPos, bulletRotation);
      var projectileSpeed = _weaponStats[WeaponStats.PROJECTILE_SPEED].GetValue;
      instantiatedProjectile.InitialSetup(holder, new AttackDamage(100, 0, Vector2.zero), projectileSpeed, _projectileModifiers.ToArray());
    }
  }
}

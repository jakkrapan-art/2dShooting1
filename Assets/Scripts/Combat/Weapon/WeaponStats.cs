using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponStats
{
  public const float MAX_SPREAD = 80f; // in radial angle

  public const string DAMAGE = "Damage";
  public const string MOD_SLOT = "Mod slot";

  #region Range Weapon Stat Names
  public const string FIRE_RATE = "Fire rate";
  public const string ACCURACY = "Accuracy";
  public const string PROJECTILE_COUNT = "Projectile";
  public const string PROJECTILE_SPEED = "Projectile speed";
  public const string PROJECTILE_DURATION = "Projectile lifetime";
  public const string AMMO_CAP = "Magazine size";
  public const string AMMO_CONSUME = "Ammo consume";
  public const string RELOAD_TIME = "Reload speed";
  #endregion

  private static Dictionary<string, string> StatNamePairs = new Dictionary<string, string>();
  private static Dictionary<string, string> StatDBNamePairs = new Dictionary<string, string>();
  public static string GetStatName(string nameInDB)
  {
    StatNamePairs.TryGetValue(nameInDB, out var result);
    return result;
  }

  public static void AddStatNamePair(string statName, string nameInDB)
  {
    StatNamePairs.Add(nameInDB, statName);
    StatDBNamePairs.Add(statName, nameInDB);
  }

  public static void Setup()
  {
    AddStatNamePair(DAMAGE, Const.DB_STAT_NAME_DAMAGE);
    AddStatNamePair(FIRE_RATE, Const.DB_STAT_NAME_FIRERATE);
    AddStatNamePair(ACCURACY, Const.DB_STAT_NAME_ACC);
    AddStatNamePair(PROJECTILE_COUNT, Const.DB_STAT_NAME_PROJECTILE_COUNT);
    AddStatNamePair(PROJECTILE_SPEED, Const.DB_STAT_NAME_PROJECTILE_SPEED);
    AddStatNamePair(PROJECTILE_DURATION, Const.DB_STAT_NAME_PROJECTILE_DURATION);
    AddStatNamePair(AMMO_CAP, Const.DB_STAT_NAME_AMMO_CAP);
    AddStatNamePair(AMMO_CONSUME, Const.DB_STAT_NAME_AMMO_CONSUME);
    AddStatNamePair(RELOAD_TIME, Const.DB_STAT_NAME_RELOAD_TIME);
  }

  private struct WeaponStatParams
  {
    public int damage;
  }

  public static IntStat NewDamageStat(int baseValue)
  {
    return new IntStat(new Stat.StatData { statName = DAMAGE, baseValue = baseValue, haveMinValue = true, minValue = 0, haveMaxValue = false });
  }

  #region Range Stats New
  public static FireRateStat NewFireRateStat(float baseValue)
  {
    return new FireRateStat(new Stat.StatData { statName = FIRE_RATE, baseValue = baseValue, haveMinValue = true, minValue = 0.25f, haveMaxValue = false });
  }

  public static IntStat NewProjectileCountStat(int baseValue)
  {
    return new IntStat(new Stat.StatData { statName = PROJECTILE_COUNT, baseValue = baseValue, haveMinValue = true, minValue = 1, haveMaxValue = false });
  }

  public static DecimalStat NewAccuracyStat(float baseValue)
  {
    return new DecimalStat(new Stat.StatData { statName = ACCURACY, baseValue = baseValue, haveMinValue = true, minValue = 0, haveMaxValue = true, maxValue = 100 });
  }

  public static DecimalStat NewProjectileSpeedStat(float baseValue)
  {
    return new DecimalStat(new Stat.StatData { statName = PROJECTILE_SPEED, baseValue = baseValue, haveMinValue = true, minValue = 200, haveMaxValue = false });
  }

  public static DecimalStat NewProjectileDurationStat(float baseValue)
  {
    return new DecimalStat(new Stat.StatData { statName = PROJECTILE_DURATION, baseValue = baseValue, haveMinValue = true, minValue = 0.25f, haveMaxValue = false });
  }

  public static IntStat NewAmmoCapacityStat(int baseValue)
  {
    return new IntStat(new Stat.StatData { statName = AMMO_CAP, baseValue = baseValue, haveMinValue = true, minValue = 1, haveMaxValue = false });
  }
  public static DecimalStat NewReloadSpeedStat(float baseValue)
  {
    return new DecimalStat(new Stat.StatData { statName = RELOAD_TIME, baseValue = baseValue, haveMinValue = false, haveMaxValue = false });
  }

  public static IntStat NewAmmoConsumeStat(int baseValue)
  {
    return new IntStat(new Stat.StatData { statName = AMMO_CONSUME, baseValue = baseValue, haveMinValue = true, minValue = 0, haveMaxValue = false });
  }
  #endregion

  private static List<Stat> NewWeaponStats(WeaponStatParams weaponStatParams)
  {
    List<Stat> stats = new List<Stat>();

    stats.Add(NewDamageStat(weaponStatParams.damage));

    return stats;
  }

  public static List<Stat> NewRangeWeaponStats(RangeWeaponData data)
  {
    List<Stat> stats = NewWeaponStats(new WeaponStatParams
    {
      damage = data.damage
    });

    stats.Add(NewFireRateStat(data.fireRate));
    stats.Add(NewProjectileCountStat(data.bulletCount));
    stats.Add(NewProjectileSpeedStat(data.bulletSpeed));
    stats.Add(NewProjectileDurationStat(data.ammoConsume));
    stats.Add(NewAccuracyStat(data.accuracy));
    stats.Add(NewAmmoCapacityStat(data.ammoCapacity));
    stats.Add(NewAmmoConsumeStat(data.ammoConsume));
    stats.Add(NewReloadSpeedStat(data.reloadTime));

    return stats;
  }
}

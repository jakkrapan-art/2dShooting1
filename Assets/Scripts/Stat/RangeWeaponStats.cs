using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RangeWeaponStats
{
  public static DecimalStat GenerateFireRateStat(float baseValue)
  {
    return new DecimalStat(RangeWeaponStatNames.FIRE_RATE, baseValue);
  }

  public static IntStat GenerateBulletCountStat(int baseValue)
  {
    return new IntStat(RangeWeaponStatNames.BULLET_COUNT, baseValue);
  }

  public static IntStat GenerateAccuracyStat(int baseValue)
  {
    return new IntStat(RangeWeaponStatNames.ACCURACY, baseValue, true, 100);
  }

  public static List<IStat> GenerateAllRangeWeaponStats(float fireRateBaseValue, int bulletCountBaseValue,int accuracyBaseValue)
  {
    List<IStat> stats = new List<IStat>();

    var bulletCountStat = GenerateBulletCountStat(bulletCountBaseValue);
    stats.Add(bulletCountStat);

    var fireRateStat = GenerateFireRateStat(fireRateBaseValue);
    stats.Add(fireRateStat);

    var accuracyStat = GenerateAccuracyStat(accuracyBaseValue);
    stats.Add(accuracyStat);

    return stats;
  }
}

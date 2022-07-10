using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{
  //Hex color code
  public static readonly string HEX_NEGATIVE_STATUS = "#ff4848";
  public static readonly string HEX_POSITIVE_STATUS = "#2FFF27";

  //Database
  public static readonly string DATABASE_PREFIX = "Assets/Resources/Databases/";

  //Modifier
  public static readonly string DB_STAT_CATEGORY_WEAPON = "weapon-modifier";
  public static readonly string DB_STAT_CATEGORY_RANGE_WEAPON = "range-weapon-modifier";
  public static readonly string DB_STAT_CATEGORY_MELEE_WEAPON = "melee-weapon-modifier";

  public static readonly string DB_STAT_NAME_DAMAGE = "damage";
  //-Range
  public static readonly string DB_STAT_NAME_FIRERATE = "fire-rate";
  public static readonly string DB_STAT_NAME_PROJECTILE_COUNT = "projectile-count";
  public static readonly string DB_STAT_NAME_PROJECTILE_SPEED = "projectile-speed";
  public static readonly string DB_STAT_NAME_PROJECTILE_DURATION = "projectile-duration";
  public static readonly string DB_STAT_NAME_ACC = "accuracy";
  public static readonly string DB_STAT_NAME_AMMO_CAP = "ammo-capacity";
  public static readonly string DB_STAT_NAME_AMMO_CONSUME = "ammo-consume";
  public static readonly string DB_STAT_NAME_RELOAD_TIME = "reload-time";

  //Rarity
  public static readonly string RARITY_COMMON = "common";
  public static readonly string RARITY_UNCOMMON = "uncommon";
  public static readonly string RARITY_RARE = "rare";
  public static readonly string RARITY_EPIC = "epic";
  public static readonly string RARITY_LEGENDARY = "legendary";

}

//enums
public enum Rarity
{
  Common = 0,
  Uncommon = 1,
  Rare = 2,
  Epic = 3,
  Legendary = 4,
}

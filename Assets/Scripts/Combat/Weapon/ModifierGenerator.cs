using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModifierGenerator
{
  private static Dictionary<string ,Dictionary<string,ModifierData.StatData>> _modifierDataDictionary;

  private static bool IsDataLoaded => _modifierDataDictionary != null;

  public static StatModifier GetRandomStatModifier(string rarity)
  {
    CheckData();

    int randomIndex = Random.Range(0, _modifierDataDictionary.Count);
    var statDatas = _modifierDataDictionary.ElementAt(randomIndex).Value;
    randomIndex = Random.Range(0, statDatas.Count);
    var stat = statDatas.ElementAt(randomIndex).Value;

    Rarity modRarity = Utility.ParseToEnum<Rarity>(rarity);
    string statName = WeaponStats.GetStatName(stat.statName);

    ModifierData.ModifyRange range = stat.values[(int)modRarity].valueRange;
    int modifyValue = Random.Range(range.min,range.max);

    if(statName == WeaponStats.RELOAD_TIME)
      return new ReloadTimeModifier(statName, modifyValue);
    else
      return new StatModifier(statName, modifyValue);
  }

  private static void LoadData()
  {
    var dataSet = Utility.GetObjectFromJson<ModifierDataSet>("stat-modifier.json");
    _modifierDataDictionary = new Dictionary<string, Dictionary<string,ModifierData.StatData>>();
    var statsDict = new Dictionary<string, ModifierData.StatData>();

    foreach (var mod in dataSet.modifiers)
    {
      foreach (var stat in mod.stats)
      {
        statsDict.Add(stat.statName, stat);
      }

      _modifierDataDictionary.Add(mod.categoryName, statsDict);
    }
  }

  private static void CheckData()
  {
    if (!IsDataLoaded)
    {
      LoadData();
    }
  }
}

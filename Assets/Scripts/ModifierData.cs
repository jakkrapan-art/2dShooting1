using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Scriptable/Stat Modifier/Data")]
public class ModifierData: ScriptableObject
{
  [Serializable]
  public struct ModifyValue
  {
    public string rarity;
    public ModifyRange valueRange;
  }

  [Serializable]
  public struct ModifyRange
  {
    public int min;
    public int max;
  }

  [Serializable]
  public struct StatData
  {
    public string statName;
    public List<ModifyValue> values;
  }

  [SerializeField]
  public string categoryName;
  [SerializeField]
  public List<StatData> stats;
}

[Serializable]
public class ModifierDataSet
{
  [SerializeField]
  public List<ModifierData> modifiers;
}

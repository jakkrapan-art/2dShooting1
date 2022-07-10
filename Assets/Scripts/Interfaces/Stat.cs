using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat
{
  public struct StatData 
  {
    public string statName;
    public float baseValue;
    public bool haveMinValue;
    public float minValue;
    public bool haveMaxValue;
    public float maxValue;
  }

  public abstract string GetStatName { get; }
  public abstract float GetValue { get; }
  public abstract float GetBaseValue { get; }

  public abstract void AddModifier(StatModifier modifier);

  public abstract void RemoveModifier(StatModifier modifier);
}

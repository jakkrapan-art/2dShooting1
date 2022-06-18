using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntStat : IStat
{
  private string _statName = "";
  private int _baseValue = 0;
  private int _modifiedValue = 0;

  private bool _isHaveMaxValue = false;
  private float _maxValue = 0;

  private List<StatModifier> _modifiers = new List<StatModifier>();

  public IntStat(string statName, int baseValue, bool haveMaxValue = false, float maxValue = 0)
  {
    _statName = statName;
    _baseValue = baseValue;
    _modifiedValue = _baseValue;

    _isHaveMaxValue = haveMaxValue;
    _maxValue = maxValue;
  }

  public string GetStatName => _statName;
  public float GetValue => _modifiedValue;
  public float GetBaseValue => _baseValue;

  public void AddModifier(StatModifier modifier)
  {
    _modifiers.Add(modifier);
    UpdateValue();
  }

  public void RemoveModifier(StatModifier modifier)
  {
    _modifiers.Remove(modifier);
    UpdateValue();
  }

  private void UpdateValue()
  {
    float totalPercentModifyValue = 0;
    int totalFlatModifyValue = 0;

    foreach (var mod in _modifiers)
    {
      switch (mod.GetModifierType)
      {
        case StatModifierType.Flat:
          totalFlatModifyValue += mod.GetModifyValue;
          break;
        case StatModifierType.Percent:
          totalPercentModifyValue += mod.GetModifyValue;
          break;
      }
    }

    _modifiedValue = _baseValue + Mathf.FloorToInt(_baseValue * totalPercentModifyValue / 100);
    _modifiedValue += totalFlatModifyValue;

    if (_isHaveMaxValue) _modifiedValue = (int)Mathf.Clamp(_modifiedValue, 0, _maxValue);
  }
}

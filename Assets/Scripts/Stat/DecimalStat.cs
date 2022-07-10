using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecimalStat : Stat
{
  protected string _statName = "";
  protected float _baseValue = 0;
  protected float _modifiedValue = 0;
  
  protected bool _isHaveMinValue = false;
  protected float _minValue = 0;
  
  protected bool _isHaveMaxValue = false;
  protected float _maxValue = 0;
  
  protected List<StatModifier> _modifiers = new List<StatModifier>();

  public DecimalStat(StatData statData)
  {
    _statName = statData.statName;

    _isHaveMaxValue = statData.haveMaxValue;
    _maxValue = statData.maxValue;

    _isHaveMinValue = statData.haveMinValue;
    _minValue = statData.minValue;

    if (statData.haveMaxValue && statData.haveMinValue)
      _baseValue = Mathf.Clamp(statData.baseValue, statData.minValue, statData.maxValue);
    else if (statData.haveMaxValue)
      _baseValue = Mathf.Clamp(statData.baseValue, float.MinValue, statData.maxValue);
    else if (statData.haveMinValue)
      _baseValue = Mathf.Clamp(statData.baseValue, statData.minValue, float.MaxValue);
    else
      _baseValue = statData.baseValue;

    _modifiedValue = _baseValue;
  }

  public override string GetStatName => _statName;

  public override float GetValue => _modifiedValue;

  public override float GetBaseValue => _baseValue;

  public override void AddModifier(StatModifier modifier)
  {
    _modifiers.Add(modifier);
    UpdateValue();
  }

  public override void RemoveModifier(StatModifier modifier)
  {
    _modifiers.Remove(modifier);
    UpdateValue();
  }

  protected virtual void UpdateValue()
  {
    float totalPercentModifyValue = 0;
    float totalFlatModifyValue = 0;

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

    _modifiedValue = _baseValue + (_baseValue * totalPercentModifyValue / 100);
    _modifiedValue += totalFlatModifyValue;

    if (_isHaveMaxValue && _isHaveMinValue) _modifiedValue = Mathf.Clamp(_modifiedValue, _minValue, _maxValue);
    else if (_isHaveMaxValue) _modifiedValue = Mathf.Clamp(_modifiedValue, float.MinValue, _maxValue);
    else _modifiedValue = Mathf.Clamp(_modifiedValue, _minValue, float.MaxValue);
  }
}

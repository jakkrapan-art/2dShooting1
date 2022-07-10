using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateStat : DecimalStat
{
  public FireRateStat(StatData statData): base(statData)
  {
    _isHaveMaxValue = true;
    _minValue = 0;
  }

  protected override void UpdateValue()
  {
    float totalModifyValue = 0;

    foreach (var mod in _modifiers)
    {
      totalModifyValue += mod.GetModifyValue;
    }

    _modifiedValue -= totalModifyValue;

    if (_isHaveMaxValue && _isHaveMinValue) _modifiedValue = Mathf.Clamp(_modifiedValue, _minValue, _maxValue);
    else if (_isHaveMaxValue) _modifiedValue = Mathf.Clamp(_modifiedValue, float.MinValue, _maxValue);
    else _modifiedValue = Mathf.Clamp(_modifiedValue, _minValue, float.MaxValue);
  }
}

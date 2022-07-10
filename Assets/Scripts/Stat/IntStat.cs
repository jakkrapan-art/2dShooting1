using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntStat : DecimalStat
{
  public IntStat(StatData statData):base(statData)
  {
    _baseValue = Mathf.RoundToInt(_baseValue);
    _modifiedValue = Mathf.RoundToInt(_modifiedValue);
  }

  protected override void UpdateValue()
  {
    base.UpdateValue();

    _baseValue = Mathf.RoundToInt(_baseValue);
    _modifiedValue = Mathf.RoundToInt(_modifiedValue);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTimeModifier : StatModifier
{
  public ReloadTimeModifier(string statName, float modifyValue, StatModifierType modifierType = StatModifierType.Flat) : base(statName, modifyValue, modifierType)
  {
  }

  public override void SetDescription()
  {
    bool isPositive = _modifyValue < 0;
    string colorCode;
    if (isPositive) colorCode = Const.HEX_POSITIVE_STATUS;
    else colorCode = Const.HEX_NEGATIVE_STATUS;

    _description = $"<color={colorCode}>{_statName} {(isPositive ? "-" : "+")}{Mathf.Abs(_modifyValue)}{(_modifierType == 0 ? "" : "%")}</color>";
  }
}

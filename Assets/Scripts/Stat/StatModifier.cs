using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType
{
  Flat = 0,
  Percent = 1
}

[System.Serializable]
public class StatModifier
{
  protected string _statName;
  protected float _modifyValue;
  protected string _description;
  public string GetDescription => _description;

  public string GetStatName => _statName;
  public float GetModifyValue => _modifyValue;

  protected StatModifierType _modifierType;
  public StatModifierType GetModifierType => _modifierType;

  public StatModifier(string statName, float modifyValue, StatModifierType modifierType = StatModifierType.Flat)
  {
    _statName = statName;
    _modifierType = modifierType;
    _modifyValue = modifyValue;

    SetDescription();
  }

  public virtual void SetDescription()
  {
    bool isPositive = _modifyValue > 0;
    string colorCode;
    if (isPositive) colorCode = Const.HEX_POSITIVE_STATUS;
    else colorCode = Const.HEX_NEGATIVE_STATUS;

    _description = $"<color={colorCode}>{_statName} {(isPositive ? "+" : "-")}{Mathf.Abs(_modifyValue)}{(_modifierType == 0 ? "" : "%")}</color>";
  }

  public static StatModifier NewRandomStatModifier()
  {
    return new StatModifier("", 0);
  }
}

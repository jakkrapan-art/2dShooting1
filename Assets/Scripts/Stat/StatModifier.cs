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
    private string _statName;
    private int _modifyValue;

    public string GetStatName => _statName;
    public int GetModifyValue => _modifyValue;

    private StatModifierType _modifierType;
    public StatModifierType GetModifierType => _modifierType;

    public StatModifier(string statName, int modifyValue, StatModifierType modifierType)
    {
        _statName = statName;
        _modifierType = modifierType;
        _modifyValue = modifyValue;
    }
}

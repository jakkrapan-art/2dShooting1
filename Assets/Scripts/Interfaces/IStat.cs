using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStat
{
    public string GetStatName { get; }
    public float GetValue { get; }
    public float GetBaseValue { get; }

    public void AddModifier(StatModifier modifier);

    public void RemoveModifier(StatModifier modifier);
}

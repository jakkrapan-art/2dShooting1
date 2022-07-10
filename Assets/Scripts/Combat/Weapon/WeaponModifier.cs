using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponModifier : MonoBehaviour, IItem
{
  public Image Icon { get; }
  protected Rarity _rarity;
  protected StatModifier[] _statModifiers;

  public Rarity GetRarity => _rarity;
  public void SetRarity(Rarity rarity) => _rarity = rarity;

  public virtual StatModifier[] GetStatModifiers => _statModifiers;

  protected virtual void Start()
  {
    Setup();
  }

  public virtual void Setup()
  {

  }
}

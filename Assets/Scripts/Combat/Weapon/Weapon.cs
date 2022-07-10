using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
  protected IAttackable holder = null;

  [SerializeField] protected WeaponData _weaponData = default;
  protected Rarity _weaponRarity;

  protected Dictionary<string, Stat> _weaponStats = new Dictionary<string, Stat>();
  protected WeaponModifier[] _weaponMods;
  protected List<StatModifier> _statMods = new List<StatModifier>();

  #region Getter/Setter
  public Rarity GetRarity => _weaponRarity;
  public void SetRarity(Rarity rarity) => _weaponRarity = rarity;
  public virtual string GetModifyDesciption
  {
    get
    {
      string description = "";

      for (int i = 0; i < _statMods.Count; i++)
      {
        StatModifier mod = _statMods[i];
        description += mod.GetDescription;
        if (i < _statMods.Count - 1) description += "\n";
      }

      return description;
    }
  }
  #endregion

  protected virtual void Awake()
  {
    InitialStatDict();

    _weaponMods = new WeaponModifier[_weaponData.modSlot];
  }

  protected virtual void Update()
  {
    if (Input.GetMouseButton(0))
    {
      Attack();
    }
  }

  protected abstract void InitialStatDict();

  public virtual bool EquipModifier(int slotIndex, WeaponModifier mod, Action onEquipMod = null)
  {
    if (mod == null || (slotIndex < 0 || slotIndex >= _weaponMods.Length)) return false;

    _weaponMods[slotIndex] = mod;
    foreach (var statMod in mod.GetStatModifiers)
    {
      AddStatModifier(statMod);
    }

    return true;
  }
  public virtual WeaponModifier UnequipModifier(int slotIndex, Action onUnequipMod = null)
  {
    WeaponModifier mod = _weaponMods[slotIndex];

    foreach (var statMod in mod.GetStatModifiers)
    {
      RemoveStatModifier(statMod);
    }

    _weaponMods[slotIndex] = null;
    onUnequipMod?.Invoke();

    return mod;
  }

  public  virtual void AddStatModifier(StatModifier modifier)
  {
    if (modifier == null) return;

    _statMods.Add(modifier);
    if (_weaponStats.ContainsKey(modifier.GetStatName))
    {
      _weaponStats[modifier.GetStatName].AddModifier(modifier);
    }
  }

  public virtual void RemoveStatModifier(StatModifier modifier)
  {
    if (!_statMods.Contains(modifier)) return;

    _statMods.Remove(modifier);
    _weaponStats[modifier.GetStatName].RemoveModifier(modifier);
  }

  public abstract void Attack();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Range/Data", fileName = "Range Weapon Data")]
public class RangeWeaponData : WeaponData
{
  public float fireRate = 0.5f;
  public int accuracy = 25; // as a percent form
  public int bulletCount = 1;
  public float bulletSpeed = 1000;
  public int ammoConsume = 1;
  public int ammoCapacity = 6;
  public float reloadTime = 1.5f;
}

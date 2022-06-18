using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceProjectile : ProjectileModifier
{
  private int _pieceCount = 0;

  public PieceProjectile(int pieceCount)
  {
    _triggerType = ProjectileModifierTriggerType.OnDamageDealt;
    _pieceCount = pieceCount;
  }
  public override ProjectileModifier CreateInstance()
  {
    return new PieceProjectile(_pieceCount);
  }

  public override void Execute()
  {
    _pieceCount--;

    if (_pieceCount <= 0)
    {
      Remove();
    }
  }
}

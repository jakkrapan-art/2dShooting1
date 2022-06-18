using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage
{
    private int _damageAmount;
    private float _knockBackForce;
    private Vector2 _knockBackDirection;

    public int GetDamageAmount => _damageAmount;
    public float GetKnockBackForce => _knockBackForce;
    public Vector2 GetKnockBackDirection => _knockBackDirection;

    public AttackDamage(int damageAmount, float knockBackForce, Vector2 knockBackDirection)
    {
        _damageAmount = damageAmount;
        _knockBackForce = knockBackForce;
        _knockBackDirection = knockBackDirection;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public void Attack(Vector2 attackerPosition, AttackDamage attackDamage, float hitboxHeight, float hitboxWidth, List<string> attackerEnemyFactions)
    {

    }

    public void AttackWithProjectile(Vector2 attackerPosition, AttackDamage attackDamage, float hitboxHeight, float hitboxWidth, List<string> attackerEnemyFactions, Projectile projectile)
    {
        Attack(attackerPosition, attackDamage, hitboxHeight, hitboxWidth, attackerEnemyFactions);
    }
}

using UnityEngine;
public interface IDamageable
{
    public void GetAttack(AttackDamage attackDamage, IAttackable attacker);
    public void TakeDamage(int damageAmount);
    public void Heal(int healAmount);
}

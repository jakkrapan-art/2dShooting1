using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Entity : MonoBehaviour, IDamageable, IStatusEffectAppliable
{
    protected Rigidbody2D _rigidbody = null;
    public Rigidbody2D GetRigidbody
    {
        get
        {
            if (!_rigidbody)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }

            return _rigidbody;
        }
    }

    [SerializeField] protected EntityDataSO _entityData;
    protected EntityDataSO GetEntityData => _entityData;

    public EntityHealth Health { get; protected set; } = null;

    protected UnityAction OnUpdateEffect;
    protected UnityAction OnStatusEffectApplied;
    protected UnityAction OnGetAttacked;
    protected UnityAction OnDamageTaken;
    protected UnityAction OnHealed;
    protected UnityAction OnDied;

    public IAttackable LastDamageFrom { get; protected set; } = null;

    [SerializeField] protected List<StatusEffect> _appliedStatusEffects = new List<StatusEffect>();
    protected bool _isImmuneStatusEffect = false;

    protected int facingDirection = 1; //-1 = Left, 1 = right

    public GameObject GetGameObject => gameObject;

    protected bool _canMove = true;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Health = GetComponent<EntityHealth>();
        Health.InitialSetUp(_entityData.GetHealthPoint);
    }

    protected virtual void Update()
    {
        OnUpdateEffect?.Invoke();

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            var effect = new FireStatusEffect(1, 10, 0.25f);
            ApplyStatusEffect(effect);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            var effect = new RegenerationEffect(2, 10, 1);
            ApplyStatusEffect(effect);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            var effect = new InstantHealEffect(10, 0);
            ApplyStatusEffect(effect);
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        if (!_canMove)
        {
            return;
        }

        if (velocity.x > 0 && facingDirection < 0)
        {
            Flip();
        }
        else if (velocity.x < 0 && facingDirection > 0)
        {
            Flip();
        }

        _rigidbody.velocity = velocity;
    }

    public void GetAttack(AttackDamage attackDamage, IAttackable attacker)
    {
        TakeDamage(attackDamage.GetDamageAmount);

        KnockBack(attackDamage.GetKnockBackDirection, attackDamage.GetKnockBackForce);

        LastDamageFrom = attacker;
        OnGetAttacked?.Invoke();
    }

    public void TakeDamage(int damageAmount)
    {
        Health.ReduceHealth(damageAmount);
        OnDamageTaken?.Invoke();

        if (Health.CurrentHealth == 0)
        {
            Die();
        }

    }
    public void Heal(int healAmount)
    {
        Health.Heal(healAmount);
        OnHealed?.Invoke();
    }

    protected void KnockBack(Vector2 direction, float knockBackForce)
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        DisableMove();
        Invoke(nameof(EnableMove), 0.65f);
    }

    public void DisableMove()
    {
        _rigidbody.velocity = Vector2.zero;
        _canMove = false;
    }

    public void EnableMove()
    {
        _canMove = true;
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
    }
    private void Die()
    {
        OnDied?.Invoke();
        Destroy(gameObject);
    }
    public virtual void ApplyStatusEffect(StatusEffect statusEffect)
    {
        if (_isImmuneStatusEffect) return;

        _appliedStatusEffects.Add(statusEffect);

        switch (statusEffect.GetTriggerType)
        {
            case StatusEffectTriggerType.OnDamageTaken:
                OnGetAttacked += statusEffect.Effect;
                break;
            case StatusEffectTriggerType.OnHealed:
                OnHealed += statusEffect.Effect;
                break;
            case StatusEffectTriggerType.OnDied:
                OnDied += statusEffect.Effect;
                break;
            case StatusEffectTriggerType.EveryTime:
                OnUpdateEffect += statusEffect.Effect;
                break;
            case StatusEffectTriggerType.OnApplied:
                OnStatusEffectApplied += statusEffect.Effect;
                break;
        }

        statusEffect.SetAppliedTarget(this);

        if (statusEffect.GetEffectType == StatusEffectType.Duration)
            OnUpdateEffect += statusEffect.CheckStatusEffectTime;

        OnStatusEffectApplied?.Invoke();
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        _appliedStatusEffects.Remove(statusEffect);
        switch (statusEffect.GetTriggerType)
        {
            case StatusEffectTriggerType.OnDamageTaken:
                OnGetAttacked -= statusEffect.Effect;
                break;
            case StatusEffectTriggerType.OnHealed:
                OnHealed -= statusEffect.Effect;
                break;
            case StatusEffectTriggerType.OnDied:
                OnDied -= statusEffect.Effect;
                break;
            case StatusEffectTriggerType.EveryTime:
                OnUpdateEffect -= statusEffect.Effect;
                break;
            case StatusEffectTriggerType.OnApplied:
                OnStatusEffectApplied -= statusEffect.Effect;
                break;
        }

        if (statusEffect.GetEffectType == StatusEffectType.Duration)
            OnUpdateEffect -= statusEffect.CheckStatusEffectTime;
    }

    public void EnableStatusEffectImmune()
    {
        _isImmuneStatusEffect = true;
    }

    public void DisableStatusEffectImmune()
    {
        _isImmuneStatusEffect = false;
    }
}

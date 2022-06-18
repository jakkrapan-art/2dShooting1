using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
  private Rigidbody2D _rigidBody;

  private IAttackable _attacker;
  private AttackDamage _projectileDamage;
  private float _lifeTime;
  private float _projectileSpeed;

  //private List<ProjectileModifier> _modifiers = new List<ProjectileModifier>();
  private Dictionary<System.Type, ProjectileModifier> _projectileModifiers = new Dictionary<System.Type, ProjectileModifier>();

  private UnityAction OnUpdate;
  private UnityAction OnHitted;
  private UnityAction OnDamageDealt;

  private Coroutine _disableOnUpdateCoroutine;
  private bool _isOnUpdateEnable = true;

  private void Awake()
  {
    _rigidBody = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    StartCoroutine(CountDownLifetime());
  }

  private void Update()
  {
    if (_isOnUpdateEnable)
    {
      OnUpdate?.Invoke();
    }
  }

  private void FixedUpdate()
  {
    _rigidBody.velocity = transform.up * _projectileSpeed * Time.fixedDeltaTime;
  }

  public void InitialSetup(IAttackable attacker, AttackDamage projectileDamage, float projectileSpeed, float lifeTime, ProjectileModifier[] modifiers = null)
  {
    _projectileDamage = projectileDamage;
    _projectileSpeed = projectileSpeed;
    _lifeTime = lifeTime;

    foreach (var modifier in modifiers)
    {
      var m = modifier.CreateInstance();
      m.ApplyTo(this);
    }
  }

  public void InitialSetup(IAttackable attacker, AttackDamage projectileDamage, float projectileSpeed, ProjectileModifier[] modifiers = null)
  {
    InitialSetup(attacker, projectileDamage, projectileSpeed, 3.5f, modifiers);
  }

  private IEnumerator CountDownLifetime()
  {
    yield return new WaitForSeconds(_lifeTime);
    Destroy(gameObject);
  }

  private void DealDamage(IDamageable target)
  {
    target.TakeDamage(/*_projectileDamage.GetDamageAmount*/0);

    OnDamageDealt?.Invoke();

    if (!_projectileModifiers.ContainsKey(typeof(PieceProjectile)) &&
        !_projectileModifiers.ContainsKey(typeof(BounceProjectile)))
    {
      Destroy(gameObject);
    }
  }

  public void AddModifier(ProjectileModifier modifier)
  {
    if (_projectileModifiers.ContainsKey(modifier.GetType()))
    {
      _projectileModifiers[modifier.GetType()] = modifier;
    }
    else
    {
      _projectileModifiers.Add(modifier.GetType(), modifier);
    }

    switch (modifier.GetTriggerType)
    {
      case ProjectileModifierTriggerType.OnHittedTarget:
        OnHitted += modifier.Execute;
        break;
      case ProjectileModifierTriggerType.OnDamageDealt:
        OnDamageDealt += modifier.Execute;
        break;
      case ProjectileModifierTriggerType.EveryTime:
        OnUpdate += modifier.Execute;
        break;
    }
  }

  public void RemoveModifier(ProjectileModifier modifier)
  {
    if (_projectileModifiers.ContainsKey(modifier.GetType()))
    {
      _projectileModifiers.Remove(modifier.GetType());

      switch (modifier.GetTriggerType)
      {
        case ProjectileModifierTriggerType.OnHittedTarget:
          OnHitted -= modifier.Execute;
          break;
        case ProjectileModifierTriggerType.OnDamageDealt:
          OnDamageDealt -= modifier.Execute;
          break;
        case ProjectileModifierTriggerType.EveryTime:
          OnUpdate -= modifier.Execute;
          break;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.transform.gameObject.GetComponent<IDamageable>() != null)
    {
      DealDamage(collision.transform.gameObject.GetComponent<IDamageable>());
    }
    else
    {
      if (!_projectileModifiers.ContainsKey(typeof(BounceProjectile)))
      {
        Destroy(gameObject);
      }
    }

    OnHitted?.Invoke();

    if (_disableOnUpdateCoroutine != null)
    {
      StopCoroutine(_disableOnUpdateCoroutine);
      _disableOnUpdateCoroutine = null;
    }
    _disableOnUpdateCoroutine = StartCoroutine(DisableOnUpdateAction(0.15f));
  }

  private IEnumerator DisableOnUpdateAction(float second)
  {
    _isOnUpdateEnable = false;
    yield return new WaitForSeconds(second);
    _isOnUpdateEnable = true;
    _disableOnUpdateCoroutine = null;
  }
}

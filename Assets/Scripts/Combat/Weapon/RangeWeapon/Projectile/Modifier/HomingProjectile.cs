using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : ProjectileModifier
{
  private Transform _target;
  private float _targetDetectRadius;
  private float _maxDistance;

  public HomingProjectile(float targetDetectRadius)
  {
    _triggerType = ProjectileModifierTriggerType.EveryTime;
    _targetDetectRadius = targetDetectRadius;
    _maxDistance = _targetDetectRadius * 2;
    SetDescription("Homing projectile");
  }

  public override ProjectileModifier CreateInstance()
  {
    return new HomingProjectile(_targetDetectRadius);
  }

  public override void Execute()
  {
    if (!_target)
    {
      Collider2D[] hits = Physics2D.OverlapCircleAll(_projectile.transform.position + (_projectile.transform.up * _targetDetectRadius), _targetDetectRadius);
      if (hits.Length > 0)
      {
        foreach (var hit in hits)
        {
          if (hit.transform != _projectile.transform && hit.transform.GetComponent<IDamageable>() != null)
          {
            _target = hit.transform;
            break;
          }
        }
      }

      return;
    }
    else
    {
      if (Vector2.Distance(_projectile.transform.position, _target.position) > _maxDistance)
      {
        _target = null;
        return;
      }

      Vector2 direction = (_target.position - _projectile.transform.position).normalized;
      float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
      var newRotation = Quaternion.RotateTowards(_projectile.transform.rotation, Quaternion.Euler(new Vector3(0, 0, targetAngle)), 0.88f);
      _projectile.transform.rotation = newRotation;
    }
  }
}

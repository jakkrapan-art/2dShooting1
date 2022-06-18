using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BounceProjectile : ProjectileModifier
{
    private int _bounceCount = 0;

    public BounceProjectile(int bounceCount)
    {
        _triggerType = ProjectileModifierTriggerType.OnHittedTarget;
        _bounceCount = bounceCount;
    }

  public override ProjectileModifier CreateInstance()
  {
    return new BounceProjectile(_bounceCount);
  }

  public override void Execute()
    {
        _bounceCount--;

        Rigidbody2D rb = _projectile.transform.GetComponent<Rigidbody2D>();

        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(_projectile.transform.position, _projectile.transform.up);
        foreach (var hit in raycastHit2D)
        {
            if (hit.transform != _projectile.transform)
            {
                var direction = Vector3.Reflect(rb.velocity, hit.normal).normalized;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
                break;
            }
        }

        if (_bounceCount <= 0)
        {
            Remove();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
  [SerializeField] private Projectile _projectile;
  [SerializeField] private Transform _firePoint;

  [Space(15f)]
  private float _lastShootTime = 0f;
  private float _fireRate = 0.6f;
  [SerializeField] private int _bulletCount = 1;
  private float _bulletSpawnDistance = 0.35f;

  private Rigidbody2D _rb;
  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    RotateToMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //RotateToMouse((Vector2)transform.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    if (Input.GetMouseButton(0) && Time.time - _lastShootTime >= _fireRate)
    {
      Shoot();
    }
  }

  private void RotateToMouse(Vector2 targetPoint)
  {
    var lookDir = targetPoint - _rb.position;
    var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

    _rb.rotation = angle;
  }

  private void Shoot()
  {
    _lastShootTime = Time.time;

    int[] middle = new int[_bulletCount % 2 == 0 ? 2 : 1];

    if (middle.Length > 1)
    {
      middle[0] = _bulletCount / 2 - 1;
      middle[1] = _bulletCount / 2;
    }
    else if (middle.Length == 1)
    {
      middle[0] = (int)(_bulletCount / 2f + 0.5f);
    }

    for (int i = 0; i < _bulletCount; i++)
    {
      Vector2 spawnPos;
      Quaternion bulletRotation;
      float bulletWidth = _projectile.transform.localScale.x;

      if (_bulletCount % 2 == 0)
      {
        if (i == middle[0])
        {
          spawnPos = _firePoint.position - (_firePoint.transform.right * (bulletWidth / 1.75f));
          bulletRotation = _firePoint.rotation;
        }
        else if (i == middle[1])
        {
          spawnPos = _firePoint.position + (_firePoint.transform.right * (bulletWidth / 1.75f));
          bulletRotation = _firePoint.rotation;
        }
        else
        {
          spawnPos = _firePoint.position;
          if (i < middle[0])
          {
            bulletRotation = _firePoint.rotation * Quaternion.Euler(0, 0, -((i - middle[0]) * 12f));
          }
          else if (i > middle[1])
          {
            bulletRotation = _firePoint.rotation * Quaternion.Euler(0, 0, -((i - middle[1]) * 12f));
          }
          else
          {
            bulletRotation = _firePoint.rotation;
          }
        }
      }
      else
      {
        spawnPos = _firePoint.position;
        bulletRotation = _firePoint.rotation * Quaternion.Euler(0, 0, -((i + 1 - middle[0]) * 12f));
      }

      var instantiatedProjectile = Instantiate(_projectile, spawnPos, bulletRotation);
      instantiatedProjectile.InitialSetup(null, new AttackDamage(100, 0, Vector2.zero), 350);
    }
  }
}

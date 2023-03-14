using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO _weaponDataSO;
    [SerializeField] protected Transform _muzzle;
    [SerializeField] protected Transform _shellEjectPosition;

    public WeaponDataSO WeaponData => _weaponDataSO;

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmon;
    public UnityEvent OnStopShooting;
    protected bool _isShooting;

    private void Awake()
    {
        
    }

    private void Update()
    {
        UseWeapon();
    }

    public void UseWeapon()
    {
        if(_isShooting )
        {
            OnShoot?.Invoke();
            for(int i = 0; i < _weaponDataSO.bulletCount; i++)
            {
                ShootBullet();
            }
        }
    }

    private void ShootBullet()
    {
        Debug.Log("葛贰馆瘤 户具户具");
    }

    public void TryShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }
}

using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour,IDamagerable
{
    public bool IsEnemy { get; set; }

    public Vector3 _hitPoint { get; private set; }

    protected bool _isDead = false;

    [SerializeField] protected int _maxHealth;

    protected int _currentHealth;

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDie = null;

    private AIActionData _actionData;

    private HealthBarUI _healthBarUI;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _actionData = transform.Find("AI").GetComponent<AIActionData>();
        _healthBarUI = transform.Find("HealthBar").GetComponent<HealthBarUI>();
    }

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (_isDead) return;

        _actionData.hitNormal = normal;
        _actionData.hitPoint = hitPoint;
        
        _currentHealth -= damage;

        OnGetHit?.Invoke();

        if(_healthBarUI.gameObject.activeSelf == false)
        _healthBarUI.gameObject.SetActive(true);

        _healthBarUI.SetHealth(_currentHealth);

        if(_currentHealth <= 0 )
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        _isDead = true;
        OnDie?.Invoke();
    }

    public void Reset()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
        _healthBarUI.SetHealth(_currentHealth);
        _healthBarUI.gameObject.SetActive(false);
    }
}

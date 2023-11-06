using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBall : PoolableMono
{
    private Light2D _light;
    public Light2D Light => _light;
    public float LightMaxIntensity = 2.5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigid;
    private bool _isDead = false;

    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private BulletDataSO _bulletData;

    private void Awake()
    {
        _light = transform.Find("Light2D").GetComponent<Light2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Flip(bool value)
    {
        _spriteRenderer.flipX = value;
    }

    public void Fire(Vector2 direction)
    {
        _rigid.velocity = direction * _bulletData.bulletSpeed;
    }

    public override void Init()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one;
        _rigid.velocity = Vector3.zero;
        _isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) ||
            ((1<< collision.gameObject.layer) & _whatIsEnemy) > 0)
        {
            HitObstacle(collision);

            _isDead = true;
            PoolManager.Instance.Push(this);
        }
    }
    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as ImpactScript;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        Vector3 explosionPosition = transform.position + transform.right * 0.5f;
        impact.SetPositionAndRotation(explosionPosition, rot);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f, _whatIsEnemy);

        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out IDamagerable health))
            {
                Vector3 normal = (transform.position - collider.transform.position).normalized;
                health.GetHit(_bulletData.damage, gameObject, collider.transform.position, normal);
            }
        }
    }
}

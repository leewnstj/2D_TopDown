using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] private FireBall _enemyBulletPrefab;

    [SerializeField] private float _coolTime = 3f;

    private float _lastFireTime = 0;

    private FireBall _currentFireBall = null;

    public override void Attack()
    {
        if(_actionData.IsAttack == false && _lastFireTime + _coolTime < Time.time)
        {
            //공격이 가능하다면
            _actionData.IsAttack = true; //공격으로 전환해주고

            StartAttackSequence();
        }
    }

    private void StartAttackSequence()
    {
        Sequence seq = DOTween.Sequence();

        _currentFireBall = PoolManager.Instance.Pop("FireBall") as FireBall;
        _currentFireBall.transform.position = transform.position + new Vector3(0, 0.25f, 0);
        _currentFireBall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        seq.Append(_currentFireBall.transform.DOMoveY(_currentFireBall.transform.position.y + 1f, 0.5f)) ;
        seq.Join(_currentFireBall.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f)); //위아래 동시 트윈

        seq.Append(_currentFireBall.transform.DOScale(new Vector3(1, 1, 1), 1.2f));

        var t = DOTween.To(()=> _currentFireBall.Light.intensity, 
            value => _currentFireBall.Light.intensity = value,
            _currentFireBall.LightMaxIntensity,
            1.2f);

        seq.Join(t);

        seq.AppendCallback(() =>
        {
            _lastFireTime = Time.time;
            _actionData.IsAttack = false;
            _currentFireBall.Fire(_currentFireBall.transform.right);

            _currentFireBall = null;
        });
    }

    public void FaceDirection(Vector2 pointerInput)
    {
        if (_currentFireBall == null) return;

        Vector3 direction = (Vector3)pointerInput - _currentFireBall.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x  ) * Mathf.Rad2Deg;

        _currentFireBall.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

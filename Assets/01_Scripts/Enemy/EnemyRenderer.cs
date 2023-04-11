using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemyRenderer : AgentRenderer
{
    [SerializeField] private Vector3 _offset;
    private readonly int _ShowRateHash = Shader.PropertyToID("_ShowRate");

    private EffectScript _effectScript;
    private AgentAnimator _animator;
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<AgentAnimator>();
    }

    public void ShowProgress(float time, Action CallBackAction)
    {
        StartCoroutine(ShowCo(time, CallBackAction));
    }

    private IEnumerator ShowCo(float time, Action CallBackAction)
    {
        Material mat = _spriteRendere.material;

        _effectScript = PoolManager.Instance.Pop("DustEffect") as EffectScript;
        _effectScript.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        _effectScript.PlayEffect();

        transform.localPosition = _offset;
        float currentRate = 1f;
        float percent = 0;
        float currentTime = 0;
        _animator.SetAnimationSpeed(0);

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1, -1, percent);
            mat.SetFloat( _ShowRateHash, currentRate);
            transform.localPosition = Vector3.Lerp(_offset, Vector3.zero, percent);

            yield return null;
        }
        transform.localPosition = Vector3.zero;
        _animator.SetAnimationSpeed(1);
        _effectScript.StopEffect();

        CallBackAction?.Invoke();
    }

    public void Reset()
    {
        StopAllCoroutines();
        _animator.SetAnimationSpeed(1);
        _spriteRendere.material.SetFloat(_ShowRateHash, -1f);
        if(_effectScript != null )
        {
            _effectScript.StopEffect();
        }
    }
}
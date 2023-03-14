using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFeedback : FeedBack
{
    [SerializeField] private Transform _objectToShake;
    [SerializeField] private float _duration = 0.2f, _strength = 1f, _randomness = 90f;
    [SerializeField] private int _vibrato = 10;

    [SerializeField] private bool _snapping = false, _fadeOut = true; // 원위치로 돌아오나 : true

    public override void CompleteFeedBack()
    {
        _objectToShake.DOComplete(); //기존에 transform에서 진행중이던 트윈을 모두 종류한다
    }

    public override void CreateFeedBack()
    {
        CompleteFeedBack();
        _objectToShake.DOShakePosition(_duration, _strength, _vibrato, _randomness, _snapping, _fadeOut);
    }
}

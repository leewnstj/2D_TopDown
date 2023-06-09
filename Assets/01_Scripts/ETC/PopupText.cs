using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : PoolableMono
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void SetUp(string text, Vector3 pos, Color color, float fontSize = 7f)
    {
        transform.position = pos;
        _textMesh.SetText(text);
        _textMesh.color = color;
        _textMesh.fontSize = fontSize;

        ShowingSequence();
    }

    public void ShowingSequence()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(transform.position.y + 0.5f, 1f)); //1초동안 위로 올라가면서
        seq.Join(_textMesh.DOFade(0f,1f)); //페이트 아웃도 같이 진행되고
        seq.AppendCallback(() =>  //다 끝나면 다시 풀로 넣어준다
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void Reset()
    {
        _textMesh.color = Color.white;
        _textMesh.fontSize = 7f;
        _textMesh.alpha = 1f; //리셋시 다시
    }
}

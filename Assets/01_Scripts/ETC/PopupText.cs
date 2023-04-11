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

        seq.Append(transform.DOMoveY(transform.position.y + 0.5f, 1f)); //1�ʵ��� ���� �ö󰡸鼭
        seq.Join(_textMesh.DOFade(0f,1f)); //����Ʈ �ƿ��� ���� ����ǰ�
        seq.AppendCallback(() =>  //�� ������ �ٽ� Ǯ�� �־��ش�
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void Reset()
    {
        _textMesh.color = Color.white;
        _textMesh.fontSize = 7f;
        _textMesh.alpha = 1f; //���½� �ٽ�
    }
}

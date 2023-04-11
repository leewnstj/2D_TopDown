using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private ItemDropTableSO _dropTable;
    private float[] _itemWeights; //��� Ȯ��

    [SerializeField] private bool _dropEffect = false; //����Ǵ� ����Ʈ
    [SerializeField] private float _dropPower = 2f;

    [SerializeField][Range(0f, 1f)] private float _dropChance;

    private void Start()
    {
        _itemWeights = _dropTable.DropList.Select(item => item.Rate).ToArray();
    }

    public void DropItem()
    {
        float dropVariable = Random.value; //0~1���� inclusive : ����, exclusive : ������
        if(dropVariable < _dropChance) //�������� �ϴ� ����ؾ��Ѵ�
        {
            int idx = GetRandomWeightIndex();
            ItemScript item = PoolManager.Instance.Pop(_dropTable.DropList[idx].ItemPrefab.name) as ItemScript;

            item.transform.position = transform.position; //�� ��ġ�� ������

            if(_dropEffect)
            {
                Vector3 offset = Random.insideUnitCircle * 1.5f;
                item.transform.DOJump(transform.position + offset, _dropPower, 1, 0.35f);
            }
        }
    }

    private int GetRandomWeightIndex()
    {
        float sum = 0;
        for (int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i];
        }
        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for (int i = 0; i < _itemWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }
            else
                tempSum += _itemWeights[i];
        }

        return 0; //���⿡�� �������� �ʴ´�
    }
}

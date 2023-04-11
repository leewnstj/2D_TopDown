using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/ResourcesData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate; //�� �������� ���Ȯ��
    public GameObject ItemPrefab; //�ش� �������� �������� ����

    public ItemType ItemType;

    [SerializeField] private int _minAmount = 1, _maxAmount = 5;

    public Color PopupTextColor; //�� �������� �Ծ��� �� �ߴ� �۾��� ����
    public AudioClip UseSound;   //�� �������� �Ծ��� �� ����� ����

    public int GetAmount()
    {
        return Random.Range(_minAmount, _maxAmount);
    }
}

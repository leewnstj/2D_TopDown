using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/ResourcesData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate; //이 아이템의 드랍확률
    public GameObject ItemPrefab; //해당 아이템의 프리팹을 저장

    public ItemType ItemType;

    [SerializeField] private int _minAmount = 1, _maxAmount = 5;

    public Color PopupTextColor; //이 아이템을 먹었을 때 뜨는 글씨의 색상
    public AudioClip UseSound;   //이 아이템을 먹었을 때 재생할 사운드

    public int GetAmount()
    {
        return Random.Range(_minAmount, _maxAmount);
    }
}

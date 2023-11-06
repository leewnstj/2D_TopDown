using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PollingListSO _poolingList;

    [SerializeField] private Transform _spawnPointParent;
    [SerializeField] private SpawnListSO _spawnList;
    private float[] _spawnWeight;
    public List<Transform> _spawnPointList = new List<Transform>();

    [SerializeField] private Transform _playerTrm;
    public Transform PlayerTrm => _playerTrm;


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        TimeComtroller.Instance = gameObject.AddComponent<TimeComtroller>();

        MakePool();

        _spawnPointParent.GetComponentsInChildren<Transform>(_spawnPointList);
        _spawnPointList.RemoveAt(0); //0번째는 부모

        _spawnWeight = _spawnList.SpawnPairs.Select(s => s.spawnPercent).ToArray();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.lis.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));

        _spawnPointList = new List<Transform>();
    }


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 3f)
            {
                currentTime = 0;
                int idx = Random.Range(0, _spawnPointList.Count);


                int cnt = Random.Range(2, 5);
                for(int i = 0; i < cnt; i++)
                {
                    int sIndex = GetRandomSpawnIndex();
                    EnemyBrain enemy = PoolManager.Instance.Pop(_spawnList.SpawnPairs[sIndex].prefab.name) as EnemyBrain;
                    Vector2 positionOffset = Random.insideUnitCircle * 2; //반지름 1짜리 원안에 랜덤한 포지션

                    enemy.transform.position = _spawnPointList[idx].position + (Vector3)positionOffset;
                    enemy.ShowEnemy();

                    float showTime = Random.Range(0.1f, 0.3f);
                    yield return new WaitForSeconds(showTime);
                }                
            }
            yield return null;
        }
    }

    private int GetRandomSpawnIndex()
    {
        float sum = 0;
        for (int i = 0; i < _spawnWeight.Length; i++)
        {
            sum += _spawnWeight[i];
        }

        float randomValue = Random.Range(0f, sum);
        float tempSum = 0;

        for (int i = 0; i < _spawnWeight.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _spawnWeight[i])
            {
                return i;
            }
            else
            {
                tempSum += _spawnWeight[i];
            }
        }
            return 0;
    }
}

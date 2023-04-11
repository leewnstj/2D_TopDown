using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PollingListSO _poolingList;

    [SerializeField] private Transform _spawnPointParent;
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
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.lis.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
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

                EnemyBrain enemy = PoolManager.Instance.Pop("EnemyGrowler") as EnemyBrain;
                
                Sequence seq = DOTween.Sequence();

                enemy.transform.position = _spawnPointList[idx].position;
                enemy.ShowEnemy();
            }
            yield return null;
        }
    }
}

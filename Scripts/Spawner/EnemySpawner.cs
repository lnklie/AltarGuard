using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : EnemySpawner.cs
==============================
*/

public class EnemySpawner : SingletonManager<EnemySpawner>
{

    [Header("RushEnemyPrefabs")]
    [SerializeField]
    private GameObject rushOrcPrefab = null;


    [Header("RushBossEnemyPrefabs")]
    [SerializeField]
    private GameObject bossOrcPrefab = null;


    [Header("SpawnPosition")]
    [SerializeField]
    private Vector2[] spawnPos = null;

    [Header("Current Boss")]
    [SerializeField]
    private BossEnemyStatus curBoss = null;
    public BossEnemyStatus CurBoss
    {
        get { return curBoss; }
    }

    private Queue<Vector2> enemyPos = new Queue<Vector2>();
    public Queue<Vector2> EnemyPos
    {
        get { return enemyPos; }
    }

    private Queue<GameObject> rushOrcs = new Queue<GameObject>();


    [SerializeField]
    private GameObject bossOrcs = null;

    private void Start()
    {
        InitEnemyPos();
        InitEnemy(rushOrcPrefab, rushOrcs);
        InitBossEnemy(bossOrcPrefab);
    }
    private void InitEnemyPos()
    {
        // 적 위치 담기
        for (int k = 0; k < 5; k++)
        {
            for (int i = 0; i < 5; i++)
            {
                enemyPos.Enqueue(new Vector2(spawnPos[0].x + i, spawnPos[0].y - k));
                enemyPos.Enqueue(new Vector2(spawnPos[1].x + i, spawnPos[1].y - k));
                enemyPos.Enqueue(new Vector2(spawnPos[2].x + i, spawnPos[2].y - k));
                enemyPos.Enqueue(new Vector2(spawnPos[3].x + i, spawnPos[3].y - k));
            }
        }
    }
    private void InitEnemy(GameObject _enemy, Queue<GameObject> _enemyQueue)
    {
        // 적 오브젝트풀 생성 
        for (int i = 0; i < 100; i++)
        {
            GameObject obj = Instantiate(_enemy);
            _enemyQueue.Enqueue(obj);
            obj.GetComponentInChildren<RushEnemyStatus>().EnemyIndex = i;
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
        }
    }
    private void InitBossEnemy(GameObject _enemyPrefab)
    {
        bossOrcs = Instantiate(_enemyPrefab, this.transform);
        bossOrcs.SetActive(false);
    }
    public void EnemySpawn(int _enemyKey, int _enemyNum = 0)
    {
        GameObject _obj = null;
        RushEnemyStatus _rushEnemyStatus = null;

        for (int i = 0; i < _enemyNum; i++)
        {
            _obj = rushOrcs.Dequeue();
            _obj.SetActive(true);
            _rushEnemyStatus = _obj.GetComponentInChildren<RushEnemyStatus>();
            _rushEnemyStatus.transform.position = enemyPos.Dequeue();
            _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
            _rushEnemyStatus.IsEnemyChange = true;
            enemyPos.Enqueue(_obj.transform.position);
        }
    }
    public void BossEnemySpawn(int _enemyKey)
    {
        bossOrcs.SetActive(true);
        BossEnemyStatus _bossEnemyStatus = bossOrcs.GetComponentInChildren<BossEnemyStatus>();
        curBoss = _bossEnemyStatus;
        _bossEnemyStatus.transform.position = new Vector2(0f, 18f);
        _bossEnemyStatus.IsHPRegen = false;
        _bossEnemyStatus.BossEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
        _bossEnemyStatus.CustomEnemy();
        UIManager.Instance.SetBossEnemy();
        UIManager.Instance.SetBossInfo(true);
        UIManager.Instance.UpdateBossInfo();
    }
    public void ReturnEnemy(GameObject _enemy)
    {
        // 적 다시 돌아오기
        if (!_enemy.CompareTag("Boss"))
        {
            rushOrcs.Enqueue(_enemy);

        }
        else
        {
            ReturnBossEnemy(_enemy);
        }
    }
    public void ReturnBossEnemy(GameObject _enemy)
    {
        Debug.Log("보스 돌아오기");
        UIManager.Instance.SetBossInfo(false);
        _enemy.transform.parent.gameObject.SetActive(false);
    }
}

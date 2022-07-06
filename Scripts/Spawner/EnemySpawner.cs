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
    private GameObject swordOrcPrefab = null;
    [SerializeField]
    private GameObject bowOrcPrefab = null;
    [SerializeField]
    private GameObject wizardOrcPrefab = null;

    [Header("RushBossEnemyPrefabs")]
    [SerializeField]
    private GameObject swordOrcKingPrefab = null;
    [SerializeField]
    private GameObject bowOrcKingPrefab = null;
    [SerializeField]
    private GameObject wizardOrcKingPrefab = null;


    [Header("Default Target")]
    [SerializeField]
    private GameObject altar = null;

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

    private Queue<GameObject> swordOrcs = new Queue<GameObject>();
    private Queue<GameObject> bowOrcs = new Queue<GameObject>();
    private Queue<GameObject> wizardOrcs = new Queue<GameObject>();

    [SerializeField]
    private List<GameObject> bossOrcs = new List<GameObject>();

    private void Start()
    {
        InitEnemyPos();
        InitEnemy(swordOrcPrefab, swordOrcs);
        InitEnemy(bowOrcPrefab, bowOrcs);
        InitEnemy(wizardOrcPrefab, wizardOrcs);
        InitBossEnemy(swordOrcKingPrefab);
        InitBossEnemy(bowOrcKingPrefab);
        InitBossEnemy(wizardOrcKingPrefab);
    }
    private void InitEnemyPos()
    {
        // 적 위치 담기
        for(int k = 0; k < 5; k++)
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
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
        }
    }
    private void InitBossEnemy(GameObject _enemyPrefab)
    {
        GameObject _enemy = Instantiate(_enemyPrefab, this.transform);
        _enemy.SetActive(false);
        bossOrcs.Add(_enemy);
    }
    public void EnemySpawn(int _enemyKey, int _enemyNum = 0)
    {
        GameObject _obj = null;
        RushEnemyStatus _rushEnemyStatus = null;
        if (DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType == (int)EnemyType.SwordOrc)
        {
            for (int i = 0; i < _enemyNum; i++)
            {
                _obj = swordOrcs.Dequeue();
                _obj.transform.position = enemyPos.Dequeue();
                _obj.SetActive(true);
                _rushEnemyStatus = _obj.GetComponent<RushEnemyStatus>();
                _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
                _rushEnemyStatus.IsEnemyChange = true;
                enemyPos.Enqueue(_obj.transform.position);
            }
        }
        else if(DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType == (int)EnemyType.BowOrc)
        {
            for (int i = 0; i < _enemyNum; i++)
            {
                _obj = bowOrcs.Dequeue();
                _obj.transform.position = enemyPos.Dequeue();
                _obj.SetActive(true);
                _rushEnemyStatus = _obj.GetComponent<RushEnemyStatus>();
                _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
                _rushEnemyStatus.IsEnemyChange = true;
                enemyPos.Enqueue(_obj.transform.position);
            }
        }
        else if(DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType == (int)EnemyType.WizardOrc)
        {
            for (int i = 0; i < _enemyNum; i++)
            {
                _obj = wizardOrcs.Dequeue();
                _obj.transform.position = enemyPos.Dequeue();
                _obj.SetActive(true);
                _rushEnemyStatus = _obj.GetComponent<RushEnemyStatus>();
                _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
                _rushEnemyStatus.IsEnemyChange = true;
                enemyPos.Enqueue(_obj.transform.position);
            }
        }
    }
    public void BossEnemySpawn(int _enemyKey)
    {
        GameObject _obj = bossOrcs[DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType];
        _obj.SetActive(true);
        _obj.transform.position = new Vector2(0f, 18f);
        BossEnemyStatus _bossEnemyStatus = _obj.GetComponent<BossEnemyStatus>();
        curBoss = _bossEnemyStatus;
        _bossEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
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
            EnemyType _enemyType = _enemy.GetComponent<EnemyStatus>().EnemyType;
            if (_enemyType == EnemyType.SwordOrc)
                swordOrcs.Enqueue(_enemy);
            else if (_enemyType == EnemyType.BowOrc)
                bowOrcs.Enqueue(_enemy);
            else if (_enemyType == EnemyType.WizardOrc)
                wizardOrcs.Enqueue(_enemy);
            _enemy.transform.localScale = new Vector3(1, 1, 1);
            _enemy.SetActive(false);
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
        _enemy.SetActive(false);
    }
}

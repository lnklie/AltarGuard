using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : EnemySpawner.cs
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
    private GameObject curBoss = null;
    public GameObject CurBoss
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

    private void Awake()
    {
        InitEnemyPos();
        InitEnemy(swordOrcPrefab, swordOrcs);
        InitEnemy(bowOrcPrefab, bowOrcs);
        InitEnemy(wizardOrcPrefab, wizardOrcs);
        InitBossEnemy(swordOrcKingPrefab);
        InitBossEnemy(bowOrcKingPrefab);
        InitBossEnemy(wizardOrcKingPrefab);
    }
    private void Update()
    {
        Debug.Log("���� ��ũ�� ���� " + swordOrcs.Count);
        Debug.Log("�û� ��ũ�� ���� " + bowOrcs.Count);
        Debug.Log("������ ��ũ�� ���� " + wizardOrcs.Count);
    }
    private void InitEnemyPos()
    {
        // �� ��ġ ���
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
        // �� ������ƮǮ ���� 
        for (int i = 0; i < 100; i++)
        {
            GameObject obj = Instantiate(_enemy);
            _enemyQueue.Enqueue(obj);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
        }
    }
    private void InitBossEnemy(GameObject _enemy)
    {
        GameObject _bossEnemy = Instantiate(_enemy);
        _bossEnemy.SetActive(false);
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
        Debug.Log("���� ��ȯ ?");
        GameObject _obj = null;
        if (DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType == (int)EnemyType.SwordOrc)
            _obj = swordOrcKingPrefab;
        else if (DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType == (int)EnemyType.BowOrc)
            _obj = bowOrcKingPrefab;
        else if (DatabaseManager.Instance.SelectRushEnemy(_enemyKey).enemyType == (int)EnemyType.WizardOrc)
            _obj = wizardOrcKingPrefab;
        _obj.transform.position = new Vector2(0f, 18f);
        _obj.SetActive(true);
        BossEnemyStatus _rushEnemyStatus = _obj.GetComponent<BossEnemyStatus>();
        _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
        _rushEnemyStatus.CustomEnemy();
        curBoss = _obj;
    }
    public void ReturnEnemy(GameObject _enemy)
    {
        // �� �ٽ� ���ƿ���
        EnemyType _enemyType = _enemy.GetComponent<EnemyStatus>().EnemyType;
        if (_enemyType == EnemyType.SwordOrc)
            swordOrcs.Enqueue(_enemy);
        else if (_enemyType == EnemyType.BowOrc)
            bowOrcs.Enqueue(_enemy);
        else if (_enemyType == EnemyType.WizardOrc)
            wizardOrcs.Enqueue(_enemy);
        _enemy.SetActive(false);
    }
    public void ReturnBossEnemy(GameObject _enemy)
    {
        _enemy.SetActive(false);
    }
}

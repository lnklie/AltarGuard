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
    public void EnemySpawn(EnemyType _enemyType, int _enemyNum = 0)
    {
        GameObject _obj = null;
        RushEnemyStatus _rushEnemyStatus = null;
        if (_enemyType == EnemyType.SwordOrc)
        {
            for (int i = 0; i < _enemyNum; i++)
            {
                _obj = swordOrcs.Dequeue();
                _obj.transform.position = enemyPos.Dequeue();
                _obj.SetActive(true);
                _rushEnemyStatus = _obj.GetComponent<RushEnemyStatus>();
                _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.rushEnemyList[(int)_enemyType];
                _rushEnemyStatus.CustomEnemy();
                enemyPos.Enqueue(_obj.transform.position);
            }
        }
        else if(_enemyType == EnemyType.BowOrc)
        {
            for (int i = 0; i < _enemyNum; i++)
            {
                _obj = bowOrcs.Dequeue();
                _obj.transform.position = enemyPos.Dequeue();
                _obj.SetActive(true);
                _rushEnemyStatus = _obj.GetComponent<RushEnemyStatus>();
                _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.rushEnemyList[(int)_enemyType];
                _rushEnemyStatus.CustomEnemy();
                enemyPos.Enqueue(_obj.transform.position);
            }
        }

    }
    //public void BossEnemySpawn(BossEnemyType _bossEnemyType, int _enemyNum = 1)
    //{
    //    GameObject _obj = null;
    //    EnemyCustomizer _enemyCustomizer = _obj.GetComponent<EnemyCustomizer>();

    //    _obj.transform.position = new Vector2(0f, 18f);
    //    _obj.SetActive(true);
    //    _enemyCustomizer = _obj.GetComponent<EnemyCustomizer>();
    //    //_enemyCustomizer.SetAIController(bossEnemyAIController[(int)_bossEnemyType]);
    //    _enemyCustomizer.SetEnemyStatus(DatabaseManager.Instance.rushEnemyList[(int)_bossEnemyType]);
    //    _enemyCustomizer.SetAnimator(rushEnemyAIController[(int)_bossEnemyType].GetComponent<Animator>().runtimeAnimatorController);
    //    _enemyCustomizer.IsActive = true;
    //    curBoss = _obj;
    //}
    public void ReturnEnemy(GameObject _enemy)
    {
        // 적 다시 돌아오기
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

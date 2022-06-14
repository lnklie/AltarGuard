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
    [Header("Enemies")]
    [SerializeField]
    private EnemyAIController slimeAIController = null;
    [SerializeField]
    private EnemyAIController slimeKingAIController = null;
    [SerializeField]
    private EnemyAIController goblineArcherAIController = null;

    [Header("EnemyPrefab")]
    [SerializeField]
    private GameObject enemyPrefab = null;

    [Header("Option")]
    [SerializeField]
    private int enemyNum = 0;

    [Header("Default Target")]
    [SerializeField]
    private GameObject building = null;

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

    private Queue<Vector2> enemyEastPos = new Queue<Vector2>();
    public Queue<Vector2> EnemyEastPos
    {
        get { return enemyEastPos; }
    }

    private Queue<Vector2> enemyNorthPos = new Queue<Vector2>();
    public Queue<Vector2> EnemyNorthPos
    {
        get { return enemyNorthPos; }
    }

    private Queue<Vector2> enemyWestPos = new Queue<Vector2>();
    public Queue<Vector2> EnemyWestPos
    {
        get { return enemyWestPos; }
    }

    private Queue<Vector2> enemySouthPos = new Queue<Vector2>();
    public Queue<Vector2> EnemySouthPos
    {
        get { return enemySouthPos; }
    }

    private Queue<GameObject> enemies = new Queue<GameObject>();
    private Queue<EnemyStatus> enemyStatus = new Queue<EnemyStatus>();

    private void Awake()
    {
        InitEnemyPos();
        InitEnemy(enemyPrefab);

    }

    private void InitEnemyPos()
    {
        // 적 위치 담기
        for(int k = 0; k < 5; k++)
        {
            for (int i = 0; i < 5; i++)
            {
                enemyEastPos.Enqueue(new Vector2(spawnPos[0].x + i, spawnPos[0].y - k));
                enemyWestPos.Enqueue(new Vector2(spawnPos[1].x + i, spawnPos[1].y - k));
                enemySouthPos.Enqueue(new Vector2(spawnPos[2].x + i, spawnPos[2].y - k));
                enemyNorthPos.Enqueue(new Vector2(spawnPos[3].x + i, spawnPos[3].y - k));
            }
        }
    }
    private void InitEnemy(GameObject _enemy)
    {
        // 적 오브젝트풀 생성 
        for (int i = 0; i < 100; i++)
        {
            GameObject obj = Instantiate(_enemy);
            enemies.Enqueue(obj);
            enemyStatus.Enqueue(obj.GetComponent<EnemyStatus>());
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
        }
    }
    public void EnemySpawn(EnemyType _enemyType, Queue<Vector2> _Dirqueue = null, int _enemyNum = 0)
    {
        // 적 스폰
        GameObject _obj = null;
        EnemyStatus _enemyStatus = null;
        if (_enemyType == EnemyType.Slime)
        {
            for(int i = 0; i < _enemyNum; i++)
            {
                _obj = enemies.Dequeue();
                _obj.transform.position = _Dirqueue.Dequeue();
                _enemyStatus = enemyStatus.Dequeue();
                _enemyStatus.SetEnemyStatus(DatabaseManager.Instance.enemyList[(int)EnemyType.Slime]);
                _enemyStatus.SetAnimator(slimeAIController.GetComponent<Animator>().runtimeAnimatorController);
                _enemyStatus.GetComponent<EnemyCustomizer>().SetAIController(slimeAIController);
                _enemyStatus.GetComponent<EnemyCustomizer>().IsActive = true;
                _Dirqueue.Enqueue(_obj.transform.position);
            }
        }
        else if(_enemyType == EnemyType.SlimeKing)
        {
            _obj.transform.position = new Vector2(0f, 18f);
            curBoss = _obj;
        }
        else if(_enemyType == EnemyType.GoblinArcher)
        {
            for (int i = 0; i < _enemyNum; i++)
            {

            }
        }
        _obj.SetActive(true);
        _obj.GetComponent<EnemyStatus>().Target = building;
    }
    public void ReturnEnemy(GameObject _enemy, EnemyType _enemyType)
    {
        // 적 다시 돌아오기
        //if (_enemyType == EnemyType.Slime)
        //    enemy_Slimes.Enqueue(_enemy);
        //else if (_enemyType == EnemyType.GoblinArcher)
        //    enemy_GoblinArcher.Enqueue(_enemy);
        //else if (_enemyType == EnemyType.SlimeKing)
        //    enemy_SlimeKing = _enemy;
        //_enemy.SetActive(false);
    }
}

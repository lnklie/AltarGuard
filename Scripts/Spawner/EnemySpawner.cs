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
    private EnemyAIController[] enemyAIController = null;
    [SerializeField]
    private EnemyAIController[] bossEnemyAIController = null;

    [Header("EnemyPrefab")]
    [SerializeField]
    private GameObject enemyPrefab = null;

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

    private Queue<Vector2> enemyPos = new Queue<Vector2>();
    public Queue<Vector2> EnemyPos
    {
        get { return enemyPos; }
    }


    private Queue<GameObject> enemies = new Queue<GameObject>();

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
                enemyPos.Enqueue(new Vector2(spawnPos[0].x + i, spawnPos[0].y - k));
                enemyPos.Enqueue(new Vector2(spawnPos[1].x + i, spawnPos[1].y - k));
                enemyPos.Enqueue(new Vector2(spawnPos[2].x + i, spawnPos[2].y - k));
                enemyPos.Enqueue(new Vector2(spawnPos[3].x + i, spawnPos[3].y - k));
            }
        }
    }
    private void InitEnemy(GameObject _enemy)
    {
        // 적 오브젝트풀 생성 
        for (int i = 0; i < 101; i++)
        {
            GameObject obj = Instantiate(_enemy);
            enemies.Enqueue(obj);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
        }
    }
    public void EnemySpawn(EnemyType _enemyType, int _enemyNum = 0)
    {
        GameObject _obj = null;
        EnemyCustomizer _enemyCustomizer = null;

        for (int i = 0; i < _enemyNum; i++)
        {
            _obj = enemies.Dequeue();
            _obj.transform.position = enemyPos.Dequeue();
            _obj.SetActive(true);
            _enemyCustomizer = _obj.GetComponent<EnemyCustomizer>();
            _enemyCustomizer.SetAIController(enemyAIController[(int)_enemyType]);
            _enemyCustomizer.SetEnemyStatus(DatabaseManager.Instance.enemyList[(int)_enemyType]);
            _enemyCustomizer.SetAnimator(enemyAIController[(int)_enemyType].GetComponent<Animator>().runtimeAnimatorController);
            _enemyCustomizer.IsActive = true;
            enemyPos.Enqueue(_obj.transform.position);
        }


    }
    public void BossEnemySpawn(BossEnemyType _bossEnemyType, int _enemyNum = 1)
    {
        GameObject _obj = null;
        EnemyCustomizer _enemyCustomizer = _obj.GetComponent<EnemyCustomizer>();

        _obj.transform.position = new Vector2(0f, 18f);
        _obj.SetActive(true);
        _enemyCustomizer = _obj.GetComponent<EnemyCustomizer>();
        _enemyCustomizer.SetAIController(bossEnemyAIController[(int)_bossEnemyType]);
        _enemyCustomizer.SetEnemyStatus(DatabaseManager.Instance.enemyList[(int)_bossEnemyType]);
        _enemyCustomizer.SetAnimator(enemyAIController[(int)_bossEnemyType].GetComponent<Animator>().runtimeAnimatorController);
        _enemyCustomizer.IsActive = true;
        curBoss = _obj;
    }
    public void ReturnEnemy(GameObject _enemy)
    {
        // 적 다시 돌아오기
        enemies.Enqueue(_enemy);
        _enemy.SetActive(false);
    }
    public void ReturnBossEnemy(GameObject _enemy)
    {
        _enemy.SetActive(false);
    }
}

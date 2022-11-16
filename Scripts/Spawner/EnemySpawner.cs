using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    [Header("RushEnemyPrefabs")] 
    [SerializeField] private GameObject rushOrcPrefab = null;


    [Header("RushBossEnemyPrefabs")]
    [SerializeField] private GameObject bossOrcPrefab = null;


    [Header("SpawnPosition")]
    [SerializeField] private Vector2[] spawnPos = null;

    [Header("Current Boss")]
    [SerializeField] private BossEnemyStatus curBoss = null;

    [SerializeField] private GameObject bossOrcs = null;


    [SerializeField] private List<GameObject> rushOrcs = new List<GameObject>();
    [SerializeField] private int queueIndex = 99;
    [SerializeField] private int spawnPosIndex = 3;
    public BossEnemyStatus CurBoss { get { return curBoss; } }
    public static EnemySpawner Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitEnemy(rushOrcPrefab);
        InitBossEnemy(bossOrcPrefab);
    }


    private void InitEnemy(GameObject _enemy)
    {
        // 적 오브젝트풀 생성 
        for (int i = 0; i < 100; i++)
        {
            GameObject obj = Instantiate(_enemy);
            rushOrcs.Add(obj);
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
        EquipmentController _rushEnemyEquipment = null;
        for (int i = 0; i < _enemyNum; i++)
        {
            _obj = rushOrcs[queueIndex];
            queueIndex--;
            if (queueIndex == -1)
            {
                queueIndex = 99;
            }
            _obj.SetActive(true);
            _rushEnemyStatus = _obj.GetComponentInChildren<RushEnemyStatus>();
            _rushEnemyEquipment = _obj.GetComponentInChildren<EquipmentController>();
            _rushEnemyStatus.RushEnemy = DatabaseManager.Instance.SelectRushEnemy(_enemyKey);
            _rushEnemyEquipment.ChangeEquipment(DatabaseManager.Instance.SelectItem(_rushEnemyStatus.RushEnemy.weaponKey));
            _rushEnemyEquipment.ChangeEquipment(DatabaseManager.Instance.SelectItem(_rushEnemyStatus.RushEnemy.helmetKey));
            _rushEnemyEquipment.ChangeEquipment(DatabaseManager.Instance.SelectItem(_rushEnemyStatus.RushEnemy.armorKey));
            _rushEnemyEquipment.ChangeEquipment(DatabaseManager.Instance.SelectItem(_rushEnemyStatus.RushEnemy.pantKey));
            _rushEnemyStatus.transform.position = spawnPos[spawnPosIndex] + new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            spawnPosIndex--;
            if (spawnPosIndex == -1)
            {
                spawnPosIndex = 3;
            }
            _rushEnemyStatus.CustomEnemy();
            _rushEnemyStatus.IsEnemyChange = true;
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

    public void ReturnBossEnemy(GameObject _enemy)
    {
        Debug.Log("보스 돌아오기");
        UIManager.Instance.SetBossInfo(false);
        _enemy.transform.parent.gameObject.SetActive(false);
    }
}

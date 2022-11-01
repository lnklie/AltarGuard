using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    [SerializeField] private bool isStart = false;
    [SerializeField] private bool isStage = false;
    [SerializeField] private EnemySpawner enemySpawner = null;
    [SerializeField] private PlayerStatus player;
    [SerializeField] private Stage curStage = null;
    [SerializeField] private int spawnedEneies = 0;

    public static StageManager Instance = null;
    public bool IsStart { get { return isStart; } }
    public bool IsStage { get { return isStage; } }
    public int SpawnedEneies { get { return spawnedEneies;} set { spawnedEneies = value; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    StartStage();
        //}
        if (isStart)
        {
            CheckStage();
            Debug.Log("스테이지 시작");
            //enemySpawner.BossEnemySpawn(curStage.bossKey);
            
            StageSpawn();
            isStart = false;
        }
        //if (isStage)
        //{
        //    if (spawnedEneies <= 0)
        //        enemySpawner.BossEnemySpawn(curStage.bossKey);
        //}
    }
    public void StartStage()
    {
        isStart = true;
        isStage = true;
    }
    public void CheckStage()
    {
        for(int i = 0; i < DatabaseManager.Instance.stageList.Count; i++)
        {
            if (player.Stage == DatabaseManager.Instance.stageList[i].stage)
            {
                curStage = DatabaseManager.Instance.stageList[i];
            }
        }
    }
    private void StageSpawn()
    {
        // 스테이지
        if (curStage != null)
        {
            spawnedEneies = curStage.enemyNum1 + curStage.enemyNum2;
            enemySpawner.EnemySpawn(curStage.enemyKey1, curStage.enemyNum1);
            enemySpawner.EnemySpawn(curStage.enemyKey2, curStage.enemyNum2);
        }
        isStart = false;
    }
}

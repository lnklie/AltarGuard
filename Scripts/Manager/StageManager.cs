using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : StageManager.cs
==============================
*/
public class StageManager : SingletonManager<StageManager>
{

    [SerializeField]
    private bool isStart = false;
    public bool IsStart
    {
        get { return isStart; }
    }
    [SerializeField]
    private bool isStage = false;
    public bool IsStage
    {
        get { return isStage; }
    }
    [SerializeField]
    private EnemySpawner enemySpawner = null;
    [SerializeField]
    private PlayerStatus player;
    [SerializeField]
    private Stage curStage = null;

    [SerializeField]
    private int spawnedEneies = 0;
    public int SpawnedEneies
    {
        get { return spawnedEneies;}
        set { spawnedEneies = value; }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isStart = true;
            isStage = true;
        }
        if (isStart)
        {
            CheckStage();
            Debug.Log("�������� ����");
            StageSpawn();
        }
        if (isStage)
        {
            if (spawnedEneies <= 0)
                enemySpawner.BossEnemySpawn(curStage.bossKey);
        }
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
        // ��������
        if (curStage != null)
        {
            spawnedEneies = curStage.enemyNum1 + curStage.enemyNum2;
            enemySpawner.EnemySpawn(curStage.enemyKey1, curStage.enemyNum1);
            enemySpawner.EnemySpawn(curStage.enemyKey2, curStage.enemyNum2);
        }
        isStart = false;
    }
}

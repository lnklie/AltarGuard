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
public class StageManager : MonoBehaviour
{

    [SerializeField]
    private bool isStart = false;
    public bool IsStart
    {
        get { return isStart; }
    }

    [SerializeField]
    private EnemySpawner enemySpawner = null;
    [SerializeField]
    private PlayerStatus player;
    [SerializeField]
    private Stage curStage = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            isStart = true;
        if (isStart)
        {
            CheckStage();
            Debug.Log("�������� ����");
            StageSpawn();
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
            enemySpawner.EnemySpawn((EnemyType)curStage.enemyKey1, curStage.enemyNum1);
            enemySpawner.EnemySpawn((EnemyType)curStage.enemyKey2, curStage.enemyNum2);
        }
        isStart = false;
    }
}

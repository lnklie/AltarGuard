using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : StageManager.cs
==============================
*/
public class StageManager : MonoBehaviour
{

    private int curStage = 0;

    private bool isStart = false;
    public bool IsStart
    {
        get { return isStart; }
    }

    [SerializeField]
    private EnemySpawner enemySpawner = null;
    [SerializeField]
    private PlayerStatus player;

    private void Awake()
    {
        curStage = player.Stage;
    }
     
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            isStart = true;
        if (isStart)
        {
            Debug.Log("스테이지 시작");
            Stage();
        }
    }
    private void Stage()
    {
        // 스테이지
        switch (curStage)
        {            case 1:
                enemySpawner.EnemySpawn(EnemyType.Slime,enemySpawner.EnemyEastPos,15);
                enemySpawner.EnemySpawn(EnemyType.GoblinArcher, enemySpawner.EnemyEastPos,10);
                enemySpawner.EnemySpawn(EnemyType.Slime, enemySpawner.EnemyWestPos, 15);
                enemySpawner.EnemySpawn(EnemyType.GoblinArcher, enemySpawner.EnemyWestPos, 10);
                enemySpawner.EnemySpawn(EnemyType.Slime, enemySpawner.EnemySouthPos, 15);
                enemySpawner.EnemySpawn(EnemyType.GoblinArcher, enemySpawner.EnemySouthPos, 10);
                enemySpawner.EnemySpawn(EnemyType.Slime, enemySpawner.EnemyNorthPos, 15);
                enemySpawner.EnemySpawn(EnemyType.GoblinArcher, enemySpawner.EnemyNorthPos, 10);
                //enemySpawner.EnemySpawn(EnemyType.SlimeKing);
                break;
        }
        isStart = false;
    }
}

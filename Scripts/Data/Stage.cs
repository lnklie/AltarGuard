using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public int stage = 0;
    public int enemyKey1 = 0;
    public int enemyKey2 = 0;
    public int bossKey = 0;
    public int enemyNum1 = 0;
    public int enemyNum2 = 0;

    public Stage(int _stage,int _enemyKey1, int _enemyKey2, int _bossKey, int _enemyNum1, int _enemyNum2)
    {
        stage = _stage;
        enemyKey1 = _enemyKey1;
        enemyKey2 = _enemyKey2;
        bossKey = _bossKey;
        enemyNum1 = _enemyNum1;
        enemyNum2 = _enemyNum2;
    }
}

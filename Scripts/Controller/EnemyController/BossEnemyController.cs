using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : BossEnemyAIController.cs
==============================
*/
public class BossEnemyController : EnemyController
{
    [SerializeField]
    private BossEnemyStatus bossEnemyStatus = null;
    public override void Awake()
    {
        base.Awake();
        bossEnemyStatus = GetComponent<BossEnemyStatus>();
    }
}

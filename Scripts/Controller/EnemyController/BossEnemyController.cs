using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : BossEnemyAIController.cs
==============================
*/
public class BossEnemyController : EnemyController
{
    private BossEnemyStatus bossEnemyStatus = null;
    public override void Awake()
    {
        bossEnemyStatus = GetComponent<BossEnemyStatus>();
    }
}

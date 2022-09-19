using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyController
{
    [SerializeField] private BossEnemyStatus bossEnemyStatus = null;
    public override void Awake()
    {
        base.Awake();
        bossEnemyStatus = GetComponent<BossEnemyStatus>();
    }
}

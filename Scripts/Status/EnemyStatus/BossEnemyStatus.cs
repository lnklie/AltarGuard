using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyStatus : EnemyStatus
{
    [SerializeField]
    protected Enemy bossEnemy = null;
    public Enemy BossEnemy
    {
        set { bossEnemy = value; }
    }

    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
        if (isEnemyChange)
        {
            CustomEnemy();
        }
    }
    public void CustomEnemy()
    {
        Debug.Log("커스텀 에너미 2");
        objectName = bossEnemy.objectName;
        str = bossEnemy.str;
        dex = bossEnemy.dex;
        wiz = bossEnemy.wiz;
        seeRange = bossEnemy.seeRange;
        defeatExp = bossEnemy.defeatExp;
        atkRange = equipedAtkRange;
        atkSpeed = equipedAtkSpeed;
        UpdateTotalAbility();

        itemDropKey.Add(bossEnemy.itemDropKey1);
        itemDropKey.Add(bossEnemy.itemDropKey2);
        itemDropKey.Add(bossEnemy.itemDropKey3);
        itemDropKey.Add(bossEnemy.itemDropKey4);
        itemDropKey.Add(bossEnemy.itemDropKey5);
        itemDropProb.Add(bossEnemy.itemDropProb1);
        itemDropProb.Add(bossEnemy.itemDropProb2);
        itemDropProb.Add(bossEnemy.itemDropProb3);
        itemDropProb.Add(bossEnemy.itemDropProb4);
        itemDropProb.Add(bossEnemy.itemDropProb5);

        curHp = maxHp;
        isEnemyChange = false;
    }

    public override void UpdateTotalAbility()
    {
        // 능력 업데이트
        maxHp = bossEnemy.hp + str * 10;
        maxMp = bossEnemy.mp + wiz * 10;
        physicalDamage = str * 5 + equipedPhysicalDamage;
        magicalDamage = wiz * 5 + equipedMagicalDamage;
        defensivePower = str * 3 + equipedDefensivePower;
        speed = bossEnemy.speed + dex * 1f;
        graceHpRegenValue = str * 1;
    }
}

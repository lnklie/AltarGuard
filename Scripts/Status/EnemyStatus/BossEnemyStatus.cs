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
        seeRange = bossEnemy.seeRange;
        defeatExp = bossEnemy.defeatExp;

        totalStatus[(int)EStatus.Str] = bossEnemy.str + equipStatus[(int)EStatus.Str];
        totalStatus[(int)EStatus.Dex] = bossEnemy.dex + equipStatus[(int)EStatus.Dex];
        totalStatus[(int)EStatus.Wiz] = bossEnemy.str + equipStatus[(int)EStatus.Wiz];
        totalStatus[(int)EStatus.MaxHp] = bossEnemy.hp + totalStatus[(int)EStatus.Str] + equipStatus[(int)EStatus.MaxHp];
        totalStatus[(int)EStatus.MaxMp] = bossEnemy.mp + totalStatus[(int)EStatus.Wiz] + equipStatus[(int)EStatus.MaxMp];
        totalStatus[(int)EStatus.PhysicalDamage] = totalStatus[(int)EStatus.Str] + 5 + equipStatus[(int)EStatus.PhysicalDamage];
        totalStatus[(int)EStatus.MagicalDamage] = totalStatus[(int)EStatus.Wiz] + 5 + equipStatus[(int)EStatus.MagicalDamage];
        totalStatus[(int)EStatus.DefensivePower] = totalStatus[(int)EStatus.Str] + 3 + equipStatus[(int)EStatus.DefensivePower];
        totalStatus[(int)EStatus.Speed] = bossEnemy.speed + totalStatus[(int)EStatus.Dex] * 0.1f;
        totalStatus[(int)EStatus.HpRegenValue] = totalStatus[(int)EStatus.Str] * 0.5f;
        totalStatus[(int)EStatus.AtkRange] = bossEnemy.atkRange + totalStatus[(int)EStatus.AtkRange];
        totalStatus[(int)EStatus.AttackSpeed] = maxAtkSpeed - (bossEnemy.atkSpeed + totalStatus[(int)EStatus.AttackSpeed]);

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

        curHp = (int)totalStatus[(int)EStatus.MaxHp];
        isEnemyChange = false;
    }

}

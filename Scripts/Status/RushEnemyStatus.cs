using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemyStatus : EnemyStatus
{
    [SerializeField]
    protected RushEnemy rushEnemy = null;
    public RushEnemy RushEnemy
    {
        set { rushEnemy = value; }
    }


    protected EquipmentController equipmentController = null;
    private void Awake()
    {
        equipmentController = this.GetComponent<EquipmentController>();
        if (isEnemyChange)
            CustomEnemy();
    }

    public void CustomEnemy()
    {
        objectName = rushEnemy.objectName;
        maxHp = rushEnemy.hp;
        str = rushEnemy.str;
        dex = rushEnemy.dex;
        wiz = rushEnemy.wiz;
        seeRange = rushEnemy.seeRange;
        AtkRange = rushEnemy.atkRange;
        speed = rushEnemy.speed;
        atkSpeed = rushEnemy.atkSpeed;
        arrowSpd = rushEnemy.arrowSpd;
        defeatExp = rushEnemy.defeatExp;
        enemyType = rushEnemy.enemyType;
        for (int i = 0; i < rushEnemy.equipedItems.Length; i ++)
        {
            if(rushEnemy.equipedItems[i] != null)
                equipmentController.ChangeEquipment(rushEnemy.equipedItems[i]);
        }

        itemDropKey.Add(enemy.itemDropKey1);
        itemDropKey.Add(enemy.itemDropKey2);
        itemDropKey.Add(enemy.itemDropKey3);
        itemDropKey.Add(enemy.itemDropKey4);
        itemDropKey.Add(enemy.itemDropKey5);
        itemDropProb.Add(enemy.itemDropProb1);
        itemDropProb.Add(enemy.itemDropProb2);
        itemDropProb.Add(enemy.itemDropProb3);
        itemDropProb.Add(enemy.itemDropProb4);
        itemDropProb.Add(enemy.itemDropProb5);
        Debug.Log("생성");
        isEnemyChange = false;
        UpdateAbility();
    }

    public override void UpdateAbility()
    {
        // 능력 업데이트
        base.UpdateAbility();
        physicalDamage = str * 5 + equipmentController.GetEquipmentPhysicDamage();
        magicalDamage = wiz * 5 + equipmentController.GetEquipmentMagicDamage();
        speed = 2 + dex * 0.1f;
        defensivePower = str * 3 + equipmentController.GetEquipmentDefensivePower();
        hpRegenValue = str * 1;
    }
}

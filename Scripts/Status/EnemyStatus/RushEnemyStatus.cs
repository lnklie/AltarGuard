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

    
    public override void Awake()
    {
        base.Awake();
        equipmentController = this.GetComponent<EquipmentController>();

    }
    public override void Update()
    {
        base.Update();
        if (isEnemyChange && rushEnemy != null)
        {
            CustomEnemy();
        }
    }
    public void CustomEnemy()
    {
        objectName = rushEnemy.objectName;
        str = rushEnemy.str;
        dex = rushEnemy.dex;
        wiz = rushEnemy.wiz;
        seeRange = rushEnemy.seeRange;
        defeatExp = rushEnemy.defeatExp;
        UpdateAbility();
        equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.helmetKey));
        equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.armorKey));
        equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.pantKey));
        equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.weaponKey));
        atkRange = equipmentController.EquipItems[7].atkRange;
        physicalDamage = equipmentController.EquipItems[7].physicalDamage;
        magicalDamage = equipmentController.EquipItems[7].magicalDamage;

        itemDropKey.Add(rushEnemy.itemDropKey1);
        itemDropKey.Add(rushEnemy.itemDropKey2);
        itemDropKey.Add(rushEnemy.itemDropKey3);
        itemDropKey.Add(rushEnemy.itemDropKey4);
        itemDropKey.Add(rushEnemy.itemDropKey5);
        itemDropProb.Add(rushEnemy.itemDropProb1);
        itemDropProb.Add(rushEnemy.itemDropProb2);
        itemDropProb.Add(rushEnemy.itemDropProb3);
        itemDropProb.Add(rushEnemy.itemDropProb4);
        itemDropProb.Add(rushEnemy.itemDropProb5);
        isEnemyChange = false;
    }

    public  void UpdateAbility()
    {
        // 능력 업데이트
        maxHp = rushEnemy.hp + str * 10;
        maxMp = rushEnemy.mp + wiz * 10;
        atkSpeed = 3 - dex * 0.1f;
        physicalDamage = str * 5 + equipmentController.GetEquipmentPhysicDamage();
        magicalDamage = wiz * 5 + equipmentController.GetEquipmentMagicDamage();
        speed = rushEnemy.speed + dex * 0.1f;
        defensivePower = str * 3 + equipmentController.GetEquipmentDefensivePower();
        hpRegenValue = str * 1;
        curHp = maxHp;
    }
}

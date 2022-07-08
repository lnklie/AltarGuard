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
        equipmentController = this.GetComponent<EquipmentController>();
        //if (equipmentController != null)
        //{
        //    Debug.Log("장비 컨트롤러 비어있지 않음 " + equipmentController.name);
        //}
        //else
        //    Debug.Log("비어져있음");
    }
    public override void Update()
    {
        base.Update();
        if (isEnemyChange)
        {
            Debug.Log("커스텀 에너미 1");
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
        if (equipmentController != null )
        {
            equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(4000));
            equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(5000));
            equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(3000));
            equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(10000));
            UpdateAbility();
            atkRange = equipmentController.EquipItems[7].atkRange;
            atkSpeed = equipmentController.EquipItems[7].atkSpeed;
        }
        else
        {
            Debug.Log("여긴 왜없나요?!!!!!!");
        }

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
        //isEnemyChange = false;
    }

    //public void UpdateAbility()
    //{
    //    // 능력 업데이트
    //    maxHp = rushEnemy.hp + str * 10;
    //    maxMp = rushEnemy.mp + wiz * 10;
    //    physicalDamage = str * 5 + equipmentController.GetEquipmentPhysicDamage();
    //    magicalDamage = wiz * 5 + equipmentController.GetEquipmentMagicDamage();
    //    defensivePower = str * 3 + equipmentController.GetEquipmentDefensivePower();
    //    speed = rushEnemy.speed + dex * 0.1f;
    //    hpRegenValue = str * 1;
    //    curHp = maxHp;
    //}
}

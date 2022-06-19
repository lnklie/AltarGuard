using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyStatus : EnemyStatus
{
    [SerializeField]
    protected RushEnemy rushEnemy = null;
    public RushEnemy RushEnemy
    {
        set { rushEnemy = value; }
    }
    [SerializeField]
    protected EquipmentController equipmentController = null;


    public override void Awake()
    {
        base.Awake();
        equipmentController = this.GetComponent<EquipmentController>();
        //if (equipmentController != null)
        //{
        //    Debug.Log("��� ��Ʈ�ѷ� ������� ���� " + equipmentController.name);
        //}
        //else
        //    Debug.Log("���������");
    }
    public override void Update()
    {
        base.Update();
        if (isEnemyChange)
        {
            Debug.Log("Ŀ���� ���ʹ� 1");
            CustomEnemy();
        }
    }
    public void CustomEnemy()
    {
        Debug.Log("Ŀ���� ���ʹ� 2");
        objectName = rushEnemy.objectName;
        str = rushEnemy.str;
        dex = rushEnemy.dex;
        wiz = rushEnemy.wiz;
        seeRange = rushEnemy.seeRange;
        defeatExp = rushEnemy.defeatExp;
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
            Debug.Log("���� �־�����?!!!!!!");
        }

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
        //isEnemyChange = false;
    }

    public void UpdateAbility()
    {
        // �ɷ� ������Ʈ
        maxHp = rushEnemy.hp + str * 10;
        maxMp = rushEnemy.mp + wiz * 10;
        physicalDamage = str * 5 + equipmentController.GetEquipmentPhysicDamage();
        magicalDamage = wiz * 5 + equipmentController.GetEquipmentMagicDamage();
        defensivePower = str * 3 + equipmentController.GetEquipmentDefensivePower();
        speed = rushEnemy.speed + dex * 0.1f;
        hpRegenValue = str * 1;
        curHp = maxHp;
    }
}

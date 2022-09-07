using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RushEnemyStatus : EnemyStatus
{
    [SerializeField] protected Enemy rushEnemy = null;


    private Image[] images = null;



    public Enemy RushEnemy { set { rushEnemy = value; } }
    public override void Awake()
    {
        base.Awake();
        images = this.gameObject.transform.parent.GetComponentsInChildren<Image>();
        images[1].canvas.worldCamera = Camera.main;
    }
    public override void Update()
    {
        base.Update();
        if (isEnemyChange && rushEnemy != null)
        {
            CustomEnemy();
        }
        if (isStateChange)
        {
            UpdateEnemyHp();
        }
    }
    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);
        UpdateEnemyHp();
    }
    public void UpdateEnemyHp()
    {
        images[1].fillAmount = curHp / (float)maxHp;
    }
    public void CustomEnemy()
    {
        objectName = rushEnemy.objectName;
        totalStr = rushEnemy.str + equipedStr;
        totalDex = rushEnemy.dex;
        totalWiz = rushEnemy.wiz;
        seeRange = rushEnemy.seeRange;
        defeatExp = rushEnemy.defeatExp;
        //equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.helmetKey));
        //equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.armorKey));
        //equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.pantKey));
        //equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(rushEnemy.weaponKey));
        UpdateAbility();
        totalAtkRange = equipedAtkRange;
        totalAtkSpeed = equipedAtkSpeed;
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

    public override void UpdateAbility()
    {
        // 능력 업데이트
        totalMaxHp = rushEnemy.hp + str * 10;
        totalMaxMp = rushEnemy.mp + wiz * 10;
        totalPhysicalDamage = str * 5 + equipedPhysicalDamage;
        totalMagicalDamage = wiz * 5 + equipedMagicalDamage;
        totalDefensivePower = str * 3 + equipedDefensivePower;
        totalSpeed = rushEnemy.speed + dex * 0.1f;
        totalHpRegenValue = str * 1;
    }
}

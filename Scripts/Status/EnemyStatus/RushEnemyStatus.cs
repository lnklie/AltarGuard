using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RushEnemyStatus : EnemyStatus
{
    [SerializeField] protected Enemy rushEnemy = null;

    private Image[] images = null;
    public Enemy RushEnemy { get { return rushEnemy; } set { rushEnemy = value; } }

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
    }
    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);
        UpdateEnemyHp();
    }
    public void UpdateEnemyHp()
    {
        images[1].fillAmount = curHp / TotalStatus[(int)EStatus.MaxHp];
    }
    public void CustomEnemy()
    {
        objectName = rushEnemy.objectName;
        seeRange = rushEnemy.seeRange;
        defeatExp = rushEnemy.defeatExp;
        totalStatus[(int)EStatus.Str] = rushEnemy.str + equipStatus[(int)EStatus.Str];
        totalStatus[(int)EStatus.Dex] = rushEnemy.dex + equipStatus[(int)EStatus.Dex];
        totalStatus[(int)EStatus.Wiz] = rushEnemy.str + equipStatus[(int)EStatus.Wiz];
        totalStatus[(int)EStatus.MaxHp] = rushEnemy.hp + totalStatus[(int)EStatus.Str] + equipStatus[(int)EStatus.MaxHp];
        totalStatus[(int)EStatus.MaxMp] = rushEnemy.mp + totalStatus[(int)EStatus.Wiz] + equipStatus[(int)EStatus.MaxMp];
        totalStatus[(int)EStatus.PhysicalDamage] = totalStatus[(int)EStatus.Str] * 5 + equipStatus[(int)EStatus.PhysicalDamage];
        totalStatus[(int)EStatus.MagicalDamage] = totalStatus[(int)EStatus.Wiz] * 5 + equipStatus[(int)EStatus.MagicalDamage];
        totalStatus[(int)EStatus.DefensivePower] = totalStatus[(int)EStatus.Str] * 3 + equipStatus[(int)EStatus.DefensivePower];
        totalStatus[(int)EStatus.Speed] = rushEnemy.speed + totalStatus[(int)EStatus.Dex] * 0.1f;
        totalStatus[(int)EStatus.HpRegenValue] = totalStatus[(int)EStatus.Str] * 0.5f;
        totalStatus[(int)EStatus.AtkRange] = rushEnemy.atkRange + totalStatus[(int)EStatus.AtkRange];
        totalStatus[(int)EStatus.AttackSpeed] = maxAtkSpeed - (rushEnemy.atkSpeed + totalStatus[(int)EStatus.AttackSpeed]);
        curHp = (int)totalStatus[(int)EStatus.MaxHp];

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

}

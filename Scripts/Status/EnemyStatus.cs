using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-11
 * 작성자 : Inklie
 * 파일명 : EnemyStatus.cs
==============================
*/
public class EnemyStatus : Status
{
    [SerializeField]
    private Enemy enemy = null;

    [SerializeField]
    private AIController aiController = null;

    [SerializeField]
    private int defeatExp = 0;
    public int DefeatExp
    {
        get { return defeatExp; }
        set { defeatExp = value; }
    }

    private int dmgCombo = 0;
    public int DmgCombo
    {
        get { return dmgCombo; }
        set { dmgCombo = value; }
    }

    private float stiffenTime = 0f;
    public float StiffenTime
    {
        get { return stiffenTime; }
        set { stiffenTime = value; }
    }

    private float maxStiffenTime = 1f;
    public float MaxStiffenTime
    {
        get { return maxStiffenTime; }
    }

    protected EnemyType enemyType;
    public EnemyType EnemyType
    {
        get { return enemyType; }
    }

    private EnemyState enemyState;
    public EnemyState EnemyState
    {
        get { return enemyState; }
        set { enemyState = value; }
    }

    private bool isKnuckBack = false;
    public bool IsKnuckBack
    {
        get { return isKnuckBack; }
        set { isKnuckBack = value; }
    }

    private List<int> itemDropKey = new List<int>();
    public List<int> ItemDropKey
    {
        get { return itemDropKey; }
        set { itemDropKey = value; }
    }

    private List<float> itemDropProb = new List<float>();
    public List<float> ItemDropProb
    {
        get { return itemDropProb; }
        set { itemDropProb = value; }
    }

    [SerializeField]
    private int damage = 0;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private bool isEnemyChange;
    public bool IsEnemyChange
    {
        get { return isEnemyChange; }
        set { isEnemyChange = value; }
    }

    private void Update()
    {
        if (isEnemyChange && enemy != null)
        {
            CustomEnemy();
        }

        if (aiController != null)
        {
            aiController.Update();
        }
    }
    public void CustomEnemy()
    {
        isEnemyChange = false;
        objectName = enemy.objectName;
        maxHp = enemy.hp;
        damage = enemy.damage;
        seeRange = enemy.seeRange;
        AtkRange = enemy.atkRange;
        speed = enemy.speed;
        atkSpeed = enemy.atkSpeed;
        defensivePower = enemy.defensivePower;
        arrowSpd = enemy.arrowSpd;
        defeatExp = enemy.defeatExp;
        enemyType = enemy.enemyType;
        
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
    }
    public AIController GetAIController()
    {
        return aiController;
    }
    public void SetAIController(AIController _aiController)
    {
        aiController = _aiController;
    }

    public Enemy GetEnemyStatus()
    {
        return enemy;
    }

    public void SetEnemyStatus(Enemy _enemy)
    {
        enemy = _enemy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : EnemyStatus.cs
==============================
*/
public class EnemyStatus : Status
{
    protected float delayTime = 0f;
    public float DelayTime
    {
        get { return delayTime; }
        set { delayTime = value; }
    }
    protected Animator ani = null;
    public Animator Ani
    {
        get { return ani; }
        set { ani = value; }
    }
    protected Rigidbody2D rig = null;
    public Rigidbody2D Rig
    {
        get { return rig; }
        set { rig = value; }
    }
    protected CircleCollider2D col = null;
    public CircleCollider2D Col
    {
        get { return col; }
        set { col = value; }
    }
    protected RaycastHit2D atkRangeRay = default;
    public RaycastHit2D AtkRangeRay
    {
        get { return atkRangeRay; }
        set { atkRangeRay = value; }
    }
    protected RaycastHit2D sightRay = default;
    public RaycastHit2D SightRay
    {
        get { return sightRay; }
        set { sightRay = value; }
    }
    protected RaycastHit2D[] allyRay = default;
    public RaycastHit2D[] AllyRay
    {
        get { return allyRay; }
        set { allyRay = value; }
    }
    protected RaycastHit2D[] enemyHitRay = default;
    public RaycastHit2D[] EnemyHitRay
    {
        get { return enemyHitRay; }
        set { enemyHitRay = value; }
    }

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
    [SerializeField]
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



    private Image[] images = null;
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private Enemy enemy = null;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        images = this.GetComponentsInChildren<Image>();
        ani = this.GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (isEnemyChange && enemy != null)
        {
            CustomEnemy();
        }
        if (isDamaged)
            UpdateEnemyHp();
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
        spriteRenderer.sprite = enemy.singleSprite;

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
    public void UpdateEnemyHp()
    {
        images[1].fillAmount = curHp / (float)maxHp;
    }


    public Enemy GetEnemyStatus()
    {
        return enemy;
    }

    public void SetEnemyStatus(Enemy _enemy)
    {
        enemy = _enemy;
        curHp = maxHp;
        IsEnemyChange = true;
    }
    public void SetAnimator(RuntimeAnimatorController _ani)
    {
        ani.runtimeAnimatorController = _ani;
    }
    public bool IsDelay()
    {
        if (delayTime >= atkSpeed)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

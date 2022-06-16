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
    [SerializeField]
    protected Animator ani;
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

    protected RaycastHit2D[] allyRay = default;
    public RaycastHit2D[] AllyRay
    {
        get { return allyRay; }
        set { allyRay = value; }
    }
    protected RaycastHit2D enemyHitRay = default;
    public RaycastHit2D EnemyHitRay
    {
        get { return enemyHitRay; }
        set { enemyHitRay = value; }
    }

    protected int defeatExp = 0;
    public int DefeatExp
    {
        get { return defeatExp; }
        set { defeatExp = value; }
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

    protected List<int> itemDropKey = new List<int>();
    public List<int> ItemDropKey
    {
        get { return itemDropKey; }
        set { itemDropKey = value; }
    }

    protected List<float> itemDropProb = new List<float>();
    public List<float> ItemDropProb
    {
        get { return itemDropProb; }
        set { itemDropProb = value; }
    }

    protected bool isEnemyChange;
    public bool IsEnemyChange
    {
        get { return isEnemyChange; }
        set { isEnemyChange = value; }
    }
    protected bool isAtk = false;
    public bool IsAtk
    {
        get { return isAtk; }
        set { isAtk = value; }
    }

    private Image[] images = null;

    [SerializeField]
    private TextMesh textMesh = null;

    public virtual void Awake()
    {
        ani = this.GetComponentInChildren<Animator>();
        images = this.GetComponentsInChildren<Image>();
        rig = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CircleCollider2D>();
        textMesh = this.GetComponentInChildren<TextMesh>();
        images[1].canvas.worldCamera = Camera.main;
    }
    public virtual void Update()
    {
        if (isDamaged)
            UpdateEnemyHp();
        textMesh.text = enemyState.ToString();
    }

    public void UpdateEnemyHp()
    {
        images[1].fillAmount = curHp / (float)maxHp;
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

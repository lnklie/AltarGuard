using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-11
 * 작성자 : Inklie
 * 파일명 : Status.cs
==============================
*/
public class Status : MonoBehaviour
{
    [SerializeField]
    protected string objectName = "";
    [SerializeField]
    protected int curHp = 0;
    [SerializeField]
    protected int maxHp = 100;
    [SerializeField]
    protected int defensivePower = 0;
    [SerializeField]
    protected bool isDamaged = false;
    [SerializeField]
    protected bool isStateChange = false;
    protected BoxCollider2D col = null;
    protected Rigidbody2D rig = null;
    protected Animator ani = null;
    [SerializeField]
    protected Transform targetPos = null;
    [SerializeField]
    private DamageTextController damageTextController;
    [SerializeField]
    protected int defeatExp = 0;
    private SpriteRenderer bodySprites = null;
    #region Property
    public int DefeatExp
    {
        get { return defeatExp; }
        set { defeatExp = value; }
    }
    public Transform TargetPos
    {
        get { return targetPos; }
    }
    public string ObjectName
    {
        get { return objectName; }
        set { objectName = value; }
    }
    public int CurHp
    {
        get { return curHp; }
        set { curHp = value; }
    }
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    public int DefensivePower
    {
        get { return defensivePower; }
        set { defensivePower = value; }
    }
    public bool IsDamaged
    {
        get { return isDamaged; }
        set { isDamaged = value; }
    }
    public bool IsStateChange
    {
        get { return isStateChange; }
        set { isStateChange = value; }
    }
    public BoxCollider2D Col
    {
        get { return col; }
    }
    public Rigidbody2D Rig
    {
        get { return rig; }
    }
    public Animator Ani
    {
        get { return ani; }
    }
    #endregion



    public virtual void Awake()
    {
        col = this.GetComponent<BoxCollider2D>();
        rig = this.GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }
    public virtual void Update()
    {
        if (isStateChange)
            isStateChange = false;
    }
    public void SetDamageText(int _damage)
    {
        damageTextController.SetDamageText(_damage);
    }
    public int ReviseDamage(int _damage, int _depensivePower)
    {
        return Mathf.CeilToInt(_damage * (1f / (1 + _depensivePower)));
    }
    public bool IsLastHit()
    {
        // 매개변수가 마지막 공격을 했는지 체크
        if (curHp <= 0f)
            return true;
        else
            return false;
    }
    public virtual void Damaged(int _damage)
    {
        curHp -= ReviseDamage(_damage, defensivePower);
        StartCoroutine(Blink());
        SetDamageText(ReviseDamage(_damage, defensivePower));
    }
    private IEnumerator Blink()
    {
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }
    public void ActiveLayer(LayerName layerName)
    {
        // 애니메이션 레이어 가중치 조절
        for (int i = 0; i < ani.layerCount; i++)
        {
            ani.SetLayerWeight(i, 0);
        }
        ani.SetLayerWeight((int)layerName, 1);
    }
}

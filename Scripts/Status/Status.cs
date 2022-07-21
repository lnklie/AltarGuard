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
    protected int maxHp = 0;
    [SerializeField]
    protected int defensivePower = 0;
    [SerializeField]
    protected bool isDamaged = false;
    protected bool isStateChange = false;
    protected CapsuleCollider2D col = null;
    protected Rigidbody2D rig = null;
    protected Animator ani = null;
    [SerializeField]
    protected Transform targetPos = null;
    
    #region Property
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
    public CapsuleCollider2D Col
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
        col = this.GetComponent<CapsuleCollider2D>();
        rig = this.GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();

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

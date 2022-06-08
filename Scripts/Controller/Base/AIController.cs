using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : AIController.cs
==============================
*/
public abstract class AIController : BaseController
{
    [Header("Attack Delay")]
    [SerializeField]
    protected float delayTime = 0f;

    protected Animator ani = null;
    protected Rigidbody2D rig = null;
    protected TextMesh txtMesh = null;

    protected Vector2 dir = new Vector2(0, 0);
    protected Vector2 distance = new Vector2(0, 0);
    protected RaycastHit2D atkRange = default;
    protected RaycastHit2D sight = default;
    protected RaycastHit2D[] allyRay = default;
    protected RaycastHit2D[] enemyHit = default;



    public virtual void Awake()
    {
        ani = this.GetComponentInChildren<Animator>();
        rig = this.GetComponent<Rigidbody2D>();
        txtMesh = this.GetComponentInChildren<TextMesh>();

    }

    public virtual void Update()
    {
        Perception();
        ChangeState();
        State();
    }
    public abstract void ChangeState();
    public abstract void State();
    public abstract void Perception();
    public abstract void Idle();
    public abstract void Chase();
    public abstract void Attack();
    public abstract IEnumerator Died();
    public abstract bool IsDelay();
    public abstract void Damaged();
    public abstract bool IsDied();

    public void ActiveLayer(LayerName layerName)
    {
        // 애니메이션 레이어 가중치 조절
        for (int i = 0; i < ani.layerCount; i++)
        {
            ani.SetLayerWeight(i, 0);
        }
        ani.SetLayerWeight((int)layerName, 1);
    }

    public void SetDelayTime(float _time)
    {
        // 딜레이타임 세팅
        delayTime = _time;
    }
}

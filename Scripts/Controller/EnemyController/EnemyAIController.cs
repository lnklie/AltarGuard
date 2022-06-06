using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : EnemyAIController.cs
==============================
*/
public class EnemyAIController : AIController
{
    protected EnemyStatus enemy = null;
    protected DropManager dropController = null;
    protected CircleCollider2D col = null;

    private Image[] images = null;
    public override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyStatus>();
        dropController = GetComponent<DropManager>();
        col = GetComponent<CircleCollider2D>();
        images = this.GetComponentsInChildren<Image>();
    }
    public override void Update()
    {
        base.Update();
        if (isDamaged)
            UpdateEnemyHp();
    }
    public override void ChangeState()
    {}

    public override void State() 
    {}
    public override void Chase()
    {
        isStateChange = false;
        ActiveLayer(LayerName.WalkLayer);
        rig.velocity = enemy.Speed * dir;
    }

    public override void Damaged()
    {
        isStateChange = false;
        enemy.DmgCombo++;
        enemy.StiffenTime = 0f;
        isDamaged = true;
    }

    public override IEnumerator Died()
    {
        isStateChange = false;
        SetEnabled(false);
        rig.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        DropManager.Instance.DropItem(this.transform.position, enemy.ItemDropKey, enemy.ItemDropProb);
        EnemySpawner.Instance.ReturnEnemy(this.gameObject,enemy.EnemyType);
        SetEnabled(true);
        enemy.EnemyState = EnemyState.Idle;
        enemy.CurHp = enemy.MaxHp;
    }

    public override void Idle()
    {
        isStateChange = false;
        ActiveLayer(LayerName.IdleLayer);
        rig.velocity = Vector2.zero;
    }

    public override bool IsDelay()
    {
        if (delayTime >= enemy.AtkSpeed)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public virtual void Stiffen()
    {
        enemy.StiffenTime += Time.deltaTime;
        if (enemy.StiffenTime >= enemy.MaxStiffenTime)
        {
            enemy.DmgCombo = 0;
            isDamaged = false;
        }
    }
    public override void Perception()
    {
        // 레이를 이용한 인식
        AnimationDirection();
        sight = Physics2D.CircleCast(this.transform.position, enemy.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        atkRange = Physics2D.CircleCast(this.transform.position, enemy.AtkRange, dir, 0, LayerMask.GetMask("Ally"));
        allyRay = Physics2D.CircleCastAll(this.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Ally"));
        GameObject altar = null;
        for(int i =0; i < allyRay.Length; i++)
        {
            if (allyRay[i].collider.gameObject.CompareTag("Altar"))
                altar = allyRay[i].collider.gameObject;
        }

        if (!altar)
            enemy.Target = null;
        else
        {
            if (sight)
                enemy.Target = sight.collider.gameObject;
            else
                enemy.Target = altar;
        }
    }
    public void UpdateEnemyHp()
    {
        images[1].fillAmount = enemy.CurHp / enemy.MaxHp;
    }
    public override bool IsDied()
    {
        if (enemy.CurHp <= 0)
            return true;
        else
            return false;
    }
    public override void Attack()
    {
        ActiveLayer(LayerName.AttackLayer);
        rig.velocity = Vector2.zero;
        delayTime += Time.deltaTime;
    }
    public void SetEnabled(bool _boolean)
    {
        col.enabled = _boolean;
    }
    public virtual void AnimationDirection()
    {
        // 애니메이션 방향
        if (dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < 0) transform.transform.localScale = new Vector3(1, 1, 1);
    }
}

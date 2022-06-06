using System.Collections;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : MercenaryAIController.cs
==============================
*/
public class MercenaryAIController : AIController
{
    protected CharacterStatus status = null;
    protected BoxCollider2D col = null;
    protected bool isAtk = false;

    private SpriteRenderer bodySprites = null;
    public bool IsAtk
    {
        set { isAtk = value; }
    }

    private float revivalTime = 5f;
    public float RevivalTime
    {
        get { return revivalTime; }
        set { revivalTime = value; }
    }

    private float knuckBackPower = 1;
    public float KnuckBackPower
    {
        get { return knuckBackPower; }
        set { knuckBackPower = value; }
    }

    [SerializeField]
    private CharacterState characterState = CharacterState.Idle;

    public override void Awake()
    {
        base.Awake();
        status = this.GetComponent<CharacterStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
        col = this.GetComponent<BoxCollider2D>();
    }
    public override void State()
    {
        txtMesh.text = characterState.ToString();
        switch (characterState)
        {
            case CharacterState.Idle:
                Idle();
                break;
            case CharacterState.Walk:
                Chase();
                break;
            case CharacterState.Attack:
                Attack();
                break;
            case CharacterState.Damaged:
                Damaged ();
                break;
            case CharacterState.Died:
                StartCoroutine(Died());
                break;
        }
    }
    public override void ChangeState()  
    {
        if (sight)
            distance = status.Target.transform.position - this.transform.position;
        dir = distance.normalized;
        if (status.CurHp < 0f)
        {
            characterState = CharacterState.Died;
        }
        else
        {
            if (isDamaged)
            {
                characterState = CharacterState.Damaged;
            }
            else
            {
                if (status.Target == null)
                {
                    characterState = CharacterState.Idle;
                }
                else
                {
                    characterState = CharacterState.Walk;
                    if (atkRange)
                    {
                        characterState = CharacterState.Attack;
                        if (sight.collider.CompareTag("Boss"))
                        {

                            if (CheckBossState() != BossState.PatternAttack)
                                characterState = CharacterState.Idle;
                        }
                    }
                }
            }
        }
    }

    public override void Chase()
    {
        ActiveLayer(LayerName.WalkLayer);
        rig.velocity = status.Speed * dir;
    }

    public override IEnumerator Died()
    {
        rig.velocity = Vector2.zero;
        col.enabled = false;

        ActiveLayer(LayerName.DieLayer);
        yield return new WaitForSeconds(revivalTime);
        Rivive();
    }
    private void Rivive()
    {
        rig.isKinematic = false;
        col.enabled = true;
        status.CurHp = status.MaxHp;
        characterState = CharacterState.Idle;
    }
    public override void Idle()
    {
        ActiveLayer(LayerName.IdleLayer);
        rig.velocity = Vector2.zero;
    }
    public bool IsLastHit(EnemyStatus _enemy)
    {
        // 마지막 공격을 했는지 체크
        if (isAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }
    public override void Perception()
    {
        AnimationDirection();
        sight = Physics2D.CircleCast(this.transform.position, status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        allyRay = Physics2D.CircleCastAll(this.transform.position, status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        atkRange = Physics2D.CircleCast(this.transform.position, status.AtkRange, dir, 0, LayerMask.GetMask("Enemy"));
        if (!sight)
            status.Target = null;
        else
        {
            status.Target = sight.collider.gameObject;
        }
    }
    public BossState CheckBossState()
    {
        return sight.rigidbody.GetComponent<BossEnemyAIController>().BossState;
    }
    public int AttackTypeDamage()
    {
        if (attackType < 1f)
            return status.PhysicalDamage;
        else
            return status.MagicalDamage;
    }
    public override void Attack()
    {
        ActiveLayer(LayerName.AttackLayer);
        ani.SetFloat("AtkType", attackType);
        delayTime += Time.deltaTime;
        rig.velocity = Vector2.zero;
    }

    public override bool IsDelay()
    {
        if (delayTime >= status.AtkSpeed)
        {
            delayTime = status.AtkSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsDied()
    {
        if (status.CurHp <= 0)
            return true;
        else
            return false;
    }

    public override void Damaged()
    {
        StartCoroutine(Blink());
    }
    private IEnumerator Blink()
    {
        isDamaged = false;
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }
    public void AnimationDirection()
    {
        if (dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rig.AddForce(-direction * knockbackPower);
        }
        yield return 0;
    }
}

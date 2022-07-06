using System.Collections;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : MercenaryAIController.cs
==============================
*/
public class MercenaryAIController : BaseController , IAIController
{
    protected MercenaryStatus mercenary = null;
    protected CapsuleCollider2D col = null;
    protected Rigidbody2D rig = null;
    protected bool isAtk = false;
    protected Animator ani = null; 
    private SpriteRenderer bodySprites = null;

    private RaycastHit2D sightRay = default;

    private RaycastHit2D[] allyRay = default;
    private Vector2 distance = Vector2.zero;

    [Header("Attack Delay")]
    [SerializeField]
    protected float delayTime = 0f;
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



    public  void Awake()
    {
        mercenary = this.GetComponent<MercenaryStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
        col = this.GetComponent<CapsuleCollider2D>();
        ani = this.GetComponentInChildren<Animator>();
        rig = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        State(mercenary);
        ChangeState(mercenary);
        Perception(mercenary);
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

    private void Rivive()
    {
        rig.isKinematic = false;
        col.enabled = true;
        mercenary.CurHp = mercenary.MaxHp;
        mercenary.AIState = AIState.Idle;
    }
    public void Idle()
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
    public  void Perception()
    {

    }
    public AIState CheckBossState()
    {
        return sightRay.rigidbody.GetComponent<EnemyStatus>().AIState;
    }
    public int AttackTypeDamage()
    {
        if (mercenary.AttackType < 1f)
            return mercenary.PhysicalDamage;
        else
            return mercenary.MagicalDamage;
    }

    public RaycastHit2D[] AttackRange()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 1f, mercenary.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, mercenary.Dir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].rigidbody.GetComponent<EnemyStatus>().AIState = AIState.Damaged;
            }
        }
        else
            Debug.Log("아무것도 없음");
        return hits;
    }
    public void EnemyAttack(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();

            enemy.CurHp -= ReviseDamage(AttackTypeDamage(), enemy.DefensivePower);
            isAtk = true;
            if (IsLastHit(enemy))
            {
                Debug.Log("막타 경험치 확득");
                mercenary.CurExp += enemy.DefeatExp;
            }
        }
    }
    public bool IsDelay()
    {
        if (delayTime >= mercenary.AtkSpeed)
        {
            delayTime = mercenary.AtkSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }


    private IEnumerator Blink()
    {
        mercenary.IsDamaged = false;
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }
    public void AnimationDirection()
    {
        if (mercenary.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (mercenary.Dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
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
    private void ShotArrow()
    {
        // 활쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(mercenary, AttackTypeDamage());

        }
        else
            Debug.Log("화살 없음");
    }

    public void ChangeState(CharacterStatus _status)
    {
        if (sightRay)
            distance = mercenary.Target.transform.position - this.transform.position;
        mercenary.Dir = distance.normalized;
        if (mercenary.CurHp < 0f)
        {
            _status.AIState = AIState.Died;
        }
        else
        {
            if (mercenary.IsDamaged)
            {
                _status.AIState = AIState.Damaged;
            }
            else
            {
                if (mercenary.Target == null)
                {
                    _status.AIState = AIState.Idle;
                }
                else
                {
                    _status.AIState = AIState.Walk;
                    if (_status.HitRay)
                    {
                        _status.AIState = AIState.Attack;
                        if (sightRay.collider.CompareTag("Boss"))
                        {

                            if (CheckBossState() != AIState.Attack)
                                _status.AIState = AIState.Idle;
                        }
                    }
                }
            }
        }
    }

    public void State(CharacterStatus _status)
    {
        switch (_status.AIState)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Walk:
                Chase(_status);
                break;
            case AIState.Attack:
                Attack(_status);
                break;
            case AIState.Damaged:
                Damaged(_status);
                break;
            case AIState.Died:
                StartCoroutine(Died(_status));
                break;
        }
    }

    public void Perception(CharacterStatus _status)
    {
        AnimationDirection();
        _status.SightRay = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        _status.HitRay = Physics2D.CircleCast(this.transform.position, _status.AtkRange, _status.Dir, 0, LayerMask.GetMask("Enemy"));
        allyRay = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (!sightRay)
            _status.Target = null;
        else
        {
            _status.Target = sightRay.collider.gameObject;
        }
    }

    public void Idle(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public void Chase(CharacterStatus _status)
    {
        ActiveLayer(LayerName.WalkLayer);
        _status.Rig.velocity = _status.Speed * _status.Dir;
    }

    public void Attack(CharacterStatus _status)
    {
        ActiveLayer(LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", mercenary.AttackType);
        _status.DelayTime += Time.deltaTime;
        _status.Rig.velocity = Vector2.zero;
        if (_status.AttackType == 0f)
        {
            if (!IsDelay())
            {
                _status.Ani.SetTrigger("AtkTrigger");
                delayTime = 0f;
                EnemyAttack(AttackRange());
            }
            else
                isAtk = false;
        }
        else if (_status.AttackType == 0.5f)
        {
            if (!IsDelay())
            {
                _status.Ani.SetTrigger("AtkTrigger");
                Debug.Log("공격");
                _status.DelayTime = 0f;
                if (_status.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
                {
                    Debug.Log("쐈다");
                    ShotArrow();
                }
            }
            else
                isAtk = false;
        }
        else
        {
            if (!IsDelay())
            {
                _status.Ani.SetTrigger("AtkTrigger");
                delayTime = 0f;
                isAtk = true;
            }
            else
                isAtk = false;
        }
    }

    public IEnumerator Died(CharacterStatus _status)
    {
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;

        ActiveLayer(LayerName.DieLayer);
        yield return new WaitForSeconds(revivalTime);
        Rivive();
    }

    public void Damaged(CharacterStatus _status)
    {
        StartCoroutine(Blink());
        _status.IsStatusUpdate = true;
    }

    public bool IsDied(CharacterStatus _status)
    {
        if (_status.CurHp <= 0)
            return true;
        else
            return false;
    }
}

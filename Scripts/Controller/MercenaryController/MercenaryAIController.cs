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
        AIState(mercenary);
        AIChangeState(mercenary);
        AIPerception(mercenary);
    }


    private void Rivive()
    {
        rig.isKinematic = false;
        col.enabled = true;
        mercenary.CurHp = mercenary.MaxHp;
        mercenary.AIState = global::AIState.Idle;
    }
    public bool IsLastHit(EnemyStatus _enemy)
    {
        // 마지막 공격을 했는지 체크
        if (isAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }
    public AIState CheckBossState()
    {
        return sightRay.rigidbody.GetComponent<EnemyStatus>().AIState;
    }
    public int AttackTypeDamage(CharacterStatus _status)
    {
        if (_status.AttackType < 1f)
            return _status.PhysicalDamage;
        else
            return _status.MagicalDamage;
    }

    public RaycastHit2D[] AttackRange(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 1f, _status.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, _status.Dir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].rigidbody.GetComponent<EnemyStatus>().AIState = global::AIState.Damaged;
            }
        }
        else
            Debug.Log("아무것도 없음");
        return hits;
    }
    public void DamageEnemy(RaycastHit2D[] hits, CharacterStatus _status)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();

            enemy.CurHp -= ReviseDamage(AttackTypeDamage(_status), enemy.DefensivePower);
            isAtk = true;
            if (IsLastHit(enemy))
            {
                Debug.Log("막타 경험치 확득");
                mercenary.CurExp += enemy.DefeatExp;
            }
        }
    }
    public bool IsDelay(CharacterStatus _status)
    {
        if (_status.DelayTime >= _status.AtkSpeed)
        {
            _status.DelayTime = _status.AtkSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }


    private IEnumerator Blink(CharacterStatus _status)
    {
        _status.IsDamaged = false;
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }
    public void AnimationDirection(CharacterStatus _status)
    {
        if (_status.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (_status.Dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
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
    private void ShotArrow(CharacterStatus _status)
    {
        // 활쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(mercenary, AttackTypeDamage(_status));

        }
        else
            Debug.Log("화살 없음");
    }

    public void AIChangeState(CharacterStatus _status)
    {

        if (_status.SightRay.Count > 0)
        {
            _status.Distance = _status.Target.transform.position - this.transform.position;
            _status.Dir = _status.Distance.normalized;
        }
        
        if (_status.CurHp < 0f)
        {
            _status.AIState = global::AIState.Died;
        }
        else
        {
            if (_status.IsDamaged)
            {
                _status.AIState = global::AIState.Damaged;
            }
            else
            {
                if (_status.Target == null)
                {
                    _status.AIState = global::AIState.Idle;
                }
                else
                {
                    _status.AIState = global::AIState.Walk;
                    if (_status.HitRay)
                    {
                        _status.AIState = global::AIState.Attack;
                    }
                }
            }
        }
    }

    public void AIState(CharacterStatus _status)
    {
        switch (_status.AIState)
        {
            case global::AIState.Idle:
                AIIdle(_status);
                break;
            case global::AIState.Walk:
                AIChase(_status);
                break;
            case global::AIState.Attack:
                AIAttack(_status);
                break;
            case global::AIState.Damaged:
                AIDamaged(_status);
                break;
            case global::AIState.Died:
                StartCoroutine(AIDied(_status));
                break;
        }
    }

    public void AIPerception(CharacterStatus _status)
    {
        AnimationDirection(_status);
        _status.SightRay.AddRange(Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy")));
        _status.AllyRay.AddRange(Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally")));
        _status.HitRay = Physics2D.CircleCast(this.transform.position, _status.AtkRange, _status.Dir, 0, LayerMask.GetMask("Enemy"));
        if (_status.SightRay.Count < 1)
            _status.Target = null;
        else
        {
            _status.Target = _status.SightRay[0].collider.gameObject;
        }
    }

    public void AIIdle(CharacterStatus _status)
    {
        ActiveLayer(LayerName.IdleLayer, _status);
        rig.velocity = Vector2.zero;
    }

    public void AIChase(CharacterStatus _status)
    {
        ActiveLayer(LayerName.WalkLayer, _status);
        _status.Rig.velocity = _status.Speed * _status.Dir;
    }

    public void AIAttack(CharacterStatus _status)
    {
        ActiveLayer(LayerName.AttackLayer, _status);
        _status.Ani.SetFloat("AtkType", _status.AttackType);
        _status.DelayTime += Time.deltaTime;
        _status.Rig.velocity = Vector2.zero;
        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            _status.DelayTime = 0f;
            if (_status.AttackType == 0f)
            {

                DamageEnemy(AttackRange(_status), _status);

            }
            else if (_status.AttackType == 0.5f)
            {
                if (_status.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
                {
                    Debug.Log("쐈다");
                    ShotArrow(_status);
                }
            }
            else
            {
                _status.IsAtk = true;
            }
        }
        else
            _status.IsAtk = false;
    }

    public IEnumerator AIDied(CharacterStatus _status)
    {
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;

        ActiveLayer(LayerName.DieLayer, _status);
        yield return new WaitForSeconds(revivalTime);
        Rivive();
    }

    public void AIDamaged(CharacterStatus _status)
    {
        StartCoroutine(Blink(_status));
        _status.IsStatusUpdate = true;
    }

    public bool AIIsDied(CharacterStatus _status)
    {
        if (_status.CurHp <= 0)
            return true;
        else
            return false;
    }
}

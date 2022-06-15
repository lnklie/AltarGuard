using System.Collections;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : MercenaryAIController.cs
==============================
*/
public class MercenaryAIController : BaseController
{
    protected CharacterStatus character = null;
    protected BoxCollider2D col = null;
    protected Rigidbody2D rig = null;
    protected bool isAtk = false;
    protected Animator ani = null; 
    private SpriteRenderer bodySprites = null;

    private RaycastHit2D sightRay = default;
    private RaycastHit2D atkRangeRay = default;
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

    [SerializeField]
    private CharacterState characterState = CharacterState.Idle;

    public  void Awake()
    {
        character = this.GetComponent<CharacterStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
        col = this.GetComponent<BoxCollider2D>();
        ani = this.GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        State();
        ChangeState();
        Perception();
    }
    public void State()
    {
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
    public void ChangeState()  
    {
        if (sightRay)
            distance = character.Target.transform.position - this.transform.position;
        character.Dir = distance.normalized;
        if (character.CurHp < 0f)
        {
            characterState = CharacterState.Died;
        }
        else
        {
            if (character.IsDamaged)
            {
                characterState = CharacterState.Damaged;
            }
            else
            {
                if (character.Target == null)
                {
                    characterState = CharacterState.Idle;
                }
                else
                {
                    characterState = CharacterState.Walk;
                    if (atkRangeRay)
                    {
                        characterState = CharacterState.Attack;
                        if (sightRay.collider.CompareTag("Boss"))
                        {

                            if (CheckBossState() != EnemyState.Attack)
                                characterState = CharacterState.Idle;
                        }
                    }
                }
            }
        }
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
    public void Chase()
    {
        ActiveLayer(LayerName.WalkLayer);
        rig.velocity = character.Speed * character.Dir;
    }

    public IEnumerator Died()
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
        character.CurHp = character.MaxHp;
        characterState = CharacterState.Idle;
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
        AnimationDirection();
        sightRay = Physics2D.CircleCast(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        atkRangeRay = Physics2D.CircleCast(this.transform.position, character.AtkRange, character.Dir, 0, LayerMask.GetMask("Enemy"));
        allyRay = Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (!sightRay)
            character.Target = null;
        else
        {
            character.Target = sightRay.collider.gameObject;
        }
    }
    public EnemyState CheckBossState()
    {
        return sightRay.rigidbody.GetComponent<EnemyStatus>().EnemyState;
    }
    public int AttackTypeDamage()
    {
        if (attackType < 1f)
            return character.PhysicalDamage;
        else
            return character.MagicalDamage;
    }
    public virtual void Attack()
    {
        ActiveLayer(LayerName.AttackLayer);
        ani.SetFloat("AtkType", attackType);
        delayTime += Time.deltaTime;
        rig.velocity = Vector2.zero;
        if (attackType == 0f)
        {
            if (!IsDelay())
            {
                ani.SetTrigger("AtkTrigger");
                delayTime = 0f;
                EnemyAttack(AttackRange());
            }
            else
                isAtk = false;
        }
        else if(attackType == 0.5f)
        {
            if (!IsDelay())
            {
                ani.SetTrigger("AtkTrigger");
                Debug.Log("공격");
                delayTime = 0f;
                if (ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
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
                ani.SetTrigger("AtkTrigger");
                delayTime = 0f;
                isAtk = true;
            }
            else
                isAtk = false;
        }
    }
    public RaycastHit2D[] AttackRange()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 1f, character.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, character.Dir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].rigidbody.GetComponent<EnemyStatus>().EnemyState = EnemyState.Damaged;
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

            enemy.CurHp -= AttackTypeDamage();
            isAtk = true;
            if (IsLastHit(enemy))
            {
                Debug.Log("막타 경험치 확득");
                character.CurExp += enemy.DefeatExp;
            }
        }
    }
    public bool IsDelay()
    {
        if (delayTime >= character.AtkSpeed)
        {
            delayTime = character.AtkSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsDied()
    {
        if (character.CurHp <= 0)
            return true;
        else
            return false;
    }

    public void Damaged()
    {
        StartCoroutine(Blink());
    }
    private IEnumerator Blink()
    {
        character.IsDamaged = false;
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }
    public void AnimationDirection()
    {
        if (character.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (character.Dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
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
            ProjectionSpawner.Instance.ShotArrow(character, AttackTypeDamage());

        }
        else
            Debug.Log("화살 없음");
    }
}

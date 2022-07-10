using System.Collections;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : MercenaryController.cs
==============================
*/
public class MercenaryController : CharacterController
{
    protected MercenaryStatus mercenary = null;
    private SpriteRenderer bodySprites = null;
    private Vector2 distance = Vector2.zero;

    [Header("Attack Delay")]
    [SerializeField]
    protected float delayTime = 0f;

    public override  void Awake()
    {
        mercenary = this.GetComponent<MercenaryStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }

    private void Rivive(CharacterStatus _Status)
    {
        _Status.Rig.isKinematic = false;
        _Status.Col.enabled = true;
        _Status.CurHp = mercenary.MaxHp;
        _Status.AIState = global::EAIState.Idle;
    }

    public bool IsLastHit(EnemyStatus _enemy, CharacterStatus _Status)
    {
        // ������ ������ �ߴ��� üũ
        if (_Status.IsAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }

    public EAIState CheckBossState(CharacterStatus _Status)
    {
        return _Status.SightRay[0].rigidbody.GetComponent<EnemyStatus>().AIState;
    }


    public override RaycastHit2D[] AttackRange(CharacterStatus _Status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 1f, _Status.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, _Status.Dir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].rigidbody.GetComponent<EnemyStatus>().AIState = EAIState.Damaged;
            }
        }
        else
            Debug.Log("�ƹ��͵� ����");
        return hits;
    }
    public override void AttackDamage(RaycastHit2D[] hits, CharacterStatus _status)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();

            enemy.CurHp -= ReviseDamage(AttackTypeDamage(_status), enemy.DefensivePower);
            _status.IsAtk = true;
            if (IsLastHit(enemy, _status))
            {
                Debug.Log("��Ÿ ����ġ Ȯ��");
                _status.CurExp += enemy.DefeatExp;
            }
        }
    }


    private IEnumerator Blink(CharacterStatus _status)
    {
        _status.IsDamaged = false;
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }

    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.SightRay[0])
        {
            _status.Distance = _status.Target.transform.position - this.transform.position;
            _status.Dir = _status.Distance.normalized;
        }
        if (_status.CurHp < 0f)
        {
            _status.AIState = EAIState.Died;
        }
        else
        {
            if (_status.IsDamaged)
            {
                _status.AIState = EAIState.Damaged;
            }
            else
            {
                if (_status.Target == null)
                {
                    _status.AIState = EAIState.Idle;
                }
                else
                {
                    _status.AIState = EAIState.Walk;
                    if (_status.HitRay)
                    {
                        _status.AIState = EAIState.Attack;
                    }
                }
            }
        }
    }



    public override void AIPerception(CharacterStatus _status)
    {
        _status.SightRay.AddRange(Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy")));
        SortSightRayList(_status.SightRay);
        _status.AllyRay.AddRange(Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally")));
        _status.HitRay = Physics2D.CircleCast(this.transform.position, _status.AtkRange, _status.Dir, 0, LayerMask.GetMask("Enemy"));
        if (!_status.SightRay[0])
            _status.Target = null;
        else
        {
            _status.Target = _status.SightRay[0].collider.gameObject;
        }
    }




    public override IEnumerator AIDied(CharacterStatus _status)
    {
        base.AIDied(_status);
        yield return new WaitForSeconds(mercenary.RevivalTime);
        Rivive(_status);
    }

    public override void AIDamaged(CharacterStatus _status)
    {
        base.AIDamaged(_status);
        StartCoroutine(Blink(_status));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : BaseController, IAIController
{
    [SerializeField]
    protected SkillController skillController = null;
    [SerializeField]
    protected CharacterStatus characterStatus = null;


    public override void Awake()
    {
        base.Awake();
        characterStatus = this.GetComponent<CharacterStatus>();
    }
    public virtual void Update()
    {
        AIPerception(characterStatus);
        AIChangeState(characterStatus);
        AIState(characterStatus);
    }

    public bool CheckRayList(RaycastHit2D _RayHit, List<RaycastHit2D> _RayList)
    {
        bool _bool = false;
        for (int i = 0; i < _RayList.Count; i++)
        {
            if (_RayHit == _RayList[i])
                _bool = true;
            else
                _bool = false;
        }
        return _bool;
    }
    public void SortSightRayList(List<RaycastHit2D> _sightRay)
    {
        // 리스트 정렬
        _sightRay.Sort(delegate (RaycastHit2D a, RaycastHit2D b)
        {
            if (GetDistance(this.transform.position, a.rigidbody.position) < GetDistance(this.transform.position, b.rigidbody.position)) return -1;
            else if (GetDistance(this.transform.position, a.rigidbody.position) > GetDistance(this.transform.position, b.rigidbody.position)) return 1;
            else return 0;

        });
    }

    public void AnimationDirection(CharacterStatus _status)
    {
        if (_status.AIState != EAIState.Died)
        {
            if (_status.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
            else if (_status.Dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj, CharacterStatus _status)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            _status.Rig.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
    public bool IsDelay(CharacterStatus _status)
    {
        float atkSpeed = _status.AtkSpeed;
        if (_status.DelayTime >= atkSpeed)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public int AttackTypeDamage(CharacterStatus _status)
    {
        if (_status.AttackType < 1f)
            return _status.PhysicalDamage;
        else
            return _status.MagicalDamage;
    }
    public void ShotArrow(CharacterStatus _status)
    {
        // 활쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(_status, AttackTypeDamage(_status));

        }
        else
            Debug.Log("화살 없음");
    }
    public bool IsDied(CharacterStatus _status)
    {
        if (_status.CurHp <= 0)
            return true;
        else
            return false;
    }

    public virtual void AttackByAttackType(CharacterStatus _status)
    {
        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            _status.DelayTime = 0f;
            _status.IsAtk = true;

            if (_status.AttackType == 0)
            {
                AttackDamage(AttackRange(_status), _status);
            }
            else if (_status.AttackType == 0.5f)
            {
                if (_status.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
                {
                    ShotArrow(_status);
                }
            }
            else
            {


            }
        }
        else
            _status.IsAtk = false;

    }
    public virtual RaycastHit2D[] AttackRange(CharacterStatus _status)
    {
        //
        return null;
    }
    public virtual void AttackDamage(RaycastHit2D[] hits, CharacterStatus _status)
    {
        //
    }
    #region AI
    public virtual void AIChangeState(CharacterStatus _status)
    {
        //
    }
    public virtual void AIState(CharacterStatus _status)
    {
        AnimationDirection(_status);
        switch (_status.AIState)
        {
            case EAIState.Idle:
                AIIdle(_status);
                break;
            case EAIState.Walk:
                AIChase(_status);
                break;
            case EAIState.Attack:
                AIAttack(_status);
                break;
            case EAIState.Damaged:
                AIDamaged(_status);
                break;
            case EAIState.Died:
                StartCoroutine(AIDied(_status));
                break;
        }
    }
    public virtual void AIPerception(CharacterStatus _status)
    {
        //
    }
    public virtual void AIIdle(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.IdleLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = Vector2.zero;
    }

    public virtual void AIChase(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.WalkLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = _status.Speed * _status.Dir;
    }
    public virtual void AIAttack(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", _status.AttackType);
        _status.Rig.velocity = Vector2.zero;
        _status.DelayTime += Time.deltaTime;
        AttackByAttackType(_status);
    }
    public virtual void AIDamaged(CharacterStatus _status)
    {
        _status.IsStatusUpdate = true;
    }
    public virtual IEnumerator AIDied(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.IdleLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        yield return null;
    }
    #endregion
}

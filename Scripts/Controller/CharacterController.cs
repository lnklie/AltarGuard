using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : BaseController, IAIController
{
    [SerializeField] protected SkillController skillController = null;
    [SerializeField] protected CharacterStatus characterStatus = null;
    [SerializeField] protected PathFindController pathFindController = null;
    
    public override void Awake()
    {
        characterStatus = this.GetComponent<CharacterStatus>();

    }
    public virtual void Start()
    {
        StartCoroutine(FindPath());
    }
    public virtual void Update()
    {
        AIPerception(characterStatus);
        AIChangeState(characterStatus);
        AIState(characterStatus);
    }
    public IEnumerator FindPath()
    {
        while(true)
        {
            pathFindController.PathFinding();
            yield return new WaitForSeconds(Random.Range(0.3f,0.4f));
        }
    }
    public WaitUntil WaitUntilAnimatorPoint(Animator _animator, int _index, string _aniName, float _point)
    {
        return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(_index).IsName(_aniName) &&
            _animator.GetCurrentAnimatorStateInfo(_index).normalizedTime >= _point);
    }
    public bool IsAtkRange(CharacterStatus _status)
    {
        if (GetDistance(_status.transform.position, _status.Target.transform.position) <= _status.TotalAtkRange)
            return true;
        else
            return false;
    }

    public void SortSightRayList(List<EnemyStatus> _sightRay)
    {
        // 리스트 정렬
        _sightRay.Sort(delegate (EnemyStatus a, EnemyStatus b)
        {
            if (GetDistance(this.transform.position, a.transform.position) < GetDistance(this.transform.position, b.transform.position)) return -1;
            else if (GetDistance(this.transform.position, a.transform.position) > GetDistance(this.transform.position, b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayList(List<Status> _sightRay)
    {
        // 리스트 정렬
        _sightRay.Sort(delegate (Status a, Status b)
        {
            if (GetDistance(this.transform.position, a.transform.position) < GetDistance(this.transform.position, b.transform.position)) return -1;
            else if (GetDistance(this.transform.position, a.transform.position) > GetDistance(this.transform.position, b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByHp(List<Status> _sightRay)
    {
        // 리스트 정렬
        _sightRay.Sort(delegate (Status a, Status b)
        {
            if (a.CurHp < b.CurHp) return -1;
            else if (a.CurHp > b.CurHp) return 1;
            else return 0;
        });
    }

    public void AnimationDirection(CharacterStatus _status)
    {
        if (_status.AIState != EAIState.Died)
        {
            if (pathFindController.targetPos.x > this.transform.position.x) this.transform.localScale = new Vector3(-1, 1, 1);
            else this.transform.localScale = new Vector3(1, 1, 1);
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
        float atkSpeed = _status.TotalAtkSpeed;
        if (_status.DelayTime < atkSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int AttackTypeDamage(CharacterStatus _status)
    {
        if (_status.AttackType < 1f)
            return _status.TotalPhysicalDamage;
        else
            return _status.TotalMagicalDamage;
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

    public virtual IEnumerator AttackByAttackType(CharacterStatus _status)
    {
        if (!IsDelay(_status))
        {
            _status.ActiveLayer(LayerName.AttackLayer);
            _status.Ani.SetBool("IsAtk", true);
            _status.IsAtk = true;
            _status.Rig.velocity = Vector2.zero;
            _status.DelayTime = 0f;
            _status.Ani.SetFloat("AtkType", _status.AttackType);

            if (_status.AttackType == 0f)
            {
                AttackDamage(_status);
            }
            else if (_status.AttackType == 0.5f)
            {
                yield return WaitUntilAnimatorPoint(_status.Ani, 2, "PlayerAttack", 0.65f);
                ShotArrow(_status);
            }

            yield return WaitUntilAnimatorPoint(_status.Ani, 2, "PlayerAttack", 0.99f);
            _status.Ani.SetBool("IsAtk", false);
            _status.IsAtk = false;
        }

    }
    public IEnumerator UseSkill(CharacterStatus _status)
    {
        skillController.IsSkillDelay = true;
        yield return new WaitForSeconds(_status.TotalCastingSpeed);
        if(skillController.Skills[0] != null)
        {
            if (skillController.Skills[0].skillType == 0)
            {
                if (_status.Target)
                {
                    skillController.UseSkill();
                }
                else   
                    Debug.Log("타겟이 없음");
            }
            else if (skillController.Skills[0].skillType == 1)
            {
                if (_status.AllyTarget != null)
                {
                    skillController.UseSkill();
                }
                else
                    Debug.Log("타겟이 없음");
            }
        }
        skillController.IsSkillDelay = false;
    }
    public virtual void AttackDamage(CharacterStatus _status)
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
        _status.DelayTime += Time.deltaTime;
        switch (_status.AIState)
        {
            case EAIState.Idle:
                AIIdle(_status);
                break;
            case EAIState.Chase:
                AIChase(_status);
                break;
            case EAIState.Attack:
                AIAttack(_status);
                break;
            case EAIState.UseSkill:
                AIUseSkill(_status);
                break;
        }
    }
    public virtual void AIPerception(CharacterStatus _status)
    {
        //
    }
    public virtual void AIIdle(CharacterStatus _status)
    {
        _status.ActiveLayer(LayerName.IdleLayer);
        _status.Rig.velocity = Vector2.zero;
    }

    public virtual void AIChase(CharacterStatus _status)
    {
        if (pathFindController.FinalNodeList.Count > 1)
        {
            Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
            _status.ActiveLayer(LayerName.WalkLayer);
            _status.transform.position = Vector2.MoveTowards(_status.transform.position, _moveDir, _status.TotalSpeed * Time.deltaTime);
        }
        else if (pathFindController.FinalNodeList.Count == 1)
        {
            pathFindController.FinalNodeList.RemoveAt(0);
        }


    }
    public virtual void AIAttack(CharacterStatus _status)
    {

        _status.ActiveLayer(LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", _status.AttackType);
        _status.Rig.velocity = Vector2.zero;
        StartCoroutine(AttackByAttackType(_status));

    }
    public virtual void AIUseSkill(CharacterStatus _status)
    {
        _status.ActiveLayer(LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", _status.AttackType);
        _status.Rig.velocity = Vector2.zero;
        StartCoroutine(UseSkill(_status));
    }
    public virtual IEnumerator AIDied(CharacterStatus _status)
    {
        yield return null;
    }


    #endregion
}

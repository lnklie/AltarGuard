using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : BaseController, IAIController
{
    [SerializeField]
    protected SkillController skillController = null;
    [SerializeField]
    protected CharacterStatus characterStatus = null;
    [SerializeField]
    protected PathFindController pathFindController = null;
    
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
            yield return new WaitForSeconds(Random.Range(0.4f,0.8f));
        }
    }
    public WaitUntil WaitUntilAnimatorPoint(Animator _animator, int _index, string _aniName, float _point)
    {
        return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(_index).IsName(_aniName) &&
            _animator.GetCurrentAnimatorStateInfo(_index).normalizedTime >= _point);
    }
    public bool IsAtkRange(CharacterStatus _status)
    {
        if (GetDistance(_status.transform.position, _status.Target.transform.position) <= _status.AtkRange)
            return true;
        else
            return false;
    }

    public bool CheckRayList(Status _RayHit, List<Status> _RayList)
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
    public bool CheckRayList(EnemyStatus _RayHit, List<EnemyStatus> _RayList)
    {
        bool _bool = false;
        
        for (int i = 0; i < _RayList.Count; i++)
        {
            Debug.Log("닿은 적의 인덱스는 " + _RayHit.EnemyIndex + " 리스트의 인덱스는 " + _RayList[i].EnemyIndex);
            if (_RayHit.EnemyIndex == _RayList[i].EnemyIndex)
                _bool = true;
            else
                _bool = false;
        }
        return _bool;
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
        float atkSpeed = _status.AtkSpeed;
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
        if (skillController.ActiveSkills[0].skillType == 0)
        {
            if (_status.Target)
            {
                skillController.UseSkill(_status.Target);
            }
            else
                Debug.Log("타겟이 없음");
        }
        else if (skillController.ActiveSkills[0].skillType == 1)
        {
            if (_status.AllyTarget)
            {
                skillController.UseSkill(_status.AllyTarget);
            }
            else
                Debug.Log("타겟이 없음");
        }
        yield return new WaitForSeconds(1f);
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
        _status.ActiveLayer(LayerName.IdleLayer);
        _status.Rig.velocity = Vector2.zero;
    }
    //public Vector2 TargetPosByAtkRange(Node _targetNode, CharacterStatus _status)
    //{
    //    Vector2 _targetPos = Vector2.zero;
    //    if(Mathf.Pow(_targetNode.x,2) + Mathf.Pow(_targetNode.y, 2) == Mathf.Pow(_status.AtkRange,2))
    //        _targetPos = new Vector2(_targetNode.x, _targetNode.y);
    //    return _targetPos; 
    //}
    public virtual void AIChase(CharacterStatus _status)
    {
        if (pathFindController.FinalNodeList.Count > 1)
        {
            Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
            _status.ActiveLayer(LayerName.WalkLayer);
            _status.transform.position = Vector2.MoveTowards(_status.transform.position, _moveDir, _status.Speed * Time.deltaTime);
        }
        else if (pathFindController.FinalNodeList.Count == 1)
        {
            pathFindController.FinalNodeList.RemoveAt(0);
        }


    }
    public virtual void AIAttack(CharacterStatus _status)
    {
        if(skillController.SkillQueue.Count > 0 && !skillController.IsSkillDelay)
        {
            StartCoroutine(UseSkill(_status));
        }
        else
        { 
            _status.ActiveLayer(LayerName.AttackLayer);
            _status.Ani.SetFloat("AtkType", _status.AttackType);
            _status.Rig.velocity = Vector2.zero;
            StartCoroutine(AttackByAttackType(_status));
        }
    }

    public virtual IEnumerator AIDied(CharacterStatus _status)
    {
        _status.ActiveLayer(LayerName.IdleLayer);
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        yield return null;
    }
    #endregion
}

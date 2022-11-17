using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IAIController
{
    [SerializeField] protected SkillController skillController = null;
    [SerializeField] protected CharacterStatus character = null;
    [SerializeField] protected PathFindController pathFindController = null;
    [SerializeField] protected Vector2 destination = Vector2.zero;

    public PathFindController PathFindController { get { return pathFindController; } }

    protected StateMachine stateMachine;

    protected Dictionary<EAIState, IState> stateDic = new Dictionary<EAIState, IState>();
    public virtual void Awake()
    {
        character = this.GetComponent<CharacterStatus>();
    }
    public virtual void Start()
    {
        StartCoroutine(FindPath());
        StartCoroutine(AIPerception());

        IState idle = new IdleState(character);
        IState chase = new ChaseState(character, this);
        IState attack = new AttackState(character, this);
        IState skill = new SkillState(character, this);
        IState die = new DieState(character, this);

        stateDic.Add(EAIState.Idle, idle);
        stateDic.Add(EAIState.Chase, chase);
        stateDic.Add(EAIState.Attack, attack);
        stateDic.Add(EAIState.UseSkill, skill);
        stateDic.Add(EAIState.Died, die);

        stateMachine = new StateMachine(idle);
    }
    public virtual void Update()
    {
        AnimationDirection();

        if (IsDelay())
        {
            character.DelayTime += Time.deltaTime;
        }
        
        
    }
    public void SetState()
    {
        stateMachine.SetState(stateDic[EAIState.Idle]);
    }
    public virtual IEnumerator FindPath()
    {
        while(true)
        {
            pathFindController.SetStartPos(this.transform.position);
            pathFindController.SetTargetPos(destination);
            pathFindController.PathFinding();

            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
        }
    }
    public WaitUntil WaitUntilAnimatorPoint(Animator _animator, int _index, string _aniName, float _point)
    {
        return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(_index).IsName(_aniName) &&
            _animator.GetCurrentAnimatorStateInfo(_index).normalizedTime >= _point);
    }
    public bool IsAtkRange(Status _target)
    {
        if (character.GetDistance(_target.transform.position) <= character.TotalStatus[(int)EStatus.AtkRange])
            return true;
        else
            return false;
    }
    public bool IsSkillRange(Status _target)
    {
        if (character.GetDistance(_target.transform.position) <= skillController.SkillQueue[0].skillRange)
            return true;
        else
            return false;   
    }
    public bool IsSkillQueue()
    {
        if (skillController.SkillQueue.Count > 0)
            return true;
        else
            return false;
    }
    public bool IsSkillDelay()
    {
        if (character.IsUseSkill)
            return true;
        else
            return false;
    }

    public void SortSightRayListByDistance(List<RaycastHit2D> _sightRay)
    {
        // 리스트 정렬
        _sightRay.Sort(delegate (RaycastHit2D a, RaycastHit2D b)
        {
            if (character.GetDistance( a.transform.position) < character.GetDistance( b.transform.position)) return -1;
            else if (character.GetDistance(a.transform.position) > character.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByDistance(List<EnemyStatus> _sightRay)
    {
        _sightRay.Sort(delegate (EnemyStatus a, EnemyStatus b)
        {
            if (character.GetDistance(a.transform.position) < character.GetDistance(b.transform.position)) return -1;
            else if (character.GetDistance(a.transform.position) > character.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByCurHp(List<RaycastHit2D> _sightRay)
    {
        _sightRay.Sort(delegate (RaycastHit2D a, RaycastHit2D b)
        {
            if (a.collider.GetComponent<Status>().CurHp < b.collider.GetComponent<Status>().CurHp) return -1;
            else if (a.collider.GetComponent<Status>().CurHp > b.collider.GetComponent<Status>().CurHp) return 1;
            else return 0;
        });
    }
    public RaycastHit2D ChooseSightRayListByRandom(List<RaycastHit2D> _sightRay)
    {
        int _randomIndex = Random.Range(0, _sightRay.Count);
        return _sightRay[_randomIndex];
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

    public virtual void AnimationDirection()
    {
        if (character.AIState != EAIState.Died)
        {
            if (pathFindController.targetPos.x > this.transform.position.x) 
                this.transform.localScale = new Vector3(-1, 1, 1);
            else 
                this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            character.Rig.AddForce(-direction * knockbackPower);
        }

        yield return null;
    }
    public bool IsDelay()
    {
        float atkSpeed = character.TotalStatus[(int)EStatus.AttackSpeed];
        if (character.DelayTime < atkSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int AttackTypeDamage()
    {
        if (character.AttackType < 1f)
            return (int)character.TotalStatus[(int)EStatus.PhysicalDamage];
        else
            return (int)character.TotalStatus[(int)EStatus.MagicalDamage];
    }
    public void ShotArrow()
    {
        // 활쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(character, AttackTypeDamage());
        }
        else
            Debug.Log("화살 없음");

    }


    public virtual IEnumerator AutoAttackByAttackType()
    {
        while(true)
        {
            if(!IsDelay())
            {
                character.Ani.SetBool("IsAtk", true);
                character.IsAtk = true;
                Debug.Log("공격 " + character.ObjectName);
                if (character.AttackType == 0f)
                {
                    AttackDamage();
                }
                else if (character.AttackType == 0.5f)
                {
                    yield return WaitUntilAnimatorPoint(character.Ani, 2, "PlayerAttack", 0.65f);
                    ShotArrow();
                }

                yield return WaitUntilAnimatorPoint(character.Ani, 2, "PlayerAttack", 0.99f);
                character.Ani.SetBool("IsAtk", false);
                yield return new WaitForSeconds(1f);
                character.IsAtk = false;
                character.DelayTime = 0f;
            }
            yield return null;
        }
    }
    public virtual void Targeting(bool _isAlly = false)
    {
        // 적 발견
    }
    public virtual void CheckTarget()
    {
        if (character.Target)
        {
            if (character.GetDistance(character.Target.transform.position) >= character.SeeRange || character.Target.IsDied)
            {
               
                character.Target = null;
            }
        }
    }
    public void ReTargeting()
    {
        if (character.Target == null || !skillController.IsMatchSkillType(skillController.SkillQueue[0], character.Target))
        {
            Debug.Log("리타겟팅");
            switch (skillController.SkillQueue[0].skillType)
            {
                case (int)ESkillType.Attack:
                case (int)ESkillType.Curse:
                    Targeting();
                    break;
                case (int)ESkillType.Cure:
                case (int)ESkillType.Buff:
                    Targeting(true);
                    break;
            }
        }
    }
    public IEnumerator UseSkill()
    {
        while(true)
        {
            yield return new WaitForSeconds(character.TotalStatus[(int)EStatus.CastingSpeed]);
            if (skillController.SkillQueue.Count > 0)
            {
                character.IsUseSkill = true;
                ReTargeting();
                StartCoroutine(skillController.UseAutoSkill(true));
            }
            yield return new WaitUntil(() => !character.IsUseSkill); 
        }

    }

    public virtual void AttackDamage()
    {
        //
    }
    #region AI
    public virtual void AIChangeState()
    {
        //
    }
    public virtual void AIState()
    {
        
    }
    public virtual IEnumerator AIPerception()
    {
        yield return null;
    }
    public virtual void AIIdle()
    {
        character.ActiveLayer(ELayerName.IdleLayer);
        character.Rig.velocity = Vector2.zero;
    }
    public bool IsTargetEnemy(Status _target)
    {
        bool _bool = false;
        if (character.gameObject.layer != _target.gameObject.layer)
            _bool = true;
        return _bool;
    }
    public virtual void AIChase()
    {
        if (pathFindController.FinalNodeList.Count > 1)
        {
            Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
            character.transform.position = Vector2.MoveTowards(character.transform.position, _moveDir, character.TotalStatus[(int)EStatus.Speed] * Time.deltaTime);
        }
        else if (pathFindController.FinalNodeList.Count == 1)
        {
            pathFindController.FinalNodeList.RemoveAt(0);
        }
    }
    public void StopAllcoroutine()
    {
        StopAllCoroutines();
    }
    public virtual void AIAttack()
    {
        StartCoroutine(AutoAttackByAttackType());
    }
    public void StopAttack()
    {
        StopCoroutine(AutoAttackByAttackType());

        character.IsAtk = false;
    }
    public virtual void AIUseSkill()
    {
        StartCoroutine(UseSkill());
    }
    public void StartAIUseSkill()
    {
        StartCoroutine(UseSkill());
    }
    public void StartAIDied()
    {
        StartCoroutine(AIDied());
    }
    public virtual IEnumerator AIDied()
    {
        yield return null;
    }


    #endregion
}

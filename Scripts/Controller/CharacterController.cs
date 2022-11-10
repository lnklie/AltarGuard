using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IAIController
{
    [SerializeField] protected SkillController skillController = null;
    [SerializeField] protected CharacterStatus character = null;
    [SerializeField] protected PathFindController pathFindController = null;
    [SerializeField] protected Vector2 destination = Vector2.zero;
    [SerializeField] private bool[] isAllyTargeted = new bool[5];
    [SerializeField] private bool[] isEnemyTargeted = new bool[101];
    [SerializeField] private TargetingBoxController targetingBoxController = null;
    public bool[] IsAllyTargeted { get { return isAllyTargeted; } set { isAllyTargeted = value; } }
    public bool[] IsEnemyTargeted { get { return isEnemyTargeted; } set { isEnemyTargeted = value; } }
    public virtual void Awake()
    {
        character = this.GetComponent<CharacterStatus>();
    }
    public virtual void Start()
    {
        StartCoroutine(FindPath());
        StartCoroutine(AIPerception());
    }
    public virtual void Update()
    {
        AIChangeState();
        AIState();
        if (!IsDelay())
        {
            character.DelayTime = character.TotalStatus[(int)EStatus.AttackSpeed];
        }
        else
        {
            character.DelayTime += Time.deltaTime;
        }

        
    }
    public IEnumerator FindPath()
    {
        while(true)
        {
            pathFindController.SetTargetPos(destination);
            pathFindController.PathFinding();
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
    public WaitUntil WaitUntilAnimatorPoint(Animator _animator, int _index, string _aniName, float _point)
    {
        return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(_index).IsName(_aniName) &&
            _animator.GetCurrentAnimatorStateInfo(_index).normalizedTime >= _point);
    }
    public bool IsAtkRange(CharacterController _target)
    {
        if (character.GetDistance(_target.transform.position) <= character.TotalStatus[(int)EStatus.AtkRange])
            return true;
        else
            return false;
    }
    public bool IsSkillRange(CharacterController _target)
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

    public void SortSightRayListByDistance(List<EnemyController> _sightRay)
    {
        // 리스트 정렬
        _sightRay.Sort(delegate (EnemyController a, EnemyController b)
        {
            if (character.GetDistance( a.transform.position) < character.GetDistance( b.transform.position)) return -1;
            else if (character.GetDistance(a.transform.position) > character.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByDistance(List<AllyController> _sightRay)
    {
        _sightRay.Sort(delegate (AllyController a, AllyController b)
        {
            if (character.GetDistance(a.transform.position) < character.GetDistance(b.transform.position)) return -1;
            else if (character.GetDistance(a.transform.position) > character.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByCurHp(List<AllyController> _sightRay)
    {
        _sightRay.Sort(delegate (AllyController a, AllyController b)
        {
            if (a.character.CurHp < b.character.CurHp) return -1;
            else if (a.character.CurHp > b.character.CurHp) return 1;
            else return 0;
        });
    }
    public AllyController ChooseSightRayListByRandom(List<AllyController> _sightRay)
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
            if (pathFindController.targetPos.x > this.transform.position.x) this.transform.localScale = new Vector3(-1, 1, 1);
            else this.transform.localScale = new Vector3(1, 1, 1);
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

        yield return 0;
    }
    public bool IsDelay()
    {
        float atkSpeed = character.TotalStatus[(int)EStatus.AttackSpeed];
        if (character.DelayTime <= atkSpeed)
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
    public bool IsDied()
    {
        if (character.CurHp <= 0)
            return true;
        else
            return false;
    }

    public virtual IEnumerator AttackByAttackType()
    {
        if (!IsDelay())
        {
            character.ActiveLayer(ELayerName.AttackLayer);
            character.Ani.SetBool("IsAtk", true);
            character.IsAtk = true;
            character.DelayTime = 0f;
            character.Ani.SetFloat("AtkType", character.AttackType);

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
            character.IsAtk = false;
        }

    }
    public virtual void Targeting(List<EnemyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if (_hit.Length > 0)
        {
            // 적 발견
        }
    }
    public virtual void Targeting(List<AllyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_hit.Length > 0)
        {
            // 아군 발견
        }
    }
    public virtual void ResortTarget(List<EnemyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            // 적이 있을때
            SortSightRayListByDistance(_targetList);
            character.Target = _targetList[0];
        }
        else
        {
            // 적이 없을 떄
        }
    }
    public virtual void ResortTarget(List<AllyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            // 아군이 있을 떄

        }
        else
        {
            // 아군이 없을때
        }
    }

    public IEnumerator UseSkill()
    {
        if(!skillController.IsSkillDelay)
        {
            if (character.Target == null)
                switch (skillController.Skills[0].skillType)
                {
                    case (int)ESkillType.Attack:
                    case (int)ESkillType.Curse:
                        ResortTarget(character.EnemyRayList);
                        break;
                    case (int)ESkillType.Cure:
                    case (int)ESkillType.Buff:
                        ResortTarget(character.AllyRayList);
                        break;
                }

            skillController.IsSkillDelay = true;
            yield return new WaitForSeconds(character.TotalStatus[(int)EStatus.CastingSpeed]);
            if(skillController.Skills[0] != null)
            {
                StartCoroutine(skillController.UseSkill(true));
            }

            skillController.IsSkillDelay = false;
            character.IsUseSkill = false;
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
        AnimationDirection();
        switch (character.AIState)
        {
            case EAIState.Idle:
                AIIdle();
                break;
            case EAIState.Chase:
                AIChase();
                break;
            case EAIState.Attack:
                AIAttack();
                break;
            case EAIState.UseSkill:
                if(!character.IsUseSkill)
                {
                    AIUseSkill();
                }
                break;
        }
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

    public virtual void AIChase()
    {
        if (pathFindController.FinalNodeList.Count > 1)
        {
            Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
            character.ActiveLayer(ELayerName.WalkLayer);
            character.transform.position = Vector2.MoveTowards(character.transform.position, _moveDir, character.TotalStatus[(int)EStatus.Speed] * Time.deltaTime);
        }
        else if (pathFindController.FinalNodeList.Count == 1)
        {
            pathFindController.FinalNodeList.RemoveAt(0);
        }
    }
    public virtual void AIAttack()
    {

        character.ActiveLayer(ELayerName.AttackLayer);
        character.Ani.SetFloat("AtkType", character.AttackType);
        character.Rig.velocity = Vector2.zero;
        StartCoroutine(AttackByAttackType());
    }
    public virtual void AIUseSkill()
    {
        character.IsUseSkill = true;
        character.ActiveLayer(ELayerName.AttackLayer);
        character.Ani.SetFloat("AtkType", character.AttackType);
        character.Rig.velocity = Vector2.zero;
        StartCoroutine(UseSkill());
    }
    public virtual IEnumerator AIDied()
    {
        character.IsDied = false;
        yield return null;
    }
    public void SetTargetingBox(bool _bool)
    {
        targetingBoxController.IsTargeting = _bool;
    }

    #endregion
}

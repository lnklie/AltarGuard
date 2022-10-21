using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IAIController
{
    [SerializeField] protected SkillController skillController = null;
    [SerializeField] protected CharacterStatus characterStatus = null;
    [SerializeField] protected PathFindController pathFindController = null;
    [SerializeField] protected Vector2 destination = Vector2.zero;
    [SerializeField] private bool[] isAllyTargeted = new bool[5];
    [SerializeField] private bool[] isEnemyTargeted = new bool[101];
    public bool[] IsAllyTargeted { get { return isAllyTargeted; } set { isAllyTargeted = value; } }
    public bool[] IsEnemyTargeted { get { return isEnemyTargeted; } set { isEnemyTargeted = value; } }
    public virtual void Awake()
    {
        characterStatus = this.GetComponent<CharacterStatus>();
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
            characterStatus.DelayTime = characterStatus.TotalAtkSpeed;
        }
        else
        {
            characterStatus.DelayTime += Time.deltaTime;
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
        if (characterStatus.GetDistance(_target.transform.position) <= characterStatus.TotalAtkRange)
            return true;
        else
            return false;
    }
    public bool IsSkillRange(CharacterController _target)
    {
        if (characterStatus.GetDistance(_target.transform.position) <= skillController.SkillQueue[0].skillRange)
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
            if (characterStatus.GetDistance( a.transform.position) < characterStatus.GetDistance( b.transform.position)) return -1;
            else if (characterStatus.GetDistance(a.transform.position) > characterStatus.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByDistance(List<AllyController> _sightRay)
    {
        _sightRay.Sort(delegate (AllyController a, AllyController b)
        {
            if (characterStatus.GetDistance(a.transform.position) < characterStatus.GetDistance(b.transform.position)) return -1;
            else if (characterStatus.GetDistance(a.transform.position) > characterStatus.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByCurHp(List<AllyController> _sightRay)
    {
        _sightRay.Sort(delegate (AllyController a, AllyController b)
        {
            if (a.characterStatus.CurHp < b.characterStatus.CurHp) return -1;
            else if (a.characterStatus.CurHp > b.characterStatus.CurHp) return 1;
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
        if (characterStatus.AIState != EAIState.Died)
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
            characterStatus.Rig.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
    public bool IsDelay()
    {
        float atkSpeed = characterStatus.TotalAtkSpeed;
        if (characterStatus.DelayTime <= atkSpeed)
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
        if (characterStatus.AttackType < 1f)
            return characterStatus.TotalPhysicalDamage;
        else
            return characterStatus.TotalMagicalDamage;
    }
    public void ShotArrow()
    {
        // 활쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(characterStatus, AttackTypeDamage());
        }
        else
            Debug.Log("화살 없음");
    }
    public bool IsDied()
    {
        if (characterStatus.CurHp <= 0)
            return true;
        else
            return false;
    }

    public virtual IEnumerator AttackByAttackType()
    {
        if (!IsDelay())
        {
            characterStatus.ActiveLayer(ELayerName.AttackLayer);
            characterStatus.Ani.SetBool("IsAtk", true);
            characterStatus.IsAtk = true;
            characterStatus.DelayTime = 0f;
            characterStatus.Ani.SetFloat("AtkType", characterStatus.AttackType);

            if (characterStatus.AttackType == 0f)
            {
                AttackDamage();
            }
            else if (characterStatus.AttackType == 0.5f)
            {
                yield return WaitUntilAnimatorPoint(characterStatus.Ani, 2, "PlayerAttack", 0.65f);
                ShotArrow();
            }

            yield return WaitUntilAnimatorPoint(characterStatus.Ani, 2, "PlayerAttack", 0.99f);
            characterStatus.Ani.SetBool("IsAtk", false);
            characterStatus.IsAtk = false;
        }

    }
    public IEnumerator UseSkill()
    {
        skillController.IsSkillDelay = true;
        yield return new WaitForSeconds(characterStatus.TotalCastingSpeed);
        if(skillController.Skills[0] != null)
        {
            skillController.UseSkill();
        }

        skillController.IsSkillDelay = false;
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
        switch (characterStatus.AIState)
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
                AIUseSkill();
                break;
        }
    }
    public virtual IEnumerator AIPerception()
    {
        yield return null;
    }
    public virtual void AIIdle()
    {
        characterStatus.ActiveLayer(ELayerName.IdleLayer);
        characterStatus.Rig.velocity = Vector2.zero;
    }

    public virtual void AIChase()
    {
        if (pathFindController.FinalNodeList.Count > 1)
        {
            Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
            characterStatus.ActiveLayer(ELayerName.WalkLayer);
            characterStatus.transform.position = Vector2.MoveTowards(characterStatus.transform.position, _moveDir, characterStatus.TotalSpeed * Time.deltaTime);
        }
        else if (pathFindController.FinalNodeList.Count == 1)
        {
            pathFindController.FinalNodeList.RemoveAt(0);
        }
    }
    public virtual void AIAttack()
    {

        characterStatus.ActiveLayer(ELayerName.AttackLayer);
        characterStatus.Ani.SetFloat("AtkType", characterStatus.AttackType);
        characterStatus.Rig.velocity = Vector2.zero;
        StartCoroutine(AttackByAttackType());
    }
    public virtual void AIUseSkill()
    {
        characterStatus.ActiveLayer(ELayerName.AttackLayer);
        characterStatus.Ani.SetFloat("AtkType", characterStatus.AttackType);
        characterStatus.Rig.velocity = Vector2.zero;
        StartCoroutine(UseSkill());
    }
    public virtual IEnumerator AIDied()
    {
        yield return null;
    }


    #endregion
}

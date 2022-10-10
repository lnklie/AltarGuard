using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IAIController
{
    [SerializeField] protected SkillController skillController = null;
    [SerializeField] protected CharacterStatus characterStatus = null;
    [SerializeField] protected PathFindController pathFindController = null;
    
    public virtual void Awake()
    {
        characterStatus = this.GetComponent<CharacterStatus>();

    }
    public virtual void Start()
    {
        StartCoroutine(FindPath());
    }
    public virtual void Update()
    {
        if(!characterStatus.IsDied)
        { 
            AIPerception();
        }
        AIChangeState();
        AIState();
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
    public bool IsAtkRange()
    {
        if (characterStatus.GetDistance(characterStatus.Target.transform.position) <= characterStatus.TotalAtkRange)
            return true;
        else
            return false;
    }
    public bool IsSkillRange()
    {
        if (characterStatus.GetDistance(characterStatus.Target.transform.position) <= skillController.SkillQueue[0].skillRange)
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

    public void SortSightRayList(List<EnemyStatus> _sightRay)
    {
        _sightRay.Sort(delegate (EnemyStatus a, EnemyStatus b)
        {
            if (characterStatus.GetDistance( a.transform.position) < characterStatus.GetDistance( b.transform.position)) return -1;
            else if (characterStatus.GetDistance(a.transform.position) > characterStatus.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayList(List<Status> _sightRay)
    {
        _sightRay.Sort(delegate (Status a, Status b)
        {
            if (characterStatus.GetDistance(a.transform.position) < characterStatus.GetDistance(b.transform.position)) return -1;
            else if (characterStatus.GetDistance(a.transform.position) > characterStatus.GetDistance(b.transform.position)) return 1;
            else return 0;
        });
    }
    public void SortSightRayListByHp(List<Status> _sightRay)
    {
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
        if (characterStatus.DelayTime < atkSpeed)
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
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(characterStatus, AttackTypeDamage());
        }
        else
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
            characterStatus.ActiveLayer(LayerName.AttackLayer);
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
            if (skillController.Skills[0].skillType == 0)
            {
                if (characterStatus.Target)
                {
                    skillController.UseSkill();
                }
                else   

            }
            else if (skillController.Skills[0].skillType == 1)
            {
                if (characterStatus.AllyTarget != null)
                {
                    skillController.UseSkill();
                }
                else
            }
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
    public virtual void AIPerception()
    {
        //
    }
    public virtual void AIIdle()
    {
        characterStatus.ActiveLayer(LayerName.IdleLayer);
        characterStatus.Rig.velocity = Vector2.zero;
    }

    public virtual void AIChase()
    {
        if (pathFindController.FinalNodeList.Count > 1)
        {
            Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
            characterStatus.ActiveLayer(LayerName.WalkLayer);
            characterStatus.transform.position = Vector2.MoveTowards(characterStatus.transform.position, _moveDir, characterStatus.TotalSpeed * Time.deltaTime);
        }
        else if (pathFindController.FinalNodeList.Count == 1)
        {
            pathFindController.FinalNodeList.RemoveAt(0);
        }
    }
    public virtual void AIAttack()
    {

        characterStatus.ActiveLayer(LayerName.AttackLayer);
        characterStatus.Ani.SetFloat("AtkType", characterStatus.AttackType);
        characterStatus.Rig.velocity = Vector2.zero;
        StartCoroutine(AttackByAttackType());
    }
    public virtual void AIUseSkill()
    {
        characterStatus.ActiveLayer(LayerName.AttackLayer);
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

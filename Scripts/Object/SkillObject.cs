using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private CircleCollider2D col = null;
    [SerializeField] private int value = 0;
    [SerializeField] private float maxCoolTime = 0f;
    [SerializeField] private bool isSkillUse = false;
    [SerializeField] private CharacterStatus castingStatus = null;
    [SerializeField] private Skill skill = null;
    private int skillHitCount = 0;
    private float maxDurationTime = 0f;
    private float durationTime = 0f;

    private Transform target = null;

    #region Property
    public int Damage { get { return value; } set { this.value = value; } }
    public int SkillHitCount { get { return skillHitCount; } set { skillHitCount = value; } }
    public float MaxDuration { get { return maxDurationTime; } set { maxDurationTime = value; } }
    public Transform Target { get { return target; } set { target = value; } }
    public float MaxCoolTime { get { return maxCoolTime; } set { maxCoolTime = value; } }
    public bool IsSkillUse { get { return isSkillUse; } set { isSkillUse = value; } }
    #endregion
    public void Awake()
    {
        col = this.GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        maxDurationTime = this.GetComponentInChildren<Animator>().speed;

    }
    private void Update()
    { 
        if(isSkillUse)
            StartCoroutine(CastingSkill());
        RemoveSkill();
    }
    public void SetSkill(Transform _target, Skill _skill, CharacterStatus _characterStatus)
    {
        skill = _skill;
        castingStatus = _characterStatus;
        target = _target; 
        value = SetSkillValueByLevel();
        skillHitCount = skill.skillHitCount;
        this.gameObject.SetActive(true);
        this.transform.position = target.transform.position;
        isSkillUse = true;
    }
    public IEnumerator CastingSkill()
    {
        isSkillUse = false;
        if(skill.skillType == 0)
        {
            RaycastHit2D[] _hitRay = HitRay();
 
            for(int i = 0; i < _hitRay.Length; i++)
            {
                if (_hitRay[i])
                {
                    Status _status = _hitRay[i].collider.gameObject.GetComponent<Status>();
                
                    for (int j = 0; j < skillHitCount; j++)
                    {
                        _status.Damaged(value);
                        if (this.transform.parent.gameObject.layer == 8)
                        {
                            castingStatus.AquireExp(_status);
                        }
                        yield return new WaitForSeconds(maxDurationTime / skillHitCount);
                    }
                    castingStatus.IsAtk = false;
                }
            }
        }
        else if(skill.skillType == 1)
        {
            Status _status = target.parent.gameObject.GetComponent<Status>();
            for (int j = 0; j < skillHitCount; j++)
            {
                _status.recovered(value);
                yield return new WaitForSeconds(maxDurationTime / skillHitCount);
            }
            castingStatus.IsAtk = false;
        }
    }

    public void RemoveSkill()
    {
        durationTime += Time.deltaTime;
        
        if (durationTime > maxDurationTime)
        {
            this.gameObject.SetActive(false);
            durationTime = 0f;
        }

    }
    public int ReviseDamage(int _damage, int _depensivePower)
    {
        return Mathf.CeilToInt(_damage * (1 / (1 + _depensivePower)));
    }
    private RaycastHit2D[] HitRay()
    {
        // 레이를 쏘는 역할
        
        RaycastHit2D[] ray = default;
        if (this.transform.parent.gameObject.layer == 3)
            ray = Physics2D.CircleCastAll(this.transform.position, col.radius,Vector2.zero, 0f ,LayerMask.GetMask("Ally", "Altar"));
        else if (this.transform.parent.gameObject.layer == 8)
            ray = Physics2D.CircleCastAll(this.transform.position, col.radius, Vector2.zero, 0f, LayerMask.GetMask("Enemy"));
        return ray;
    }
    public int SetSkillValueByLevel()
    {
        int _skillDamage = 0;
        switch (skill.skillLevel)
        {
            case 1:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue1 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures1);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue1 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures1);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue1 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures1);
                break;
            case 2:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue2 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures2);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue2 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures2);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue2 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures2);
                break;
            case 3:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue3 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures3);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue3 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures3);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue3 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures3);
                break;
            case 4:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue4 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures4);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue4 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures4);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue4 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures4);
                break;
            case 5:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue5 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures5);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue5 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures5);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue5 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures5);
                break;
            case 6:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue6 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures6);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue6 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures6);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue6 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures6);
                break;
            case 7:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue7 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures7);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue7 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures7);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue7 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures7);
                break;
            case 8:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue8 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures8);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue8 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures8);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue8 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures8);
                break;
            case 9:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue9 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures9);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue9 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures9);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue9 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures9);
                break;
            case 10:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue10 + Mathf.CeilToInt(castingStatus.TotalStr * skill.skillFigures10);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue10 + Mathf.CeilToInt(castingStatus.TotalDex * skill.skillFigures10);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue10 + Mathf.CeilToInt(castingStatus.TotalWiz * skill.skillFigures10);
                break;
        }
        return _skillDamage;
    }
}

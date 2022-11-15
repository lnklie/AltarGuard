using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private CircleCollider2D col = null;
    [SerializeField] private int value = 0;
    [SerializeField] private float maxCoolTime = 0f;
    [SerializeField] private bool isSkillUse = false;
    [SerializeField] private bool isSkillActive = false;
    [SerializeField] private CharacterStatus castingStatus = null;
    [SerializeField] private Skill skill = null;
    private int skillHitCount = 0;
    private float maxDurationTime = 0f;
    private float durationTime = 0f;

    private Transform target = null;

    #region Property
    public bool IsSkillActive { get { return isSkillActive; } set { isSkillActive = value; } }
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
        RemoveSkill();
    }
    public void SetSkill(Skill _skill)
    {
        skill = _skill;
    }
    public void SetSkillTarget(Transform _target, CharacterStatus _characterStatus)
    {
        castingStatus = _characterStatus;
        target = _target; 
        value = SetSkillValueByLevel();
        skillHitCount = skill.skillHitCount;
        this.gameObject.SetActive(true);
        this.transform.position = target.transform.position;
    }
    public IEnumerator CastingSkill()
    {
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
                            if(_status.IsDied)
                                castingStatus.AquireExp(_status);
                        }
                        yield return new WaitForSeconds(maxDurationTime / skillHitCount);
                    }
                }
            }
        }
        else if(skill.skillType == 1)
        {
            Status _status = target.gameObject.GetComponent<Status>();
            for (int j = 0; j < skillHitCount; j++) 
            {
                _status.recovered(value);
                yield return new WaitForSeconds(maxDurationTime / skillHitCount);
            }
            castingStatus.Target = null;
        }
        castingStatus.IsUseSkill = false;
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
            ray = Physics2D.BoxCastAll(this.transform.position, new Vector2(skill.skillScopeX,skill.skillScopeY), 0f ,Vector2.zero, 0f,LayerMask.GetMask("Ally", "Altar"));
        else if (this.transform.parent.gameObject.layer == 8)
            ray = Physics2D.BoxCastAll(this.transform.position, new Vector2(skill.skillScopeX, skill.skillScopeY), 0f, Vector2.zero,0f, LayerMask.GetMask("Enemy"));
        return ray;
    }
    public int SetSkillValueByLevel()
    {
        int _skillDamage = 0;
        switch (skill.skillLevel)
        {
            case 1:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue1 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures1);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue1 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures1);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue1 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures1);
                break;
            case 2:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue2 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures2);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue2 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures2);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue2 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures2);
                break;
            case 3:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue3 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures3);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue3 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures3);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue3 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures3);
                break;
            case 4:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue4 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures4);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue4 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures4);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue4 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures4);
                break;
            case 5:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue5 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures5);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue5 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures5);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue5 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures5);
                break;
            case 6:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue6 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures6);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue6 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures6);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue6 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures6);
                break;
            case 7:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue7 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures7);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue7 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures7);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue7 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures7);
                break;
            case 8:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue8 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures8);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue8 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures8);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue8 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures8);
                break;
            case 9:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue9 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures9);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue9 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures9);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue9 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures9);
                break;
            case 10:
                if (skill.skillVariable == 0)
                    _skillDamage = skill.skillValue10 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Str] * skill.skillFigures10);
                else if (skill.skillVariable == 1)
                    _skillDamage = skill.skillValue10 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Dex] * skill.skillFigures10);
                else if (skill.skillVariable == 2)
                    _skillDamage = skill.skillValue10 + Mathf.CeilToInt(castingStatus.TotalStatus[(int)EStatus.Wiz] * skill.skillFigures10);
                break;
        }
        return _skillDamage;
    }
    
}

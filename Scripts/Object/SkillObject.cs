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
    [SerializeField] private Vector2 targetPos = Vector2.zero;
    private float durationTime = 0f;


    #region Property
    public bool IsSkillActive { get { return isSkillActive; } set { isSkillActive = value; } }
    #endregion
    public void Awake()
    {
        col = this.GetComponent<CircleCollider2D>();
    }

    private void Update()
    { 
        RemoveSkill();
    }
    public void SetSkill(Skill _skill)
    {
        skill = _skill;
    }
    public void SetSkillTarget(CharacterStatus _characterStatus, Vector2 _target,int _value)
    {
        castingStatus = _characterStatus;
        targetPos = _target; 
        value = _value;
        this.gameObject.SetActive(true);
        this.transform.position = targetPos;
    }
    public void SetSkillTarget(CharacterStatus _characterStatus, Vector2 _target)
    {
        castingStatus = _characterStatus;
        targetPos = _target;
        this.gameObject.SetActive(true);
        this.transform.position = targetPos;
    }
    public IEnumerator CastingSkill(int _skillType)
    {
        if(_skillType == 0)
        {
            for (int j = 0; j < skill.skillHitCount; j++)
            {
                RaycastHit2D[] _hitRay = HitRay();

                for (int i = 0; i < _hitRay.Length; i++)
                {
                    if (_hitRay[i])
                    {
                        Status _status = _hitRay[i].collider.gameObject.GetComponent<Status>();


                        _status.Damaged(value, Color.blue);
                        if (castingStatus.gameObject.layer == 8)
                        {
                            if (_status.IsLastHit())
                                castingStatus.AquireExp(_status);
                        }
                        

                    }
                }
                yield return new WaitForSeconds(1f / skill.skillHitCount);
            }
        }
        else if(_skillType == 1)
        {
            castingStatus.Target = null;
        }
        castingStatus.IsUseSkill = false;
    }

    public void RemoveSkill()
    {
        durationTime += Time.deltaTime;
        
        if (durationTime > 1f)
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
        if (castingStatus.gameObject.layer == 3)
            ray = Physics2D.BoxCastAll(this.transform.position, new Vector2(skill.skillScopeX,skill.skillScopeY), 0f ,Vector2.zero, 0f,LayerMask.GetMask("Ally", "Altar"));
        else if (castingStatus.gameObject.layer == 8)
            ray = Physics2D.BoxCastAll(this.transform.position, new Vector2(skill.skillScopeX, skill.skillScopeY), 0f, Vector2.zero,0f, LayerMask.GetMask("Enemy"));
        return ray;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private CircleCollider2D col = null;
    [SerializeField]
    private int damage = 0;
    private int skillHitCount = 0;
    private float maxDurationTime = 0f;
    private float durationTime = 0f;

    private Transform target = null;
    [SerializeField]
    private float maxCoolTime = 0f;
    private bool isSkillUse = false;
    private CharacterController castingController = null;
    private CharacterStatus castingStatus = null;


    #region Property
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int SkillHitCount
    {
        get { return skillHitCount; }
        set { skillHitCount = value; }
    }
    public float MaxDuration
    {
        get { return maxDurationTime; }
        set { maxDurationTime = value; }
    }
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    public float MaxCoolTime
    {
        get { return maxCoolTime; }
        set { maxCoolTime = value; }
    }
    public bool IsSkillUse
    {
        get { return isSkillUse; }
        set { isSkillUse = value; }
    }
    #endregion
    public void Awake()
    {
        col = this.GetComponent<CircleCollider2D>();
        castingController = this.transform.parent.parent.GetComponentInChildren<CharacterController>();
        castingStatus = castingController.GetComponent<CharacterStatus>();
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
    public IEnumerator CastingSkill()
    {
        isSkillUse = false;
        
        RaycastHit2D[] _hitRay = HitRay();
        for(int i = 0; i < _hitRay.Length; i++)
        {
            if (_hitRay[i])
            {
                Status _status = _hitRay[i].collider.gameObject.GetComponent<Status>();
                
                for (int j = 0; j < skillHitCount; j++)
                {
                    _status.Damaged(damage / skillHitCount);
                    if (this.transform.parent.gameObject.layer == 8)
                    {
                        castingStatus.AquireExp(_status);
                        
                    }
                    yield return new WaitForSeconds(durationTime / skillHitCount);
                    castingStatus.IsAtk = false;
                }
            }
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
            ray = Physics2D.CircleCastAll(this.transform.position, col.radius,Vector2.zero, 0f ,LayerMask.GetMask("Ally"));
        else if (this.transform.parent.gameObject.layer == 8)
            ray = Physics2D.CircleCastAll(this.transform.position, col.radius, Vector2.zero, 0f, LayerMask.GetMask("Enemy"));
        return ray;
    }
}

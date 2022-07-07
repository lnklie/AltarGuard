using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private CircleCollider2D col = null;

    [SerializeField]
    private int damage = 0;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private int skillHitCount = 0;
    public int SkillHitCount
    {
        get { return skillHitCount; }
        set { skillHitCount = value; }
    }
    private float maxDurationTime = 0f;
    public float MaxDuration
    {
        get { return maxDurationTime; }
        set { maxDurationTime = value; }
    }
    private float durationTime = 0f;

    private GameObject target = null;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }
    [SerializeField]
    private float maxCoolTime = 0f;
    public float MaxCoolTime
    {
        get { return maxCoolTime; }
        set { maxCoolTime = value; }
    }
    private bool isSkillUse = false;
    public bool IsSkillUse
    {
        get { return isSkillUse; }
        set { isSkillUse = value; }
    }
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
                    _status.CurHp -= damage / skillHitCount;
                    _status.IsDamaged = true;
                    Debug.Log(_status.CurHp);
                    yield return new WaitForSeconds(durationTime / skillHitCount);
                }
                if (this.transform.parent.gameObject.layer == 8)
                {
                    if(this.gameObject.CompareTag("Mercenary"))
                    {
                        CharacterStatus mercenary = this.transform.parent.parent.GetComponentInChildren<CharacterStatus>();
                        mercenary.IsAtk = true;
                        if (mercenary.GetComponent<MercenaryAIController>().IsLastHit(_hitRay[i].collider.GetComponent<EnemyStatus>()))
                        {
                            mercenary.CurExp += _hitRay[i].collider.GetComponent<EnemyStatus>().DefeatExp;
                            // 용병 업데이트
                        }
                    }
                    else
                    {
                        CharacterStatus player = this.transform.parent.parent.GetComponentInChildren<CharacterStatus>();
                        player.IsAtk = true;
                        if(player.GetComponent<PlayerController>().IsLastHit(_hitRay[i].collider.GetComponent<EnemyStatus>(), player))
                        {
                            player.GetComponent<CharacterStatus>().CurExp += _hitRay[i].collider.GetComponent<EnemyStatus>().DefeatExp;
                        }
                    }
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

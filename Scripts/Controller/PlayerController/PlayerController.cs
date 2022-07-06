using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : PlayerController.cs
==============================
*/
public class PlayerController : BaseController , IAIController
{
    private CharacterStatus character = null;
    [SerializeField]
    private SkillController skillController = null;
    private SpriteRenderer bodySprites = null;
    private Animator ani = null;
    private Rigidbody2D rig = null;
    private CapsuleCollider2D col = null;
    [SerializeField]
    private TextMesh txtMesh = null;
    private RaycastHit2D[] itemSight = default;
    private Vector2 dir = new Vector2(0, 0);
    private Vector2 lookDir = Vector2.down;

    [SerializeField]
    private List<RaycastHit2D> sightRay = new List<RaycastHit2D>();
    private RaycastHit2D atkRangeRay = default;

    private float delayTime = 0f;
    private bool isAtk = false;
    public bool IsAtk
    {
        get { return isAtk; }
        set { isAtk = value; }
    }
    private float revivalTime = 5f;
    public float RevivalTime
    {
        get { return revivalTime; }
        set { revivalTime = value; }
    }

    private float knuckBackPower = 1f;
    public float KnuckBackPower
    {
        get { return knuckBackPower; }
        set { knuckBackPower = value; }
    }

    [SerializeField]
    private AIState characterState = AIState.Idle;

    private void Awake()
    {
        ani = this.GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CapsuleCollider2D>();
        character = this.GetComponent<CharacterStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        InputKey();
    }
    private void Update()
    {
        //ChangeState();
        //CurState();
        AquireRay();
        MouseTargeting();
        //Perception();
    }
    //public void ChangeState()
    //{
    //    if(IsDied())
    //    {
    //        character.IsStatusUpdate = true;
    //        characterState = AIState.Died;
    //    }
    //    else
    //    {
    //        if (character.IsDamaged)
    //        {
    //            character.IsStatusUpdate = true;
    //            StartCoroutine(Blink());
    //        }

    //        if (!IsMove())
    //            characterState = AIState.Idle;
    //        else
    //            characterState = AIState.Walk;

    //        if (Input.GetKey(KeyCode.LeftControl))
    //            characterState = AIState.Attack;
    //        else if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            if (!skillController.IsCoolTime[0])
    //            {
    //                if (skillController.ActiveSkills[0].skillType == 0)
    //                {
    //                    if (character.Target)
    //                    {
    //                        skillController.UseSkill(skillController.ActiveSkills[0], character.Target);
    //                    }
    //                    else
    //                        Debug.Log("타겟이 없음");
    //                }
    //                else if (skillController.ActiveSkills[0].skillType == 1)
    //                {
    //                    if (character.AllyTarget)
    //                    {
    //                        skillController.UseSkill(skillController.ActiveSkills[0], character.AllyTarget);
    //                    }
    //                    else
    //                        Debug.Log("타겟이 없음");
    //                }
    //            }
    //            else
    //                Debug.Log("첫 번째 스킬 쿨타임 중");
    //        }
    //        else if(Input.GetKeyDown(KeyCode.X))
    //        {
    //            if (!skillController.IsCoolTime[1])
    //            {
    //                if (skillController.ActiveSkills[1].skillType == 0)
    //                {
    //                    if (character.Target)
    //                    {
    //                        skillController.UseSkill(skillController.ActiveSkills[1], character.Target);
    //                    }
    //                    else
    //                        Debug.Log("타겟이 없음");
    //                }
    //                else if (skillController.ActiveSkills[1].skillType == 1)
    //                {
    //                    if (character.AllyTarget)
    //                    {
    //                        skillController.UseSkill(skillController.ActiveSkills[1], character.AllyTarget);
    //                    }
    //                    else
    //                        Debug.Log("타겟이 없음");
    //                }
    //            }
    //            else
    //                Debug.Log("두 번째 스킬 쿨타임 중");
    //        }
    //        else if(Input.GetKeyDown(KeyCode.C))
    //        {

    //            if (!skillController.IsCoolTime[2])
    //            {
    //                if (skillController.ActiveSkills[2].skillType == 0)
    //                {
    //                    if (character.Target)
    //                    {
    //                        skillController.UseSkill(skillController.ActiveSkills[2], character.Target);
    //                    }
    //                    else
    //                        Debug.Log("타겟이 없음");
    //                }
    //                else if(skillController.ActiveSkills[2].skillType == 1)
    //                {
    //                    if (character.AllyTarget)
    //                    {
    //                        skillController.UseSkill(skillController.ActiveSkills[2], character.AllyTarget);
    //                    }
    //                    else
    //                        Debug.Log("타겟이 없음");
    //                }
    //            }
    //            else
    //                Debug.Log("세 번째 스킬 쿨타임 중");
    //        }
    //    }

    //}
    public void CurState()
    {
        txtMesh.text = characterState.ToString();
        if(delayTime < character.AtkSpeed)
            delayTime += Time.deltaTime;
        AnimationDirection();

        switch (characterState)
        {
            case AIState.Idle:
                PlayerIdle();
                break;
            case AIState.Walk:
                PlayerRun();
                break;
            case AIState.Attack:
                PlayerAttack(character.AttackType);
                break;
            case AIState.Died:
                StartCoroutine(Died());
                break;
        }
    }
    public void MouseTargeting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("아군 클릭");

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,0f,LayerMask.GetMask("Ally"));

            if (hit.rigidbody)
            {
                if (GetDistance(this.transform.position, hit.rigidbody.transform.position) <= character.SeeRange)
                {
                    if (character.AllyTarget)
                    {
                        character.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    }
                    Debug.Log("아군 타겟팅 " + hit.rigidbody.gameObject.name);
                    TargetAlly(hit.rigidbody.gameObject);
                }
                else
                    Debug.Log("대상이 너무 멀리있습니다.");
            }
            else
            {
                if (character.AllyTarget)
                {
                    character.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    character.AllyTarget = null;
                }
            }
        }
        if (character.AllyTarget)
        {
            if (GetDistance(this.transform.position, character.AllyTarget.transform.position) >= character.SeeRange)
            {
                character.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                character.AllyTarget = null;

            }
        }
    }
    public void PlayerIdle() 
    {
        ActiveLayer(LayerName.IdleLayer);
        rig.velocity = Vector2.zero;
    }
    public void PlayerRun()
    {
        // 움직임 실행
        ActiveLayer(LayerName.WalkLayer);
        rig.velocity = character.Speed * dir;
    }
    public void InputKey()
    {
        // 키입력
        dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDir = Vector2.left;
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDir = Vector2.right;
            dir.x = 1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lookDir = Vector2.up;
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = Vector2.down;
            dir.y = -1;
        }            
    }
    public bool IsMove()
    {
        // 움직이고 있는지 확인
        if (Mathf.Abs(dir.x) > 0 || Mathf.Abs(dir.y) > 0)
            return true;
        else
            return false;
    }
    public bool IsDied()
    {
        int hp = character.CurHp;
        if (hp <= 0)
            return true;
        else
            return false;
    }
    private int AttackTypeDamage()
    {
        // 물리 데미지와 마법 데미지 구분
        if (character.AttackType < 1f)
            return character.PhysicalDamage;
        else
            return character.MagicalDamage;
    }
    private void PlayerAttack(float _weaponType)
    {
        // 공격실행
        rig.velocity = Vector2.zero;
        ActiveLayer(LayerName.AttackLayer);
        ani.SetFloat("AtkType", _weaponType);

        if (!IsDelay())
            StartCoroutine(AttackState());
    }
    private IEnumerator AttackState()
    {
        // 공격 애니메이션 실행
        ani.SetTrigger("AtkTrigger");
        delayTime = 0f;
        isAtk = true;
        while (!ani.GetCurrentAnimatorStateInfo(2).IsName("PlayerAttack") )
        {
            //전환 중일 때 실행되는 부분
            if(isAtk)
                DamageEnemy(AttackRange());
            yield return null;
        }
        isAtk = false;
    }
    public void DamageEnemy(RaycastHit2D[] hits)
    {
        // 범위안에 있는 적들에게 데미지
        for (int i =0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();
            enemy.CurHp -= ReviseDamage(AttackTypeDamage(), enemy.DefensivePower);

            if (IsLastHit(enemy))
                character.CurExp += enemy.DefeatExp;
        }
    }
    public bool IsLastHit(EnemyStatus _enemy)
    {
        // 마지막 공격을 했는지 체크
        if (isAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }

    public RaycastHit2D[] AttackRange()
    {
        // 히트박스를 만들어내고 범위안에 들어온 적들을 반환
        var hits = Physics2D.CircleCastAll(this.transform.position, character.AtkRange, lookDir, 1f,LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, lookDir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus hitsEnemy = hits[i].rigidbody.GetComponent<EnemyStatus>();
                UIManager.Instance.Notice(hitsEnemy.ObjectName);
                hitsEnemy.IsDamaged = true;
            }
        }
        else
            Debug.Log("아무것도 없음");
        return hits;
    }
    private void AquireRay()
    {
        itemSight = Physics2D.CircleCastAll(this.transform.position, 1f, Vector2.up, 0, LayerMask.GetMask("Item"));
        for (int i = 0; i < itemSight.Length; i++)
        {
            if (itemSight[i])
            {
                DropItem dropItem = itemSight[i].collider.GetComponent<DropItem>();
                InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(dropItem.CurItemKey));
                DropManager.Instance.ReturnItem(dropItem.gameObject);
            }
        }
    }
    public bool IsDelay()
    {
        float atkSpeed = character.AtkSpeed;
        if (delayTime >= atkSpeed)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void AnimationDirection()
    {
        if(characterState != AIState.Died)
        {
            if (dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
            else if (dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private IEnumerator Blink()
    {
        character.IsDamaged = false;        
        bodySprites.color = new Color(1f,1f,1f,155/255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }

    private IEnumerator Died()
    {
        rig.velocity = Vector2.zero;
        col.enabled = false;
        dir = Vector2.zero;
        ActiveLayer(LayerName.DieLayer); 
        yield return new WaitForSeconds(revivalTime);
        Rivive();
    }

    private void Rivive()
    {
        rig.isKinematic = false;
        col.enabled = true;
        character.CurHp = character.MaxHp;
        characterState = AIState.Idle;
    }

    public void ActiveLayer(LayerName layerName)
    {
        for(int i = 0; i < ani.layerCount; i++)
        {
            ani.SetLayerWeight(i, 0);
        }
        ani.SetLayerWeight((int)layerName, 1);
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rig.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
    public void Perception()
    {
        atkRangeRay = Physics2D.CircleCast(this.transform.position, character.AtkRange, character.Dir, 0, LayerMask.GetMask("Enemy"));
        TargetCloseEnemy();
    }
    public void TargetCloseEnemy()
    {
        sightRay.AddRange(Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy")));
        SortSightRayList(sightRay);
        if (sightRay.Count > 0)
            TargetEnemy(sightRay[0].collider.gameObject);

        if (character.Target)
        {
            if (GetDistance(this.transform.position, character.Target.transform.position) >= character.SeeRange)
            {
                character.Target.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                character.Target = null;

            }
        }
    }
    public void SortSightRayList(List<RaycastHit2D> _inventory)
    {
        // 리스트 정렬
        _inventory.Sort(delegate (RaycastHit2D a, RaycastHit2D b)
        {
            if (GetDistance(this.transform.position, a.rigidbody.position) < GetDistance(this.transform.position, b.rigidbody.position)) return -1;
            else if (GetDistance(this.transform.position, a.rigidbody.position) > GetDistance(this.transform.position, b.rigidbody.position)) return 1;
            else return 0;

        });
    }
    public void TargetEnemy(GameObject _target)
    {
        Debug.Log("타겟팅");
        if(_target)
        {
            if(character.Target)
            {
                character.Target.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
            }
            character.Target = _target;
            character.Target.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;
        }
    }
    public void TargetAlly(GameObject _allyTarget)
    {
        Debug.Log("타겟팅");
        if (_allyTarget)
        {
            if (character.AllyTarget)
            {
                character.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
            }
            character.AllyTarget = _allyTarget;
            character.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;
        }
    }

    public void ChangeState(CharacterStatus _status)
    {
        
    }

    public void State(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public void Perception(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public void Idle(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public void Chase(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator Died(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public void Damaged(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public bool IsDied(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }

    public int AttackTypeDamage(CharacterStatus _status)
    {
        throw new System.NotImplementedException();
    }
}
    
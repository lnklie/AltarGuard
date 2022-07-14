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
public class PlayerController : CharacterController
{
    private PlayerStatus player = null;
    private SpriteRenderer bodySprites = null;
    private Vector2 lookDir = Vector2.down;
    [SerializeField]
    public override void Awake()
    {
        base.Awake();
        player = this.GetComponent<PlayerStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(!player.IsAutoMode)
        {
            if(!player.IsAtk)
            {
                if (InputArrowKey(player))
                    PlayerMove(player);
                else
                    PlayerIdle(player);
            }
        }
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            player.IsAutoMode = !player.IsAutoMode;
        if (!IsDelay(player))
        {
            player.DelayTime = player.AtkSpeed;
        }
        else
        {
            player.DelayTime += Time.deltaTime;
        }
        if (player.IsAutoMode)
            base.Update();
        else
        {
            Perception(player);
            MouseTargeting(player);
            if (player.IsDamaged)
            {
                StartCoroutine(Blink(player));
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartCoroutine(PlayerAttack(player));
            }

            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!skillController.IsCoolTime[0])
                {
                    if (skillController.ActiveSkills[0].skillType == 0)
                    {
                        if (player.Target)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[0], player.Target);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                    else if (skillController.ActiveSkills[0].skillType == 1)
                    {
                        if (player.AllyTarget)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[0], player.AllyTarget);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                }
                else
                    Debug.Log("첫 번째 스킬 쿨타임 중");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                if (!skillController.IsCoolTime[1])
                {
                    if (skillController.ActiveSkills[1].skillType == 0)
                    {
                        if (player.Target)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[1], player.Target);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                    else if (skillController.ActiveSkills[1].skillType == 1)
                    {
                        if (player.AllyTarget)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[1], player.AllyTarget);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                }
                else
                    Debug.Log("두 번째 스킬 쿨타임 중");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {

                if (!skillController.IsCoolTime[2])
                {
                    if (skillController.ActiveSkills[2].skillType == 0)
                    {
                        if (player.Target)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[2], player.Target);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                    else if (skillController.ActiveSkills[2].skillType == 1)
                    {
                        if (player.AllyTarget)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[2], player.AllyTarget);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                }
                else
                    Debug.Log("세 번째 스킬 쿨타임 중");
            }
        }
        AquireRay();
    }

    public void MouseTargeting(PlayerStatus _status)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("아군 클릭");

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,0f,LayerMask.GetMask("Ally"));

            if (hit.rigidbody)
            {
                if (GetDistance(this.transform.position, hit.rigidbody.transform.position) <= _status.SeeRange)
                {
                    if (player.AllyTarget)
                    {
                        _status.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    }
                    Debug.Log("아군 타겟팅 " + hit.rigidbody.gameObject.name);
                    TargetAlly(hit.rigidbody.GetComponent<CharacterStatus>());
                }
                else
                    Debug.Log("대상이 너무 멀리있습니다.");
            }
            else
            {
                if (_status.AllyTarget)
                {
                    _status.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    _status.AllyTarget = null;
                }
            }
        }
        if (_status.AllyTarget)
        {
            if (GetDistance(this.transform.position, _status.AllyTarget.transform.position) >= _status.SeeRange)
            {
                _status.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                _status.AllyTarget = null;

            }
        }
    }
    public void PlayerIdle(PlayerStatus _status) 
    {
        _status.ActiveLayer(LayerName.IdleLayer);
        _status.Rig.velocity = Vector2.zero;
    }
    public void PlayerMove(PlayerStatus _status)
    {
        // 움직임 실행
        _status.ActiveLayer(LayerName.WalkLayer);
        _status.Rig.velocity = _status.Speed * _status.Dir;
        AnimationDirection(_status);
    }
    public bool InputArrowKey(PlayerStatus _status)
    {
        // 키입력
        _status.Dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDir = Vector2.left;
            _status.Dir = Vector2.left;

            return true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDir = Vector2.right;
            _status.Dir = Vector2.right;
            return true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lookDir = Vector2.up;
            _status.Dir = Vector2.up;
            return true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = Vector2.down;
            _status.Dir = Vector2.down;
            return true;
        }
        return false;
    }
    public bool IsMove(PlayerStatus _status)
    {
        // 움직이고 있는지 확인
        if (Mathf.Abs(_status.Dir.x) > 0 || Mathf.Abs(_status.Dir.y) > 0)
            return true;
        else
            return false;
    }

    private IEnumerator PlayerAttack(PlayerStatus _status)
    {

        if (!IsDelay(_status))
        {
            _status.ActiveLayer(LayerName.AttackLayer);
            _status.Ani.SetTrigger("AtkTrigger");
            _status.IsAtk = true;
            _status.Rig.velocity = Vector2.zero;
            _status.DelayTime = 0f;
            _status.Ani.SetFloat("AtkType", _status.AttackType);
            
            if (_status.AttackType == 0f)
            {
                DamageEnemy(_status, AttackRange(_status));

            }
            else if (_status.AttackType == 0.5f)
            {
                yield return WaitUntilAnimatorPoint(_status.Ani, 2, "PlayerAttack", 0.65f);
                    ShotArrow(_status);
            }
            yield return WaitUntilAnimatorPoint(_status.Ani, 2, "PlayerAttack", 0.99f);
            Debug.Log("1111");
            _status.IsAtk = false;
        }
    }


    public void SetFalseIsAtk(PlayerStatus _status)
    {
        _status.IsAtk = false;
    }
    private IEnumerator AttackState(PlayerStatus _status)
    {
        // 공격 애니메이션 실행

        _status.IsAtk = true;
        while (!_status.Ani.GetCurrentAnimatorStateInfo(2).IsName("PlayerAttack") )
        {
            //전환 중일 때 실행되는 부분
            if(_status.IsAtk)
                DamageEnemy(_status,AttackRange(_status));
            yield return null;
        }
        //Debug.Log("애니메이션 종료");
        _status.IsAtk = false;
    }

    public void DamageEnemy(PlayerStatus _status, RaycastHit2D[] hits)
    {
        // 범위안에 있는 적들에게 데미지
        for (int i =0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();
            enemy.CurHp -= ReviseDamage(AttackTypeDamage(_status), enemy.DefensivePower);
            enemy.GetComponentInChildren<DamageTextController>().SetDamageText(ReviseDamage(AttackTypeDamage(_status), enemy.DefensivePower));
            Debug.Log("현재 적의 체력은 " + enemy.CurHp); 
            if (IsLastHit(enemy, _status))
                _status.CurExp += enemy.DefeatExp;
        }
    }
    public bool IsLastHit(EnemyStatus _enemy, CharacterStatus _status)
    {
        // 마지막 공격을 했는지 체크
        if (_status.IsAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }

    public override RaycastHit2D[] AttackRange(CharacterStatus _status)
    {
        // 히트박스를 만들어내고 범위안에 들어온 적들을 반환
        var hits = Physics2D.CircleCastAll(this.transform.position, _status.AtkRange, lookDir, 1f,LayerMask.GetMask("Enemy"));
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
        player.ItemSight = Physics2D.CircleCastAll(this.transform.position, 1f, Vector2.up, 0, LayerMask.GetMask("Item"));
        for (int i = 0; i < player.ItemSight.Length; i++)
        {
            if (player.ItemSight[i])
            {
                DropItem dropItem = player.ItemSight[i].collider.GetComponent<DropItem>();
                InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(dropItem.CurItemKey));
                DropManager.Instance.ReturnItem(dropItem.gameObject);
            }
        }
    }

    private IEnumerator Blink(CharacterStatus _status)
    {
        _status.IsDamaged = false;        
        bodySprites.color = new Color(1f,1f,1f,155/255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }

    private IEnumerator Died(PlayerStatus _status)
    {
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        _status.Dir = Vector2.zero;
        _status.ActiveLayer(LayerName.DieLayer); 
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive(_status);
    }

    private void Rivive(CharacterStatus _status)
    {
        _status.Rig.isKinematic = false;
        _status.Col.enabled = true;
        _status.CurHp = _status.MaxHp;
        _status.AIState = EAIState.Idle;
    }

    public void Perception(CharacterStatus _status)
    {
        if (_status.SightRayList.Count > 0)
        {
            _status.Distance = _status.Target.position - this.transform.position;
            _status.TargetDir = _status.Distance.normalized;
        }
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        CharacterStatus _enemyHitStatus = _enemyHit.collider.GetComponent<CharacterStatus>();
        if (_enemyHit && !CheckRayList(_enemyHitStatus, _status.SightRayList))
            _status.SightRayList.Add(_enemyHitStatus);

        SortSightRayList(_status.SightRayList);
        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        CharacterStatus _allyHitStatus = _allyHit.collider.GetComponent<CharacterStatus>();
        if (_allyHit && !CheckRayList(_allyHitStatus, _status.AllyRayList))
            _status.AllyRayList.Add(_allyHitStatus);

        

        if (_status.SightRayList.Count > 0)
        {
            TargetEnemy(_status.SightRayList[0]);
        }

        for (int i = 0; i < _status.SightRayList.Count; i++)
        {
            if (GetDistance(this.transform.position, _status.SightRayList[i].transform.position) >= _status.SeeRange
                || _status.SightRayList[i].transform.GetComponent<CharacterStatus>().AIState == EAIState.Died)
            {
                if (_status.Target == _status.SightRayList[i])
                {
                    _status.Target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    _status.Target = null;
                    _status.TargetDir = lookDir;
                    _status.AIState = EAIState.Idle;
                }
                _status.SightRayList.Remove(_status.SightRayList[i]);
            }
        }
    }

    public void TargetEnemy(Status _target)
    {
        if(_target)
        {
            if(player.Target)
            {
                player.Target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
            }
            player.Target = _target.TargetPos;
            player.Target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;
        }
    }
    public void TargetAlly(CharacterStatus _allyTarget)
    {
        Debug.Log("타겟팅");
        if (_allyTarget)
        {
            if (player.AllyTarget)
            {
                player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
            }
            player.AllyTarget = _allyTarget.TargetPos;
            player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;
        }
    }
    #region AI
    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.SightRayList.Count > 0)
        {
            _status.Distance = _status.Target.transform.position - this.transform.position;
            _status.TargetDir = _status.Distance.normalized;
        }
        if (_status.CurHp < 0f)
        {
            _status.AIState = EAIState.Died;
        }
        else
        {
            if (_status.IsDamaged)
            {
                _status.AIState = EAIState.Damaged;
            }
            else
            {
                if (_status.Target == null)
                {
                    _status.AIState = EAIState.Idle;
                }
                else
                {
                    _status.AIState = EAIState.Walk;
                    if (GetDistance(this.transform.position, _status.Target.transform.position) <= _status.AtkRange)
                    {
                        _status.AIState = EAIState.Attack;
                    }
                }
            }
        }
    }

    public void AnimationDirection(PlayerStatus _status)
    {
        if (_status.AIState != EAIState.Died)
        {
            if(_status.IsAutoMode)
            {
                if (_status.TargetDir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
                else if (_status.TargetDir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (_status.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
                else if (_status.Dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override void AIPerception(CharacterStatus _status)
    {
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        CharacterStatus _enemyHitStatus = _enemyHit.collider.GetComponent<CharacterStatus>();
        if (_enemyHit && !CheckRayList(_enemyHitStatus, _status.SightRayList))
            _status.SightRayList.Add(_enemyHitStatus);

        SortSightRayList(_status.SightRayList);
        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        CharacterStatus _allyHitStatus = _allyHit.collider.GetComponent<CharacterStatus>();
        if (_allyHit && !CheckRayList(_allyHitStatus, _status.AllyRayList))
            _status.AllyRayList.Add(_allyHitStatus);


        if (_status.SightRayList.Count > 0)
        {
            TargetEnemy(_status.SightRayList[0]);

        }
        for (int i = 0; i < _status.SightRayList.Count; i++)
        {
            if (GetDistance(this.transform.position, _status.SightRayList[i].transform.position) >= _status.SeeRange)
            {
                if (_status.Target == _status.SightRayList[i])
                {
                    _status.Target.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    _status.Target = null;
                }
                _status.SightRayList.Remove(_status.SightRayList[i]);
            }
        }
    }


    public override void AttackDamage(RaycastHit2D[] hits, CharacterStatus _status)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();

            enemy.CurHp -= ReviseDamage(AttackTypeDamage(_status), enemy.DefensivePower);
            _status.IsAtk = true;
            if (IsLastHit(enemy, _status))
            {
                Debug.Log("막타 경험치 확득");
                _status.CurExp += enemy.DefeatExp;
            }
        }
    }
    public override IEnumerator AIDied(CharacterStatus _status)
    {
        base.AIDied(_status);
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive(_status);
    }

    public override void AIDamaged(CharacterStatus _status)
    {
        base.AIDamaged(_status);
        StartCoroutine(Blink(_status));
    }
    #endregion
}
    
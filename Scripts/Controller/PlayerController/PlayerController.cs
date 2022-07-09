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

    public override void Awake()
    {
        base.Awake();
        player = this.GetComponent<PlayerStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(!player.IsAutoMode)
            InputKey(player);
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            player.IsAutoMode = !player.IsAutoMode;
        AquireRay();
        MouseTargeting(player);
        if (player.IsAutoMode)
            base.Update();
        else
        {
            Debug.Log("현재 시야 안에 있는 적의 수는 " + characterStatus.SightRayList.Count);
            ChangeState(player);
            CurState(player);
            Perception(player);
        }
    }
    public void ChangeState(CharacterStatus _status)
    {
        if (IsDied(_status))
        {
            _status.IsStatusUpdate = true;
            _status.AIState = EAIState.Died;
        }
        else
        {
            if (_status.IsDamaged)
            {
                _status.IsStatusUpdate = true;
                StartCoroutine(Blink(_status));
            }

            if (!IsMove(_status))
                _status.AIState = EAIState.Idle;
            else
                _status.AIState = EAIState.Walk;

            if (Input.GetKey(KeyCode.LeftControl))
                _status.AIState = EAIState.Attack;
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!skillController.IsCoolTime[0])
                {
                    if (skillController.ActiveSkills[0].skillType == 0)
                    {
                        if (_status.Target)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[0], _status.Target);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                    else if (skillController.ActiveSkills[0].skillType == 1)
                    {
                        if (_status.AllyTarget)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[0], _status.AllyTarget);
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
                        if (_status.Target)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[1], _status.Target);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                    else if (skillController.ActiveSkills[1].skillType == 1)
                    {
                        if (_status.AllyTarget)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[1], _status.AllyTarget);
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
                        if (_status.Target)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[2], _status.Target);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                    else if (skillController.ActiveSkills[2].skillType == 1)
                    {
                        if (_status.AllyTarget)
                        {
                            skillController.UseSkill(skillController.ActiveSkills[2], _status.AllyTarget);
                        }
                        else
                            Debug.Log("타겟이 없음");
                    }
                }
                else
                    Debug.Log("세 번째 스킬 쿨타임 중");
            }
        }

    }
    public void CurState(CharacterStatus _status)
    {
        txtMesh.text = _status.ToString();
        if(_status.DelayTime < player.AtkSpeed)
            _status.DelayTime += Time.deltaTime;
        AnimationDirection(_status);

        switch (_status.AIState)
        {
            case EAIState.Idle:
                PlayerIdle(_status);
                break;
            case EAIState.Walk:
                PlayerRun(_status);
                break;
            case EAIState.Attack:
                PlayerAttack(_status);
                break;
            case EAIState.Died:
                StartCoroutine(Died(_status));
                break;
        }
    }
    public void MouseTargeting(CharacterStatus _status)
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
                    TargetAlly(hit.rigidbody.gameObject);
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
    public void PlayerIdle(CharacterStatus _status) 
    {
        _status.ActiveLayer(LayerName.IdleLayer);
        _status.Rig.velocity = Vector2.zero;
    }
    public void PlayerRun(CharacterStatus _status)
    {
        // 움직임 실행
        _status.ActiveLayer(LayerName.WalkLayer);
        _status.Rig.velocity = _status.Speed * _status.Dir;
    }
    public void InputKey(CharacterStatus _status)
    {
        // 키입력
        _status.Dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDir = Vector2.left;
            _status.Dir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDir = Vector2.right;
            _status.Dir = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lookDir = Vector2.up;
            _status.Dir = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = Vector2.down;
            _status.Dir = Vector2.down;
        }            
    }
    public bool IsMove(CharacterStatus _status)
    {
        // 움직이고 있는지 확인
        if (Mathf.Abs(_status.Dir.x) > 0 || Mathf.Abs(_status.Dir.y) > 0)
            return true;
        else
            return false;
    }

    private void PlayerAttack(CharacterStatus _status)
    {
        // 공격실행
        _status.Rig.velocity = Vector2.zero;
        _status.ActiveLayer(LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", _status.AttackType);

        if (!IsDelay(_status))
            StartCoroutine(AttackState(_status));
    }
    private IEnumerator AttackState(CharacterStatus _status)
    {
        // 공격 애니메이션 실행
        _status.Ani.SetTrigger("AtkTrigger");
        _status.DelayTime = 0f;
        _status.IsAtk = true;
        while (!_status.Ani.GetCurrentAnimatorStateInfo(2).IsName("PlayerAttack") )
        {
            //전환 중일 때 실행되는 부분
            if(_status.IsAtk)
                DamageEnemy(_status,AttackRange(_status));
            yield return null;
        }
        _status.IsAtk = false;
    }
    public void DamageEnemy(CharacterStatus _status, RaycastHit2D[] hits)
    {
        // 범위안에 있는 적들에게 데미지
        for (int i =0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();
            enemy.CurHp -= ReviseDamage(AttackTypeDamage(_status), enemy.DefensivePower);

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

    private IEnumerator Died(CharacterStatus _status)
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
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if (_enemyHit && !CheckRayList(_enemyHit, _status.SightRayList))
            _status.SightRayList.Add(_enemyHit);

        SortSightRayList(_status.SightRayList);
        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_allyHit && !CheckRayList(_allyHit, _status.AllyRayList))
            _status.AllyRayList.Add(_allyHit);


        if (_status.SightRayList.Count > 0)
        {
            TargetEnemy(_status.SightRayList[0].collider.gameObject);
        }

        for (int i = 0; i < _status.SightRayList.Count; i++)
        {
            if (GetDistance(this.transform.position, _status.SightRayList[i].transform.position) >= _status.SeeRange)
            {
                if (_status.Target == _status.SightRayList[i])
                {
                    _status.Target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    _status.Target = null;
                }
                _status.SightRayList.Remove(_status.SightRayList[i]);
            }
        }
    }

    public void TargetEnemy(GameObject _target)
    {
        Debug.Log("타겟팅");
        if(_target)
        {
            if(player.Target)
            {
                player.Target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
            }
            player.Target = _target;
            player.Target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;
        }
    }
    public void TargetAlly(GameObject _allyTarget)
    {
        Debug.Log("타겟팅");
        if (_allyTarget)
        {
            if (player.AllyTarget)
            {
                player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
            }
            player.AllyTarget = _allyTarget;
            player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;
        }
    }
    #region AI
    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.SightRayList.Count > 0)
        {
            _status.Distance = _status.Target.transform.position - this.transform.position;
            _status.Dir = _status.Distance.normalized;
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



    public override void AIPerception(CharacterStatus _status)
    {
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if (_enemyHit && !CheckRayList(_enemyHit, _status.SightRayList))
            _status.SightRayList.Add(_enemyHit);

        SortSightRayList(_status.SightRayList);
        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_allyHit && !CheckRayList(_allyHit, _status.AllyRayList))
            _status.AllyRayList.Add(_allyHit);


        if (_status.SightRayList.Count > 0)
        {
            TargetEnemy(_status.SightRayList[0].collider.gameObject);

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
    
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
    private FlagController hitFlag = null;
    [SerializeField] private GameObject rivivePoint = null;

    public override void Awake()
    {
        base.Awake();
        player = this.GetComponent<PlayerStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }

    //public override void Start()
    //{
    //    return;
    //}
    public override void Update()
    {
        if(player.CurHp < 0f && !player.IsDied )
        {
            StartCoroutine(Died(player));
        }
        
        if(!player.IsDied)
        {
            PlayerState();
        }

        if (player.Target)
        {
            player.Distance = player.Target.position - this.transform.position;
            player.TargetDir = player.Distance.normalized;
        }

        if (!IsDelay(player))
        {
            player.DelayTime = player.TotalAtkSpeed;
        }
        else
        {
            player.DelayTime += Time.deltaTime;
        }
        DragFlag();
        if(player.IsAutoMode)
        {
            player.Flag.transform.position = this.transform.position;
            player.IsAutoMode = false;
        }

        if(player.IsPlayMode)
        {
            player.IsPlayMode = false;
        }
    }
    public void DragFlag()
    {
        if (Input.GetMouseButtonDown(0) && !player.IsUiOn)
        {
            RaycastHit2D _hitFlag = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, LayerMask.GetMask("Flag"));
            if (_hitFlag)
            {
                hitFlag = _hitFlag.collider.GetComponent<FlagController>();
                hitFlag.IsSelect = true;
            }
        }
        else if(Input.GetMouseButtonUp(0) && hitFlag != null &&hitFlag.IsSelect && !player.IsUiOn)
        {
            hitFlag.IsSelect = false;
            hitFlag = null;
        }
    } 
    public void PlayerState()
    {
        switch(player.PlayerState)
        {
            case EPlayerState.Play:
                if(!player.IsDied)
                    Perception(player);
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    StartCoroutine(PlayerAttack(player));
                }
                if (!player.IsAtk)
                {
                    if (InputArrowKey(player))
                        PlayerMove(player);
                    else
                        PlayerIdle(player);
                }
                break;
            case EPlayerState.AutoPlay:
                AIPerception(player);
                AIChangeState(player);
                AIState(player);
                break;
        }
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
        _status.Rig.velocity = _status.TotalSpeed * _status.Dir;
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
            _status.Ani.SetBool("IsAtk",true);
            _status.IsAtk = true;
            _status.Rig.velocity = Vector2.zero;
            _status.DelayTime = 0f;
            _status.Ani.SetFloat("AtkType", _status.AttackType);

            if (_status.AttackType == 0f)
            {
                DamageEnemy(_status);
            }
            else if (_status.AttackType == 0.5f)
            {
                yield return WaitUntilAnimatorPoint(_status.Ani, 2, "PlayerAttack", 0.65f);
                ShotArrow(_status);
            }

            yield return WaitUntilAnimatorPoint(_status.Ani, 2, "PlayerAttack", 0.99f);
            _status.Ani.SetBool("IsAtk", false);
            _status.IsAtk = false;
        }
    }

    public void DamageEnemy(PlayerStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, _status.AtkRange, lookDir, 1f, LayerMask.GetMask("Enemy"));
        // 범위안에 있는 적들에게 데미지
        if (hits.Length > 0)
        {
            for (int i =0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();
                _enemy.Damaged(AttackTypeDamage(_status));
                if (_enemy.IsLastHit())
                {
                    _status.AquireExp(_enemy);
                    bool[] _isDrops = _enemy.RandomChoose(_enemy.ItemDropProb, _status.TotalDropProbability);
                    for (int j = 0; j < 5; j++)
                    {
                        if (_isDrops[j])
                        {
                            InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(_enemy.ItemDropKey[j]));
                        }
                    }
                }
            }
        }
    }



    private IEnumerator Died(PlayerStatus _status)
    {
        _status.AIState = EAIState.Died;
        _status.IsDied = true;
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        _status.Dir = Vector2.zero;
        _status.ActiveLayer(LayerName.DieLayer); 
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive(_status);
        _status.IsDied = false;
    }

    private void Rivive(CharacterStatus _status)
    {
        this.gameObject.transform.position = rivivePoint.transform.position;
        _status.Rig.isKinematic = false;
        _status.Col.enabled = true;
        _status.CurHp = _status.MaxHp;
        _status.AIState = EAIState.Idle;
    }

    public void Perception(CharacterStatus _status)
    {
        RaycastHit2D[] _enemyHit = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if(_enemyHit.Length > 0)
        {
            for(int i = 0; i < _enemyHit.Length; i++)
            {
                EnemyStatus _enemyHitStatus = _enemyHit[i].collider.GetComponent<EnemyStatus>();
                if (!_enemyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum])
                {
                    _status.EnemyRayList.Add(_enemyHitStatus);
                    _enemyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum] = true;
                }
            }
        }

        RaycastHit2D[] _allyHit = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if(_allyHit.Length > 0)
        {
            for(int i =0; i < _allyHit.Length; i++)
            {
                CharacterStatus _allyHitStatus = _allyHit[i].collider.GetComponent<CharacterStatus>();
                if (!_allyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum])
                {
                    _status.AllyRayList.Add(_allyHitStatus);
                    _allyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum] = true;
                }
            }
        }

        if (_status.EnemyRayList.Count > 0)
        {
            TargetEnemy(_status.EnemyRayList[0]);
            SortSightRayList(_status.EnemyRayList);
            for (int i = 0; i < _status.EnemyRayList.Count; i++)
            {
                if (GetDistance(this.transform.position, _status.EnemyRayList[i].transform.position) >= _status.SeeRange
                    || _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().AIState == EAIState.Died)
                {
                    if (_status.EnemyRayList[i].TargetPos == _status.Target)
                    {
                        _status.Target.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                        _status.Target = null;
                        _status.TargetDir = lookDir;
                        _status.AIState = EAIState.Idle;
                    }
                    _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().IsAllyTargeted[((AllyStatus)_status).AllyNum] = false;
                    _status.EnemyRayList.Remove(_status.EnemyRayList[i]);
                }
            }
        }
        else
        {
            _status.Target = null;
        }

        if (_status.AllyRayList.Count > 0)
        {
            TargetAlly(_status.AllyRayList[0]);
            SortSightRayList(_status.AllyRayList);
            for (int i = 0; i < _status.AllyRayList.Count; i++)
            {
                if (GetDistance(this.transform.position, _status.AllyRayList[i].transform.position) >= _status.SeeRange
                    || _status.AllyRayList[i].transform.GetComponent<AllyStatus>().AIState == EAIState.Died)
                {
                    if (_status.AllyRayList[i].TargetPos == _status.AllyTarget)
                    {
                        _status.AllyTarget = null;
                    }
                    _status.AllyRayList[i].transform.GetComponent<AllyStatus>().IsAllyTargeted[((AllyStatus)_status).AllyNum] = false;
                    _status.AllyRayList.Remove(_status.AllyRayList[i]);
                }
            }
        }
        else
        {
            _status.AllyTarget = null;
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
    public void TargetAlly(Status _target)
    {
        if (_target)
        {
            player.AllyTarget = _target.TargetPos;
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
        if (_status.Target)
        {
            _status.Distance = _status.Target.position - _status.TargetPos.position;
            _status.TargetDir = _status.Distance.normalized;
        }
        if (_status.CurHp < 0f)
        {
            _status.IsDied = true;
            _status.AIState = EAIState.Died;
        }
        else
        {

            if (_status.Target == null)
            {
                if (pathFindController.FinalNodeList.Count == 0)
                    _status.AIState = EAIState.Idle;
                else
                    _status.AIState = EAIState.Chase;
            }
            else
            {
                _status.AIState = EAIState.Chase;
                if (skillController.SkillQueue.Count > 0 && !skillController.IsSkillDelay &&
                    GetDistance(this.transform.position, _status.Target.transform.position) <= skillController.SkillQueue[0].skillRange)
                {
                    _status.AIState = EAIState.UseSkill;
                }
                else if (GetDistance(this.transform.position, _status.Target.transform.position) <= _status.TotalAtkRange)
                {
                    _status.AIState = EAIState.Attack;
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
        RaycastHit2D[] _enemyHit = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if(_enemyHit.Length > 0)
        {
            for(int i =0; i < _enemyHit.Length; i++)
            {
                EnemyStatus _enemyHitStatus = _enemyHit[i].collider.GetComponent<EnemyStatus>();  
                if (!_enemyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum])
                {
                    _status.EnemyRayList.Add(_enemyHitStatus);
                    _enemyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum] = true;
                }
            }
        }

        RaycastHit2D[] _allyHit = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_allyHit.Length > 0)
        {
            for(int i= 0; i < _allyHit.Length; i++)
            {
                CharacterStatus _allyHitStatus = _allyHit[i].collider.GetComponent<CharacterStatus>();
                if (!_allyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum])
                {

                    _status.AllyRayList.Add(_allyHitStatus);
                    _allyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum] = true;
                }
            }
        }
        if (_status.EnemyRayList.Count > 0)
        {
            TargetEnemy(_status.EnemyRayList[0]);
            SortSightRayList(_status.EnemyRayList);
            for (int i = 0; i < _status.EnemyRayList.Count; i++)
            {
                if (GetDistance(this.transform.position, _status.EnemyRayList[i].transform.position) >= _status.SeeRange
                    || _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().AIState == EAIState.Died)
                {
                    if (_status.EnemyRayList[i].TargetPos == _status.Target)
                    {
                        _status.Target.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                        _status.Target = null;
                        _status.TargetDir = lookDir;
                        _status.AIState = EAIState.Idle;
                    }
                    _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().IsAllyTargeted[((AllyStatus)_status).AllyNum] = false;
                    _status.EnemyRayList.Remove(_status.EnemyRayList[i]);
                }
            }
        }
        else
        {
            _status.Target = null;
        }

          
        if (_status.AllyRayList.Count > 0)
        {
            TargetAlly(_status.AllyRayList[0]);
            SortSightRayList(_status.AllyRayList);
            for (int i = 0; i < _status.AllyRayList.Count; i++)
            {
                if (GetDistance(this.transform.position, _status.AllyRayList[i].transform.position) >= _status.SeeRange
                    || _status.AllyRayList[i].transform.GetComponent<AllyStatus>().AIState == EAIState.Died)
                {
                    if (_status.AllyRayList[i].TargetPos == _status.AllyTarget)
                    {
                        _status.AllyTarget = null;
                    }
                    _status.AllyRayList[i].transform.GetComponent<AllyStatus>().IsAllyTargeted[((AllyStatus)_status).AllyNum] = false;
                    _status.AllyRayList.Remove(_status.AllyRayList[i]);
                }
            }
        }
        else
        {
            _status.AllyTarget = null;
        }
    }


    public override void AttackDamage(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, _status.AtkRange, lookDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();

                _status.IsAtk = true;
                _enemy.Damaged(AttackTypeDamage(_status));
                if (_enemy.IsLastHit())
                {
                    _status.AquireExp(_enemy);
                    bool[] _isDrops = _enemy.RandomChoose(_enemy.ItemDropProb, player.TotalDropProbability);
                    for (int j = 0; j < 5; j++)
                    {
                        if (_isDrops[i])
                        {
                            InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(_enemy.ItemDropKey[j]));
                        }
                    }
                }
            }
        }
    }
    public override IEnumerator AIDied(CharacterStatus _status)
    {
        _status.AIState = EAIState.Died;
        _status.IsDied = true;
        _status.ActiveLayer(LayerName.DieLayer);
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive(_status);
        _status.IsDied = false;
    }

    #endregion
}
    
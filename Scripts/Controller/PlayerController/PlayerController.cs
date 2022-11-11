using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AllyController
{
    private PlayerStatus player = null;
    private SpriteRenderer bodySprites = null;
    private Vector2 lookDir = Vector2.down;
    private FlagController hitFlag = null;
    private Status preEnemyTarget = null;
    [SerializeField] private bool isControlOnAutoPlay = false;
    [SerializeField] private bool checkControlOnAutoPlay = true;
    [SerializeField] private GameObject rivivePoint = null;



    public bool CheckControlOnAutoPlay { set { checkControlOnAutoPlay = value; } }
    public override void Awake()
    {
        base.Awake();
        player = this.GetComponent<PlayerStatus>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }
    public override void Start()
    {
        base.Start();

    }
    public override void Update()
    {
        DragFlag();
        RaycastItem();
        

        if (character.Target)
            destination = character.Target.transform.position;
        else
            destination = character.Flag.transform.position;
        if (player.Target)
        {
            player.Distance = player.Target.transform.position - this.transform.position;
            player.TargetDir = player.Distance.normalized;
            AnimationDirection();
        }

        if (player.CurHp <= 0f && !player.IsDied )
        {
            StartCoroutine(Died());
        }
        
        if(!player.IsDied)
        {
            PlayerState();
            PlayerStateCondition();
        }
    }
    public void RaycastItem()
    {
        RaycastHit2D raycast = Physics2D.CircleCast(this.transform.position, 1f, Vector2.zero, 0f, LayerMask.GetMask("Item"));
        
        if (raycast)
        {
            DropItem _dropItem = raycast.collider.GetComponent<DropItem>();
            AquireItem(_dropItem.CurItem);
            DropManager.Instance.ReturnItem(_dropItem);
        } 
    }

    public void AquireItem(Item _item)
    {
        InventoryManager.Instance.AcquireItem(_item);
    }
    public void DragFlag()
    {
        if (Input.GetMouseButtonDown(0) && !UIManager.Instance.IsUIOn && !UIManager.Instance.IsLogScrolling)
        {
            RaycastHit2D _hitFlag = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, LayerMask.GetMask("Flag"));
            if (_hitFlag)
            {
                hitFlag = _hitFlag.collider.GetComponent<FlagController>();
                hitFlag.IsSelect = true;
            }
        }
        else if(Input.GetMouseButtonUp(0) && hitFlag != null &&hitFlag.IsSelect && !UIManager.Instance.IsUIOn)
        {
            hitFlag.IsSelect = false;
            hitFlag = null;
        }
    }
    public void PlayerStateCondition()
    {
        if (player.IsAutoMode && !isControlOnAutoPlay)
        {
            player.PlayerState = EPlayerState.AutoPlay;
        }
        else
        {
            player.PlayerState = EPlayerState.Play;
        }
    }
    public void PlayerState()
    {
        switch(player.PlayerState)
        {
            case EPlayerState.Play:
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    StartCoroutine(PlayerAttack());
                }
                if (!player.IsAtk)
                {
                    if (InputArrowKey())
                    {
                        PlayerMove();
                        if (checkControlOnAutoPlay)
                            player.Flag.transform.position = this.transform.position;
                    }
                    else
                    {
                        if (checkControlOnAutoPlay)
                            isControlOnAutoPlay = InputArrowKey();
                        PlayerIdle();
                    }
                }
                break;
            case EPlayerState.AutoPlay:
                if(checkControlOnAutoPlay)
                {
                    isControlOnAutoPlay = InputArrowKey();
                }    
                AIChangeState();
                stateMachine.DoUpdateState();
                break;
        }
    }

    //public void MouseTargeting()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("아군 클릭");

    //        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,0f,LayerMask.GetMask("Ally"));

    //        if (hit.rigidbody)
    //        {
    //            if (player.GetDistance(hit.rigidbody.transform.position) <= player.SeeRange)
    //            {
    //                if (player.AllyTarget)
    //                {
    //                    player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
    //                }
    //                Debug.Log("아군 타겟팅 " + hit.rigidbody.gameObject.name);
    //                TargetAlly(hit.rigidbody.GetComponent<CharacterStatus>());
    //            }
    //            else
    //                Debug.Log("대상이 너무 멀리있습니다.");
    //        }
    //        else
    //        {
    //            if (player.AllyTarget)
    //            {
    //                player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
    //                player.AllyTarget = null;
    //            }
    //        }
    //    }
    //    if (player.AllyTarget)
    //    {
    //        if (player.GetDistance(player.AllyTarget.transform.position) >= player.SeeRange)
    //        {
    //            player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
    //            player.AllyTarget = null;

    //        }
    //    }
    //}
    public void PlayerIdle() 
    {
        player.ActiveLayer(ELayerName.IdleLayer);
        player.Rig.velocity = Vector2.zero;
    }
    public void PlayerMove()
    {
        // 움직임 실행
        player.ActiveLayer(ELayerName.WalkLayer);
        player.Rig.velocity = player.TotalStatus[(int)EStatus.Speed] * player.Dir;
        
    }
    public bool InputArrowKey()
    {
        // 키입력
        player.Dir = new Vector2(0, 0);
        bool _bool = true;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDir = Vector2.left;
            player.Dir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDir = Vector2.right;
            player.Dir = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lookDir = Vector2.up;
            player.Dir = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = Vector2.down;
            player.Dir = Vector2.down;
        }
        else
            _bool = false;
        return _bool;
    }
    public bool IsMove()
    {
        // 움직이고 있는지 확인
        if (Mathf.Abs(player.Dir.x) > 0 || Mathf.Abs(player.Dir.y) > 0)
            return true;
        else
            return false;
    }

    private IEnumerator PlayerAttack()
    {

        if (!IsDelay())
        {
            player.ActiveLayer(ELayerName.AttackLayer);
            player.Ani.SetBool("IsAtk",true);
            player.IsAtk = true;
            player.Rig.velocity = Vector2.zero;
            player.DelayTime = 0f;
            player.Ani.SetFloat("AtkType", player.AttackType);

            if (player.AttackType == 0f)
            {
                DamageEnemy();
            }
            else if (player.AttackType == 0.5f)
            {
                yield return WaitUntilAnimatorPoint(player.Ani, 2, "PlayerAttack", 0.65f);
                ShotArrow();
            }

            yield return WaitUntilAnimatorPoint(player.Ani, 2, "PlayerAttack", 0.99f);
            player.Ani.SetBool("IsAtk", false);
            player.IsAtk = false;
        }
    }

    public void DamageEnemy()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, player.TotalStatus[(int)EStatus.AtkRange], lookDir, 1f, LayerMask.GetMask("Enemy"));
        // 범위안에 있는 적들에게 데미지
        if (hits.Length > 0)
        {
            for (int i =0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();
                _enemy.Damaged(AttackTypeDamage());
                if (_enemy.IsLastHit())
                {
                    _enemy.IsDied = true;
                    _enemy.SetKilledAlly(player);
                    player.AquireExp(_enemy);
                }
            }
        }
    }



    private IEnumerator Died()
    {
        player.AIState = EAIState.Died;
        player.IsDied = true;
        player.Rig.velocity = Vector2.zero;
        player.Col.enabled = false;
        player.Dir = Vector2.zero;
        player.ActiveLayer(ELayerName.DieLayer); 
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive();
        player.IsDied = false;
    }

    private void Rivive()
    {
        this.gameObject.transform.position = rivivePoint.transform.position;
        player.Rig.isKinematic = false;
        player.Col.enabled = true;
        player.CurHp = (int)player.TotalStatus[(int)EStatus.MaxHp];
        player.AIState = EAIState.Idle;
    }
    public override void Targeting(int _layer)
    {
        // 레이쏘기 -> 적 발견 -> 리스트에 넣기 -> 타겟팅 -> 리스트의 적이 멀어졌거나 타겟이 죽었다면 리스트에서 제거 후 재정렬  

        RaycastHit2D _hit = Physics2D.CircleCast(this.transform.position, player.SeeRange, Vector2.up, 0, _layer);
        if (_hit)
        {

            CharacterStatus _hitStatus = _hit.collider.GetComponent<CharacterStatus>();
            player.Target = _hitStatus;


            if (player.GetDistance(player.Target.transform.position) >= player.SeeRange
                || player.Target.IsDied)
            {

                _hitStatus.IsAllyTargeted[player.AllyNum] = false;
                player.Target = null;

            }

        }
        else
            player.Target = null;
    }
    //public override void Targeting(List<AllyStatus> _targetList)
    //{
    //    RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, player.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
    //    if (_hit.Length > 0)
    //    {
    //        for (int i = 0; i < _hit.Length; i++)
    //        {
    //            AllyStatus _hitStatus = _hit[i].collider.GetComponent<AllyStatus>();
    //            if (!_hitStatus.IsAllyTargeted[player.AllyNum])
    //            {
    //                _targetList.Add(_hitStatus);
    //                _hitStatus.IsAllyTargeted[player.AllyNum] = true;
    //            }
    //        }

    //        for (int i = 0; i < _targetList.Count; i++)
    //        {
    //            if (player.GetDistance(_targetList[i].transform.position) >= player.SeeRange
    //                || _targetList[i].IsDied)
    //            {

    //                if (_targetList[i] == player.Target)
    //                {
    //                    player.Target = null;
    //                }

    //                _targetList[i].IsAllyTargeted[player.AllyNum] = false;
    //                _targetList.Remove(_targetList[i]);
    //            }
    //        }
    //    }
    //}

    //public override void ResortTarget(List<AllyStatus> _targetList)
    //{
    //    if (_targetList.Count > 0)
    //    {
    //        switch(player.AllyTargetIndex)
    //        {
    //            case EAllyTargetingSetUp.OneSelf:
    //                player.Target = player;
    //                break;
    //            case EAllyTargetingSetUp.CloseAlly:
    //                SortSightRayListByDistance(_targetList);
    //                player.Target = _targetList[1];
    //                break;
    //            case EAllyTargetingSetUp.Random:
    //                player.Target = ChooseSightRayListByRandom(_targetList);
    //                break;
    //            case EAllyTargetingSetUp.WeakAlly:
    //                SortSightRayListByCurHp(_targetList);
    //                player.Target = _targetList[0];
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        player.Target = null;
    //    }
    //}
    public void Perception()
    {
        if(player.Target == null)
            Targeting(LayerMask.GetMask("Enemy"));

    }
    public void SetTarget(Transform _target,Transform _object, bool _isTargetingBox = false)
    {

        if (_target && _isTargetingBox)
        {
            _target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
        }

        _target = _object;
        if(_isTargetingBox)
            _target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;

    }

    #region AI
    public override void AIChangeState()
    {
        if (player.CurHp <= 0f && !player.IsDied)
        {
            stateMachine.SetState(stateDic[EAIState.Died]);
        }

        if(!player.IsDied)
        {
            if (player.Target == null)
            {
                if (pathFindController.startPos == pathFindController.targetPos)
                    stateMachine.SetState(stateDic[EAIState.Idle]);
                else
                {
                    stateMachine.SetState(stateDic[EAIState.Chase]);
                }
            }
            else
            {
                if (IsSkillQueue() && !skillController.IsSkillDelay && IsSkillRange(player.Target))
                {
                    stateMachine.SetState(stateDic[EAIState.UseSkill]);
                }
                else if (IsAtkRange(player.Target))
                {
                    stateMachine.SetState(stateDic[EAIState.Attack]);
                }
                else
                {
                    stateMachine.SetState(stateDic[EAIState.Chase]);
                }
            }

        }
    }

    public override void AnimationDirection()
    {
        if (player.AIState != EAIState.Died)
        {
            if(player.IsAutoMode && !isControlOnAutoPlay)
            {
                if (player.TargetDir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
                else if (player.TargetDir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (player.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
                else if (player.Dir.x < 0) this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override IEnumerator AIPerception()
    {
        while(true)
        {
            Targeting(LayerMask.GetMask("Enemy"));


            yield return new WaitForSeconds(0.5f);
        }
    }


    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, player.TotalStatus[(int)EStatus.AtkRange], lookDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();

                player.IsAtk = true;
                _enemy.Damaged(AttackTypeDamage());
                if (_enemy.IsLastHit())
                {
                    _enemy.IsDied = true;
                    _enemy.SetKilledAlly(player);
                    player.AquireExp(_enemy);
                }
            }
        }
    }
    public override IEnumerator AIDied()
    {
        player.AIState = EAIState.Died;
        player.IsDied = true;
        player.ActiveLayer(ELayerName.DieLayer);
        player.Rig.velocity = Vector2.zero;
        player.Col.enabled = false;
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive();
        player.IsDied = false;
    }

    #endregion
}
    
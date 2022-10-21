using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AllyController
{
    private PlayerStatus player = null;
    private SpriteRenderer bodySprites = null;
    private Vector2 lookDir = Vector2.down;
    private FlagController hitFlag = null;
    private EnemyController preEnemyTarget = null;
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
     
    public override void Update()
    {
        DragFlag();
        if (characterStatus.EnemyTarget)
            destination = characterStatus.EnemyTarget.transform.position;
        else
            destination = characterStatus.Flag.transform.position;

        if (player.CurHp <= 0f && !player.IsDied )
        {
            StartCoroutine(Died());
        }
        
        if(!player.IsDied)
        {
            PlayerState();
            PlayerStateCondition();
        }

        if (player.EnemyTarget)
        {
            player.Distance = player.EnemyTarget.transform.position - this.transform.position;
            player.TargetDir = player.Distance.normalized;
        }


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
                Debug.Log("현재 " + checkControlOnAutoPlay);
                if(checkControlOnAutoPlay)
                {
                    isControlOnAutoPlay = InputArrowKey();
                    Debug.Log("여기 " + isControlOnAutoPlay);
                }    
                AIChangeState();
                AIState();
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
        player.Rig.velocity = player.TotalSpeed * player.Dir;
        AnimationDirection();
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
        var hits = Physics2D.CircleCastAll(this.transform.position, player.AtkRange, lookDir, 1f, LayerMask.GetMask("Enemy"));
        // 범위안에 있는 적들에게 데미지
        if (hits.Length > 0)
        {
            for (int i =0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();
                _enemy.Damaged(AttackTypeDamage());
                if (_enemy.IsLastHit())
                {
                    player.AquireExp(_enemy);
                    bool[] _isDrops = _enemy.RandomChoose(_enemy.ItemDropProb, player.TotalDropProbability);
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
        player.CurHp = player.TotalMaxHp;
        player.AIState = EAIState.Idle;
    }
    public void Targeting(List<EnemyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, player.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                EnemyController _hitStatus = _hit[i].collider.GetComponent<EnemyController>();
                if (!_hitStatus.IsAllyTargeted[player.AllyNum])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsAllyTargeted[player.AllyNum] = true;
                }
            }
        }
    }
    public void Targeting(List<AllyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, player.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                AllyController _hitStatus = _hit[i].collider.GetComponent<AllyController>();
                if (!_hitStatus.IsAllyTargeted[player.AllyNum])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsAllyTargeted[player.AllyNum] = true;
                }
            }
        }
    }
    public void ResortTarget(List<EnemyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            SortSightRayListByDistance(_targetList);
            player.EnemyTarget = _targetList[0];

            if (preEnemyTarget != null && preEnemyTarget != player.EnemyTarget)
                preEnemyTarget.SetTargetingBox(false);

            preEnemyTarget = player.EnemyTarget;
            player.EnemyTarget.SetTargetingBox(true);
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (player.GetDistance(_targetList[i].transform.position) >= player.SeeRange
                    || _targetList[i].IsDied())
                {

                    if (_targetList[i] == player.EnemyTarget)
                    {
                        player.EnemyTarget.SetTargetingBox(false);
                        player.EnemyTarget = null;
                    }

                    _targetList[i].IsAllyTargeted[player.AllyNum] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
            
        }
        else
        {
            player.EnemyTarget = null;
        }
    }
    public void ResortTarget(List<AllyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            switch(player.AllyTargetIndex)
            {
                case EAllyTargetingSetUp.OneSelf:
                    player.AllyTarget = this;
                    break;
                case EAllyTargetingSetUp.CloseAlly:
                    SortSightRayListByDistance(_targetList);
                    player.AllyTarget = _targetList[1];
                    break;
                case EAllyTargetingSetUp.Random:
                    player.AllyTarget = ChooseSightRayListByRandom(_targetList);
                    break;
                case EAllyTargetingSetUp.WeakAlly:
                    SortSightRayListByCurHp(_targetList);
                    player.AllyTarget = _targetList[0];
                    break;
            }
           
            
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (player.GetDistance(_targetList[i].transform.position) >= player.SeeRange
                    || _targetList[i].IsDied())
                {

                    if (_targetList[i] == player.AllyTarget)
                    {
                        player.AllyTarget = null;
                    }

                    _targetList[i].IsAllyTargeted[player.AllyNum] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
        }
        else
        {
            player.AllyTarget = null;
        }
    }
    public void Perception()
    {
        Targeting(player.EnemyRayList);
        ResortTarget(player.EnemyRayList);

        Targeting(player.AllyRayList);
        ResortTarget(player.AllyRayList);
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
        if (player.EnemyTarget)
        {
            player.Distance = player.EnemyTarget.transform.position - player.TargetPos.position;
            player.TargetDir = player.Distance.normalized;
        }
        if (player.CurHp < 0f)
        {
            player.IsDied = true;
            player.AIState = EAIState.Died;
        }
        else
        {
            if (player.EnemyTarget == null)
            {
                if (pathFindController.FinalNodeList.Count == 0)
                    player.AIState = EAIState.Idle;
                else
                    player.AIState = EAIState.Chase;
            }
            else
            {
                player.AIState = EAIState.Chase;
                if (skillController.SkillQueue.Count > 0 && !skillController.IsSkillDelay &&
                    player.GetDistance(player.EnemyTarget.transform.position) <= skillController.SkillQueue[0].skillRange)
                {
                    player.AIState = EAIState.UseSkill;
                }
                else if (player.GetDistance(player.EnemyTarget.transform.position) <= player.TotalAtkRange)
                {
                    player.AIState = EAIState.Attack;
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
            Targeting(player.EnemyRayList);
            ResortTarget(player.EnemyRayList);

            Targeting(player.AllyRayList);
            ResortTarget(player.AllyRayList);
            yield return new WaitForSeconds(0.5f);
        }
    }


    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, player.AtkRange, lookDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();

                player.IsAtk = true;
                _enemy.Damaged(AttackTypeDamage());
                if (_enemy.IsLastHit())
                {
                    player.AquireExp(_enemy);
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
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            StartCoroutine(Died());
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

        if (!IsDelay())
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
                    Perception();
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    StartCoroutine(PlayerAttack());
                }
                if (!player.IsAtk)
                {
                    if (InputArrowKey())
                        PlayerMove();
                    else
                        PlayerIdle();
                }
                break;
            case EPlayerState.AutoPlay:
                AIPerception();
                AIChangeState();
                AIState();
                break;
        }
    }

    public void MouseTargeting()
    {
        if(Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,0f,LayerMask.GetMask("Ally"));

            if (hit.rigidbody)
            {
                if (player.GetDistance(hit.rigidbody.transform.position) <= player.SeeRange)
                {
                    if (player.AllyTarget)
                    {
                        player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    }
                    Debug.Log("�Ʊ� Ÿ���� " + hit.rigidbody.gameObject.name);
                    TargetAlly(hit.rigidbody.GetComponent<CharacterStatus>());
                }
                else
                    Debug.Log("����� �ʹ� �ָ��ֽ�ϴ�.");
            }
            else
            {
                if (player.AllyTarget)
                {
                    player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                    player.AllyTarget = null;
                }
            }
        }
        if (player.AllyTarget)
        {
            if (player.GetDistance(player.AllyTarget.transform.position) >= player.SeeRange)
            {
                player.AllyTarget.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                player.AllyTarget = null;

            }
        }
    }
    public void PlayerIdle() 
    {
        player.ActiveLayer(LayerName.IdleLayer);
        player.Rig.velocity = Vector2.zero;
    }
    public void PlayerMove()
    {
        player.ActiveLayer(LayerName.WalkLayer);
        player.Rig.velocity = player.TotalSpeed * player.Dir;
        AnimationDirection();
    }
    public bool InputArrowKey()
    {
        player.Dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDir = Vector2.left;
            player.Dir = Vector2.left;

            return true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDir = Vector2.right;
            player.Dir = Vector2.right;
            return true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lookDir = Vector2.up;
            player.Dir = Vector2.up;
            return true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = Vector2.down;
            player.Dir = Vector2.down;
            return true;
        }
        return false;
    }
    public bool IsMove()
    {
        if (Mathf.Abs(player.Dir.x) > 0 || Mathf.Abs(player.Dir.y) > 0)
            return true;
        else
            return false;
    }

    private IEnumerator PlayerAttack()
    {

        if (!IsDelay())
        {
            player.ActiveLayer(LayerName.AttackLayer);
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
        player.ActiveLayer(LayerName.DieLayer); 
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
    public void Targeting(int _layer, List<Status> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, player.SeeRange, Vector2.up, 0, _layer);
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                CharacterStatus _hitStatus = _hit[i].collider.GetComponent<CharacterStatus>();
                if (!_hitStatus.IsAllyTargeted[player.AllyNum])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsAllyTargeted[player.AllyNum] = true;
                }
            }
        }
    }
    public void ResortTarget(List<Status> _targetList, bool _isEnemy, bool _isTargetingBox = false, Transform _defaultTransform = null)
    {
        if (_targetList.Count > 0)
        {
            if (_isEnemy)
                player.Target = _targetList[0].TargetPos;
            else
                player.AllyTarget = _targetList[0].TargetPos;
            SortSightRayList(_targetList);
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (player.GetDistance(_targetList[i].transform.position) >= player.SeeRange
                    || _targetList[i].transform.GetComponent<CharacterStatus>().AIState == EAIState.Died)
                {
                    if (_isEnemy)
                    {

                        if (_targetList[i].TargetPos == player.Target)
                        {
                            if (_isTargetingBox)
                                player.Target.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = false;
                            player.Target = _defaultTransform;
                        }
                    }
                    else
                    {
                        if (_targetList[i].TargetPos == player.AllyTarget)
                        {
                            player.AllyTarget = _defaultTransform;
                        }
                    }

                    _targetList[i].transform.GetComponent<CharacterStatus>().IsAllyTargeted[player.AllyNum] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
        }
        else
        {
            if (_isEnemy)
                player.Target = _defaultTransform;
            else
                player.AllyTarget = _defaultTransform;
        }

        _target = _object;
        if(_isTargetingBox)
            _target.gameObject.transform.parent.GetComponentInChildren<TargetingBoxController>().IsTargeting = true;

    }
    public void Perception()
    {
        Targeting(LayerMask.GetMask("Enemy"),player.EnemyRayList);
        ResortTarget(player.EnemyRayList,true, true);

        Targeting(LayerMask.GetMask("Ally"),player.AllyRayList);
        ResortTarget(player.AllyRayList, false, false);
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
    public void TargetAlly(Status _target)
    {
        if (_target)
        {
            player.AllyTarget = _target.TargetPos;
        }
    }
    public void TargetAlly(CharacterStatus _allyTarget)
    {
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
    public override void AIChangeState()
    {
        if (player.Target)
        {
            player.Distance = player.Target.position - player.TargetPos.position;
            player.TargetDir = player.Distance.normalized;
        }
        if (player.CurHp < 0f)
        {
            player.IsDied = true;
            player.AIState = EAIState.Died;
        }
        else
        {
            if (player.Target == null)
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
                    player.GetDistance(player.Target.transform.position) <= skillController.SkillQueue[0].skillRange)
                {
                    player.AIState = EAIState.UseSkill;
                }
                else if (player.GetDistance(player.Target.transform.position) <= player.TotalAtkRange)
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
            if(player.IsAutoMode)
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

    public override void AIPerception()
    {
        Targeting(LayerMask.GetMask("Enemy"), player.EnemyRayList);
        ResortTarget(player.EnemyRayList, player.Target);

        Targeting(LayerMask.GetMask("Ally"), player.AllyRayList);
        ResortTarget(player.AllyRayList,player.AllyTarget);
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
        player.ActiveLayer(LayerName.DieLayer);
        player.Rig.velocity = Vector2.zero;
        player.Col.enabled = false;
        yield return new WaitForSeconds(player.RevivalTime);
        Rivive();
        player.IsDied = false;
    }

    #endregion
}
    
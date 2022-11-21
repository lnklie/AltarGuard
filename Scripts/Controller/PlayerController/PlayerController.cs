using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AllyController
{
    [SerializeField] private bool isControlOnAutoPlay = false;
    [SerializeField] private bool checkControlOnAutoPlay = true;

    private PlayerStatus player = null;
    private SpriteRenderer bodySprites = null;

    private FlagController hitFlag = null;
    private Status preEnemyTarget = null;


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
        base.Update();
        DragFlag();
        RaycastItem();
        MouseTargeting();
        if (player.AIState != EAIState.Died)
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
            InventoryManager.Instance.AcquireItem(_dropItem.CurItem);
            _dropItem.gameObject.SetActive(false);
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
            player.PlayerMode = EPlayerMode.AutoPlay;
        }
        else
        {
            player.PlayerMode = EPlayerMode.ManualPlay;
        }
    }
    public void PlayerState()
    {
        switch(player.PlayerMode)
        {
            case EPlayerMode.ManualPlay:
                ChangeState();
                if (checkControlOnAutoPlay)
                {
                    isControlOnAutoPlay = InputArrowKey();
                }
                break;
            case EPlayerMode.AutoPlay:
                if(checkControlOnAutoPlay)
                {
                    isControlOnAutoPlay = InputArrowKey();
                }    
                AIChangeState();
                stateMachine.DoUpdateState();
                break;
        }
    }
    public void ChangeState()
    {
        if (player.CurHp <= 0f)
        {
            StartCoroutine(Died());
        }
        else
        {
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
                    PlayerIdle();
                }
            }
        }
    }

    public void MouseTargeting()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, LayerMask.GetMask("Ally","Enemy"));

            if (hit.rigidbody)
            {
                if (player.GetDistance(hit.rigidbody.transform.position) <= player.SeeRange)
                {
                    player.Target = hit.rigidbody.GetComponent<CharacterStatus>();
                    if (preEnemyTarget != null)
                        preEnemyTarget.SetTargetingBox(false);
                    preEnemyTarget = character.Target;
                    character.Target.SetTargetingBox(true);
                }
                else
                    Debug.Log("대상이 너무 멀리있습니다.");
            }
        }
    }
    public override void CheckTarget()
    {
        if (character.Target)
        {
            if (character.GetDistance(character.Target.transform.position) >= character.SeeRange || character.Target.IsDied)
            {
                character.Target.SetTargetingBox(false);
                character.Target.IsAllyTargeted[player.AllyNum] = false;
                character.Target = null;
            }
        }
    }
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
        player.CurRevivalTime = player.MaxRevivalTime;
        player.IsDied = true;
        player.Rig.velocity = Vector2.zero;
        player.Col.enabled = false;
        player.Dir = Vector2.zero;
        player.ActiveLayer(ELayerName.DieLayer); 
        yield return new WaitForSeconds(player.MaxRevivalTime);
        Rivive();
        player.IsDied = false;
    }

    public void Perception()
    {
        if(player.Target == null)
            Targeting();
    }


    public override void AnimationDirection()
    {
        if (player.AIState != EAIState.Died)
        {
            if(player.IsAutoMode && !isControlOnAutoPlay)
            {
                if (pathFindController.targetPos.x > this.transform.position.x)
                    this.transform.localScale = new Vector3(-1, 1, 1);
                else
                    this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (player.Dir.x > 0)
                    this.transform.localScale = new Vector3(-1, 1, 1);
                else if (player.Dir.x < 0) 
                    this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public override void Targeting(bool _isAlly = false)
    {
        if (!_isAlly)
        {
            RaycastHit2D _hit = Physics2D.CircleCast(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
            if (_hit)
            {
                CharacterStatus _hitStatus = _hit.collider.GetComponent<CharacterStatus>();
                character.Target = _hitStatus;
                if (preEnemyTarget != null)
                    preEnemyTarget.SetTargetingBox(false);
                preEnemyTarget = character.Target;
                character.Target.SetTargetingBox(true);
            }
            else
                character.Target = null;
        }
        else
        {
            List<RaycastHit2D> _hit = new List<RaycastHit2D>();
            _hit.AddRange(Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally")));
            if (_hit.Count > 0)
            {
                switch (character.AllyTargetIndex)
                {
                    case EAllyTargetingSetUp.OneSelf:
                        character.Target = character;
                        break;
                    case EAllyTargetingSetUp.CloseAlly:
                        SortSightRayListByDistance(_hit);
                        character.Target = _hit[1].collider.GetComponent<CharacterStatus>();
                        break;
                    case EAllyTargetingSetUp.Random:
                        character.Target = ChooseSightRayListByRandom(_hit).collider.GetComponent<CharacterStatus>();
                        break;
                    case EAllyTargetingSetUp.WeakAlly:
                        SortSightRayListByCurHp(_hit);
                        character.Target = _hit[0].collider.GetComponent<CharacterStatus>();
                        break;
                    default:
                        if (preEnemyTarget != null)
                            preEnemyTarget.SetTargetingBox(false);
                        preEnemyTarget = character.Target;
                        character.Target.SetTargetingBox(true);
                        break;
                }

            }
        }
    }
}
    
using System.Collections;
using UnityEngine;

/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : MercenaryController.cs
==============================
*/
public class MercenaryController : CharacterController
{
    protected MercenaryStatus mercenary = null;
    [SerializeField] private GameObject rivivePoint = null;

    public override  void Awake()
    {
        base.Awake();
        mercenary = this.GetComponent<MercenaryStatus>();
    }

    private void Rivive(CharacterStatus _Status)
    {
        this.gameObject.transform.position = rivivePoint.transform.position;

        _Status.Rig.isKinematic = false;
        _Status.Col.enabled = true;
        _Status.CurHp = mercenary.TotalMaxHp;
        _Status.TriggerStatusUpdate = true;
        _Status.AIState = EAIState.Idle;
    }

    public bool IsLastHit(EnemyStatus _enemy, CharacterStatus _Status)
    {
        if (_Status.IsAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }

    public EAIState CheckBossState(CharacterStatus _Status)
    {
        return _Status.EnemyRayList[0].GetComponent<Rigidbody>().GetComponent<EnemyStatus>().AIState;
    }


    public override void AttackDamage(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position,_status.TotalAtkRange, _status.TargetDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();
                _status.IsAtk = true;

                _enemy.Damaged(AttackTypeDamage(_status));
                if(_enemy.IsLastHit())
                {
                    _status.AquireExp(_enemy);

                    bool[] _isDrops = _enemy.RandomChoose(_enemy.ItemDropProb, mercenary.TotalDropProbability);
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




    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.Target)
        {
            _status.Distance = _status.Target.transform.position - _status.TargetPos.position;
            _status.TargetDir = _status.Distance.normalized;
        }
        if (_status.CurHp < 0f && !_status.IsDied)
        {
            StartCoroutine(AIDied(_status));
        }

        if(!_status.IsDied)
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
                if(skillController.SkillQueue.Count > 0 && !skillController.IsSkillDelay &&
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



    public override void AIPerception(CharacterStatus _status)
    {
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, UnityEngine.Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if(_enemyHit)
        {
            EnemyStatus _enemyHitStatus = _enemyHit.collider.GetComponent<EnemyStatus>();  
            if (!_enemyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum])
            {
                _status.EnemyRayList.Add(_enemyHitStatus);
                _enemyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum] = true;
            }
        }

        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, UnityEngine.Vector2.up, 0, LayerMask.GetMask("Ally"));
        if(_allyHit)
        {
            CharacterStatus _allyHitStatus = _allyHit.collider.GetComponent<CharacterStatus>();
            if (!_allyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum])
            {
                _status.AllyRayList.Add(_allyHitStatus);
                _allyHitStatus.IsAllyTargeted[((AllyStatus)_status).AllyNum] = true;
            }
        }
        if (_status.EnemyRayList.Count > 0)
        {
            _status.Target = _status.EnemyRayList[0].TargetPos;
            SortSightRayList(_status.EnemyRayList);
            for (int i = 0; i < _status.EnemyRayList.Count; i++)
            {
                if (GetDistance(this.transform.position, _status.EnemyRayList[i].transform.position) >= _status.SeeRange
                    || _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().AIState == EAIState.Died)
                {
                    _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().IsAllyTargeted[((AllyStatus)_status).AllyNum] = false;
                    _status.EnemyRayList.Remove(_status.EnemyRayList[i]);
                }
            }
        }
        else
        {
            _status.Target = null;
        }
    }




    public override IEnumerator AIDied(CharacterStatus _status)
    {
        _status.AIState = EAIState.Died;
        _status.IsDied = true;
        _status.ActiveLayer(LayerName.DieLayer);
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        yield return new WaitForSeconds(mercenary.RevivalTime);
        Rivive(_status);
        _status.IsDied = false;
    }

}

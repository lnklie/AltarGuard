using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : EnemyAIController.cs
==============================
*/
public class EnemyController : CharacterController
{
    [SerializeField]
    private Status altar = null;
    [SerializeField]
    protected EnemyStatus enemyStatus = null;

    public override void Awake()
    {
        base.Awake();
        enemyStatus = GetComponent<EnemyStatus>();
        FindAltar(enemyStatus);
    }

    public void FindAltar(EnemyStatus _enemy)
    {
        _enemy.AltarRay = Physics2D.CircleCastAll(_enemy.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Ally"));
        for (int i = 0; i < _enemy.AltarRay.Length; i++)
        {
            if (_enemy.AltarRay[i].collider.gameObject.CompareTag("Altar"))
                altar = _enemy.AltarRay[i].rigidbody.GetComponent<Status>();
        }
    }
    public bool FrontOtherEnemy(RaycastHit2D _enemyHit, Status _enemy)
    {

        // 앞에 다른 적이 있는 지 확인

        if (_enemyHit.collider.gameObject != _enemy.gameObject)
        {
            return true;
        }
        else
            return false;
    }


    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.Target)
        {
            _status.Distance = _status.Target.position - this.transform.position;
            _status.TargetDir = _status.Distance.normalized;
        }
        if (_status.CurHp < 0f)
        {
            _status.AIState = EAIState.Died;
        }
        else
        {
            if (_status.Target == null)
            {
                _status.AIState = EAIState.Idle;
            }
            else
            {
                _status.AIState = EAIState.Chase;
                if (IsAtkRange(_status))
                {
                    _status.AIState = EAIState.Attack;
                }
            }

        }
    }


    public override void AIPerception(CharacterStatus _status)
    {
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if(_enemyHit)
        {
            Status _enemyHitStatus = _enemyHit.collider.GetComponent<Status>();
            if (_enemyHit && !CheckRayList(_enemyHitStatus, _status.SightRayList))
                _status.SightRayList.Add(_enemyHitStatus);
            SortSightRayList(_status.SightRayList);
        }

        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if(_allyHit)
        {
            Status _allyHitStatus = _allyHit.collider.GetComponent<Status>();
            if (_allyHit && !CheckRayList(_allyHitStatus, _status.AllyRayList))
                _status.AllyRayList.Add(_allyHitStatus);
        }

        if (!altar)
            _status.Target = _status.TargetPos;
        else
        {
            if (_status.SightRayList.Count > 0)
            {
                _status.Target = _status.SightRayList[0].TargetPos;
            }
            else
            {
                _status.Target = altar.TargetPos;
            }
        }



        for (int i = 0; i < _status.SightRayList.Count; i++)
        {
            if (GetDistance(this.transform.position, _status.SightRayList[i].transform.position) >= _status.SeeRange)
            {
                _status.SightRayList.Remove(_status.SightRayList[i]);
            }
        }

    }

    public override IEnumerator AIDied(CharacterStatus _status)
    {
        yield return null;
    }
    public Item DropItem(EnemyStatus _status)
    {
        // 아이템 드랍
        int _ranIndex = RandomChoose(_status.ItemDropProb);
        return DatabaseManager.Instance.SelectItem(_status.ItemDropKey[_ranIndex]);
    }
    private int RandomChoose(List<float> _probs)
    {
        // 무작위 선택
        float total = 0;

        foreach (float elem in _probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < _probs.Count; i++)
        {
            if (randomPoint < _probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= _probs[i];
            }
        }
        return _probs.Count - 1;
    }
    public override void AttackDamage(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, _status.AtkRange, _status.TargetDir, 1f, LayerMask.GetMask("Ally"));

        for (int i = 0; i < hits.Length; i++)
        {
            Status ally = hits[i].collider.GetComponent<Status>();
            ally.Damaged(AttackTypeDamage(_status));
            
        }
    }
}

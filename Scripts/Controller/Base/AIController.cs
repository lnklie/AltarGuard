using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : AIController.cs
==============================
*/
public abstract class AIController : BaseController
{
    public abstract void ChangeState(EnemyStatus _status);
    public abstract void State(EnemyStatus _status);
    public abstract void Perception(EnemyStatus _enemy);
    public abstract void Idle(EnemyStatus _enemy);
    public abstract void Chase(EnemyStatus _enemy);
    public abstract void Attack(EnemyStatus _enemy);
    public abstract IEnumerator Died(EnemyStatus _enemy);
    public abstract void Damaged(EnemyStatus _enemy);
    public abstract bool IsDied(EnemyStatus _enemy);

    public void SetState(EnemyStatus _enemy, EnemyState _enemyState)
    {
        _enemy.EnemyState = _enemyState;
        Debug.Log("현재상태는 " + _enemy.EnemyState + " 바꾸려는 상태는 " + _enemyState);
        _enemy.IsStateChange = true;
    }
    public void ActiveLayer(Animator _ani,LayerName layerName)
    {
        // 애니메이션 레이어 가중치 조절
        for (int i = 0; i < _ani.layerCount; i++)
        {
            _ani.SetLayerWeight(i, 0);
        }
        _ani.SetLayerWeight((int)layerName, 1);
    }
}

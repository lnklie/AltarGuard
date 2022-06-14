using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : AIController.cs
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
        Debug.Log("������´� " + _enemy.EnemyState + " �ٲٷ��� ���´� " + _enemyState);
        _enemy.IsStateChange = true;
    }
    public void ActiveLayer(Animator _ani,LayerName layerName)
    {
        // �ִϸ��̼� ���̾� ����ġ ����
        for (int i = 0; i < _ani.layerCount; i++)
        {
            _ani.SetLayerWeight(i, 0);
        }
        _ani.SetLayerWeight((int)layerName, 1);
    }
}

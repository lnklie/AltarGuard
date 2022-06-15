using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : GoblinArcherAIController.cs
==============================
*/
public class GoblinArcherAIController : EnemyAIController
{
    private void ShotArrow(EnemyStatus _object)
    {
        // ȭ����
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(_object, AttackTypeDamage(_object));
        }
        else
            Debug.Log("ȭ�� ����");
    }
    public override void Attack(EnemyStatus _object)
    {
        base.Attack( _object);
        _object.Ani.speed = 1 / _object.AtkSpeed;
        if (!_object.IsDelay())
        {
            _object.DelayTime = 0f;
            if (_object.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
            {
                Debug.Log("����");
                ShotArrow(_object);
            }
        }
    }
}

using System.Collections;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : RushEnemyAIController.cs
==============================
*/
public class RushEnemyAIController : EnemyController
{
    private RushEnemyStatus rushEnemyStatus = null;
    private void Awake()
    {
        rushEnemyStatus = GetComponent<RushEnemyStatus>();
    }
    private void Start()
    {
        FindAltar(rushEnemyStatus);
    }
    private void Update()
    {
        Perception(rushEnemyStatus);
        ChangeState(rushEnemyStatus);
        State(rushEnemyStatus);
    }

    
    public bool IsDelay(EnemyStatus _status)
    {
        if (_status.DelayTime >= _status.AtkSpeed)
        {
            _status.DelayTime = _status.AtkSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }





    public IEnumerator Knockback(float _knockbackDuration, float _knockbackPower, Transform _obj, Rigidbody2D _rig)
    {
        // 넉백 효과
        float timer = 0;

        while(_knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (_obj.transform.position - this.transform.position).normalized;
            _rig.AddForce(-direction * _knockbackPower);
        }

        yield return 0;
    }
}

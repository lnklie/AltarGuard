using System.Collections;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : RushEnemyAIController.cs
==============================
*/
public class RushEnemyController : EnemyController
{
    private RushEnemyStatus rushEnemyStatus = null;
    public override void Awake()
    {
        base.Awake();
        rushEnemyStatus = this.GetComponent<RushEnemyStatus>();
    }
    public bool IsDelay(EnemyStatus _status)
    {
        if (_status.DelayTime >= _status.TotalAtkSpeed)
        {
            _status.DelayTime = _status.TotalAtkSpeed;
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

    public override IEnumerator AIDied(CharacterStatus _status)
    {
        rushEnemyStatus.UpdateEnemyHp();
        _status.AIState = EAIState.Died;
        _status.ActiveLayer(LayerName.DieLayer);
        _status.TriggerStateChange = false;
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        yield return new WaitForSeconds(2f);
        _status.transform.parent.localScale = new Vector3(1, 1, 1);
        _status.transform.parent.gameObject.SetActive(false);
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
}

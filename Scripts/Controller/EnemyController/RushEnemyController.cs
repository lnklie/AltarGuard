using System.Collections;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : RushEnemyAIController.cs
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
        // �˹� ȿ��
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
        _status.ActiveLayer(LayerName.IdleLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = Vector2.zero;
        _status.Col.enabled = false;
        yield return new WaitForSeconds(2f);
        if (Random.Range(1, 101) <= 50)
        {
            InventoryManager.Instance.AcquireItem(DropItem(rushEnemyStatus));
            Debug.Log("ȹ��");
        }
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
}

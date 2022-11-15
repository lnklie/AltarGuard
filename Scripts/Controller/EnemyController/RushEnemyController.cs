using System.Collections;
using UnityEngine;

public class RushEnemyController : EnemyController
{
    private RushEnemyStatus rushEnemyStatus = null;
    private float returnTime = 1f;

    public override void Awake()
    {
        base.Awake();
        rushEnemyStatus = this.GetComponent<RushEnemyStatus>();
    }
    public override void Update()
    {
        base.Update();
        AIChangeState();
        stateMachine.DoUpdateState();
    }

    public IEnumerator Knockback(float _knockbackDuration, float _knockbackPower, Transform _obj, Rigidbody2D _rig)
    {
        // ³Ë¹é È¿°ú
        float timer = 0;

        while(_knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (_obj.transform.position - this.transform.position).normalized;
            _rig.AddForce(-direction * _knockbackPower);
        }

        yield return 0;
    }

    public override IEnumerator AIDied()
    {
        base.AIDied();
        rushEnemyStatus.UpdateEnemyHp();
        rushEnemyStatus.AIState = EAIState.Died;
        rushEnemyStatus.ActiveLayer(ELayerName.DieLayer);
        rushEnemyStatus.Rig.velocity = Vector2.zero;
        rushEnemyStatus.Col.enabled = false;
        yield return new WaitForSeconds(returnTime);
        bool[] _isDrops = {false, false, false, false, false};

        if (rushEnemyStatus.GetKilledAlly() != null)
            _isDrops = rushEnemyStatus.RandomChoose(rushEnemyStatus.ItemDropProb, rushEnemyStatus.GetKilledAlly().TotalStatus[(int)EStatus.DropProbability]);
        for (int j = 0; j < rushEnemyStatus.ItemDropProb.Count; j++)
        {
            if (_isDrops[j])
            {
                DropManager.Instance.DropItem(
                    ItemManager.Instance.CreateItem(
                        DatabaseManager.Instance.SelectItem(rushEnemyStatus.ItemDropKey[j])), this.gameObject.transform.position);
            }
        }
        rushEnemyStatus.transform.parent.localScale = new Vector3(1, 1, 1);
        rushEnemyStatus.transform.parent.gameObject.SetActive(false);
        rushEnemyStatus.AIState = EAIState.Idle;
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
}

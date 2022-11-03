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
        base.IsDied();
        rushEnemyStatus.UpdateEnemyHp();
        rushEnemyStatus.AIState = EAIState.Died;
        rushEnemyStatus.ActiveLayer(ELayerName.DieLayer);
        rushEnemyStatus.Rig.velocity = Vector2.zero;
        rushEnemyStatus.Col.enabled = false;
        yield return new WaitForSeconds(returnTime);
        rushEnemyStatus.transform.parent.localScale = new Vector3(1, 1, 1);
        rushEnemyStatus.transform.parent.gameObject.SetActive(false);
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
}

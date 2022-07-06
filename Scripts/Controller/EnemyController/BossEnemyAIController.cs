using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : BossEnemyAIController.cs
==============================
*/
public class BossEnemyController : EnemyController
{
    private BossEnemyStatus bossEnemyStatus = null;
    private void Awake()
    {
        bossEnemyStatus = GetComponent<BossEnemyStatus>();
    }
    private void Start()
    {
        FindAltar(bossEnemyStatus);
    }
    private void Update()
    {
        Perception(bossEnemyStatus);
        ChangeState(bossEnemyStatus);
        State(bossEnemyStatus);
    }



    public override void AnimationDirection(CharacterStatus _enemy)
    {
        // �ִϸ��̼� ����
        if (_enemy.Dir.x > 0) this.transform.localScale = new Vector3(-6, 6, 1);
        else if (_enemy.Dir.x < 0) transform.transform.localScale = new Vector3(6, 6, 1);
    }
    //public override IEnumerator Died(EnemyStatus _status)
    //{
    //    ActiveLayer(_status.Ani, LayerName.IdleLayer);
    //    _status.IsStateChange = false;
    //    _status.Rig.velocity = Vector2.zero;
    //    SetEnabled(_status, false);
    //    yield return new WaitForSeconds(2f);
    //    Debug.Log("����");
    //    DropManager.Instance.DropItem(this.transform.position, _status.ItemDropKey, _status.ItemDropProb);
    //    EnemySpawner.Instance.ReturnBossEnemy(this.gameObject);
    //}
}

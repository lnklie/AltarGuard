using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomizer : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField]
    private RushEnemyStatus rushEnemyStatus = null;
    [SerializeField]
    private RushEnemyController rushEnemyAIController = null;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    private void Awake()
    {
        rushEnemyStatus = this.GetComponent<RushEnemyStatus>();
    }
    private void Update()
    {
        if(rushEnemyAIController != null && isActive)
        {
            rushEnemyAIController.AIPerception(rushEnemyStatus);
            rushEnemyAIController.AIChangeState(rushEnemyStatus);
            rushEnemyAIController.AIState(rushEnemyStatus);
            rushEnemyStatus.Distance = rushEnemyStatus.Target.transform.position - this.transform.position;
            rushEnemyStatus.Dir = rushEnemyStatus.Distance.normalized;
        }
    }

    public void SetAIController(RushEnemyController _enemyAIController)
    {
        RushEnemyController _rushEnemyAIController = _enemyAIController;
        rushEnemyAIController = _rushEnemyAIController;
    }
    public void SetEnemyStatus(Enemy _rushenemy)
    {
        rushEnemyStatus.RushEnemy = _rushenemy;
        rushEnemyStatus.CurHp = rushEnemyStatus.MaxHp;
        rushEnemyStatus.IsEnemyChange = true;
    }
    public void SetAnimator(RuntimeAnimatorController _ani)
    {
        rushEnemyStatus.Ani.runtimeAnimatorController = _ani; 
    }
}

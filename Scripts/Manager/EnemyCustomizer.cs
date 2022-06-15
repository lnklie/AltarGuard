using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomizer : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField]
    private EnemyStatus enemyStatus = null;
    [SerializeField]
    private EnemyAIController enemyAIController = null;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    private void Awake()
    {
        enemyStatus = this.GetComponent<EnemyStatus>();
    }
    private void Update()
    {
        if(enemyAIController != null && isActive)
        {
            enemyAIController.Perception(enemyStatus);
            enemyAIController.ChangeState(enemyStatus);
            enemyAIController.State(enemyStatus);
            enemyStatus.Distance = enemyStatus.Target.transform.position - this.transform.position;
            enemyStatus.Dir = enemyStatus.Distance.normalized;
        }
    }
    public AIController GetAIController()
    {
        return enemyAIController;
    }
    public void SetAIController(EnemyAIController _enemyAIController)
    {
        enemyAIController = _enemyAIController;
    }
    public void SetEnemyStatus(Enemy _enemy)
    {
        enemyStatus.Enemy = _enemy;
        enemyStatus.CurHp = enemyStatus.MaxHp;
        enemyStatus.IsEnemyChange = true;
    }
    public void SetAnimator(RuntimeAnimatorController _ani)
    {
        enemyStatus.Ani.runtimeAnimatorController = _ani; 
    }
}

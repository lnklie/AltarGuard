using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomizer : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField]
    private RushEnemyStatus rushEnemyStatus = null;
    [SerializeField]
    private RushEnemyAIController rushEnemyAIController = null;
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
            rushEnemyAIController.Perception(rushEnemyStatus);
            rushEnemyAIController.ChangeState(rushEnemyStatus);
            rushEnemyAIController.State(rushEnemyStatus);
            rushEnemyStatus.Distance = rushEnemyStatus.Target.transform.position - this.transform.position;
            rushEnemyStatus.Dir = rushEnemyStatus.Distance.normalized;
        }
    }
    public AIController GetAIController()
    {
        return rushEnemyAIController;
    }
    public void SetAIController(RushEnemyAIController _enemyAIController)
    {
        RushEnemyAIController _rushEnemyAIController = _enemyAIController;
        rushEnemyAIController = _rushEnemyAIController;
    }
    public void SetEnemyStatus(RushEnemy _rushenemy)
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

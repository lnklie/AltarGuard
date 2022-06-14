using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomizer : MonoBehaviour
{
    protected Animator ani = null;
    public Animator Ani
    {
        get { return ani; }
        set { ani = value; }
    }

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
            Debug.Log("현재 상태는 " + enemyStatus.EnemyState);
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
}

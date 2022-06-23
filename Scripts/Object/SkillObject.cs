using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private Rigidbody2D rig = null;
    private CircleCollider2D col = null;

    [SerializeField]
    private int damage = 0;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private float maxDurationTime = 0f;
    public float MaxDuration
    {
        get { return maxDurationTime; }
        set { maxDurationTime = value; }
    }
    private float durationTime = 0f;

    private GameObject target = null;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }
    [SerializeField]
    private float maxCoolTime = 0f;
    public float MaxCoolTime
    {
        get { return maxCoolTime; }
        set { maxCoolTime = value; }
    }
    [SerializeField]
    private float coolTime = 0f;
    public float CoolTime
    {
        get { return coolTime; }
        set { coolTime = value; }
    }
    public void Awake()
    {
        rig = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        maxDurationTime = this.GetComponentInChildren<Animator>().speed;
    }
    private void Update()
    {
        if (target != null)
        {
            coolTime += Time.deltaTime;
            durationTime += Time.deltaTime;
            this.transform.position = target.transform.position;
            if (durationTime > maxDurationTime)
            {
                this.gameObject.SetActive(false);
                durationTime = 0f;
            }


        }
        else
        {
            Debug.Log("타겟이 없습니다.");
        }
    }
}

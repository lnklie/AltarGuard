using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : SlimeKingAIController.cs
==============================
*/
public class SlimeKingAIController : BossEnemyAIController
{
    private RaycastHit2D[] patternHit = default;
    [SerializeField]
    private SlimeKingPattern curPattern = SlimeKingPattern.JumpAttack;

    private bool targetHit = false;
    private bool decidedPattern = false;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Wait()
    {
        base.Wait();
        col.enabled = true;
        decidedPattern = false;
        targetHit = false;
    }
    public override void PatternCondition()
    {
        if(bossState == BossState.PatternAttack && decidedPattern == false)
        {
            int patternNum = Random.Range(1, 3);
            decidedPattern = true;
            ani.SetBool("IsPattern", true);
            ani.SetFloat("AttackPattern", patternNum - 1);
        }
    }
    public override void Pattern()
    {
        patternDelay += Time.deltaTime;
        col.enabled = false;
        switch (curPattern)
        {
            case SlimeKingPattern.NotPattern:
                Debug.Log("���� �ƹ� ���ϵ� ����");
                break;
            case SlimeKingPattern.JumpAttack:
                Debug.Log("���� ����");
                ani.SetFloat("AttackPattern", 0);
                maxPatternDelay = 7f;
                TargetDash(enemy.Dir, 2f);

                if (!isPatterning())
                {
                    SetDelayTime(0f);
                    InitPatternDelay();
                    ani.SetBool("IsPattern", false);
                }
                else
                {
                    if (patternDelay > 5.5f)
                    {
                        patternHit = Physics2D.BoxCastAll(this.transform.position, new Vector2(1f, 1f), 90f, Vector2.down, 1f, LayerMask.GetMask("Ally", "Altar"));
                        Debug.DrawRay(this.transform.position, Vector2.down * 1f, Color.red);
                        if (TargetHit())
                        {
                            TarGetDamage(false);
                            patternDelay = maxPatternDelay;
                        }
                    } 
                }
                break;
            case SlimeKingPattern.DashAttack:
                Debug.Log("��� ����");
                ani.SetFloat("AttackPattern", 0.5f);
                SetMaxPatternDelay(3f);
                TargetDash(enemy.Dir, 5f);
                patternHit = Physics2D.BoxCastAll(this.transform.position, new Vector2(3f, 3f), 90f, enemy.Dir , 2f, LayerMask.GetMask("Ally", "Altar"));
                Debug.DrawRay(this.transform.position, enemy.Dir * 3f, Color.red);
                if (!isPatterning())
                {
                    SetDelayTime(0f);
                    InitPatternDelay();
                    ani.SetBool("IsPattern", false);
                }
                else
                {
                    if (TargetHit())
                    {
                        TarGetDamage(true);
                        Debug.Log("����");
                    }
                }
                break;
        }
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        // �˹�
        float timer = 0; 
        Debug.Log("���� �� �˹�");
        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rig.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
    public void TarGetDamage(bool _isKnockBack)
    {
        // Ÿ��
        if(targetHit == false)
        {
            for(int i = 0; i < patternHit.Length; i++)
            {
                patternHit[i].collider.GetComponent<CharacterStatus>().CurHp--;
                if (patternHit[i].collider.tag == "Player" && _isKnockBack)
                {
                    StartCoroutine(patternHit[i].collider.GetComponent<PlayerController>().Knockback(1f,10f,this.transform));
                    Debug.Log("�˹�");
                }
                else if(patternHit[i].collider.tag == "Mercenary" && _isKnockBack)
                {
                    StartCoroutine(patternHit[i].collider.GetComponent<MercenaryAIController>().Knockback(1f, 10f, this.transform));
                    Debug.Log("�뺴 �˹�");
                }
            }
            targetHit = true;
            Debug.Log("Ÿ���� ����");
        }
    }

    public void TargetDash(Vector2 _dir, float _speed)
    {
        // Ÿ�ٿ��� �̵�
        rig.velocity = _dir * _speed;
    }

    public bool TargetHit()
    {
        // Ÿ���� �¾��� �� 
        if (patternHit.Length > 0)
        {
            rig.velocity = Vector2.zero;

            for(int i =0; i < patternHit.Length; i++)
            {
                if (patternHit[i].collider.tag == "Player")
                {
                    patternHit[i].collider.GetComponent<BaseController>().IsDamaged = true;
                    patternHit[i].collider.GetComponent<BaseController>().Debuff = Debuff.Slowed;
                }
            }
            Debug.Log("Ÿ����ҿ� ����"); 
            return true;
        }
        else
            return false;
    }
}

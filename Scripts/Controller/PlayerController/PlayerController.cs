using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : PlayerController.cs
==============================
*/
public class PlayerController : BaseController
{
    private CharacterStatus character = null;
    private SpriteRenderer bodySprites = null;
    private Animator ani = null;
    private Rigidbody2D rig = null;
    private BoxCollider2D col = null;
    private TextMesh txtMesh = null;
    private RaycastHit2D[] itemSight = default;
    private Vector2 dir = new Vector2(0, 0);
    private Vector2 lookDir = Vector2.down;

    private float delayTime = 0f;
    private bool isAtk = false;

    private float revivalTime = 5f;
    public float RevivalTime
    {
        get { return revivalTime; }
        set { revivalTime = value; }
    }

    private float knuckBackPower = 1f;
    public float KnuckBackPower
    {
        get { return knuckBackPower; }
        set { knuckBackPower = value; }
    }

    [SerializeField]
    private CharacterState characterState = CharacterState.Idle;
    private void Awake()
    {
        character = this.GetComponent<CharacterStatus>();
        ani = GetComponentInChildren<Animator>();
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
        txtMesh = GetComponentInChildren<TextMesh>();
    }

    private void FixedUpdate()
    {
        InputKey();
    }
    private void Update()
    {
        ChangeState();
        CurState();
        AquireRay();
    }
    public void ChangeState()
    {
        if(IsDied())
        {
            characterState = CharacterState.Died;
        }
        else
        {
            if (character.IsDamaged)
                StartCoroutine(Blink());

            if (!IsMove())
                characterState = CharacterState.Idle;
            else
                characterState = CharacterState.Walk;

            if (Input.GetKey(KeyCode.LeftControl))
                characterState = CharacterState.Attack;
        }

    }
    public void CurState()
    {
        txtMesh.text = characterState.ToString();
        if(delayTime < character.AtkSpeed)
            delayTime += Time.deltaTime;
        AnimationDirection();

        switch (characterState)
        {
            case CharacterState.Idle:
                PlayerIdle();
                break;
            case CharacterState.Walk:
                PlayerRun();
                break;
            case CharacterState.Attack:
                PlayerAttack(attackType);
                break;
            case CharacterState.Died:
                StartCoroutine(Died());
                break;
        }
    }

    public void PlayerIdle() 
    {
        ActiveLayer(LayerName.IdleLayer);
        rig.velocity = Vector2.zero;
    }
    public void PlayerRun()
    {
        // 움직임 실행
        ActiveLayer(LayerName.WalkLayer);
        rig.velocity = character.Speed * dir;
    }
    public void InputKey()
    {
        // 키입력
        dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDir = Vector2.left;
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDir = Vector2.right;
            dir.x = 1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lookDir = Vector2.up;
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = Vector2.down;
            dir.y = -1;
        }            
    }
    public bool IsMove()
    {
        // 움직이고 있는지 확인
        if (Mathf.Abs(dir.x) > 0 || Mathf.Abs(dir.y) > 0)
            return true;
        else
            return false;
    }
    public bool IsDied()
    {
        int hp = character.CurHp;
        if (hp <= 0)
            return true;
        else
            return false;
    }
    private int AttackTypeDamage()
    {
        // 물리 데미지와 마법 데미지 구분
        if (attackType < 1f)
            return character.PhysicalDamage;
        else
            return character.MagicalDamage;
    }
    private void PlayerAttack(float _weaponType)
    {
        // 공격실행
        rig.velocity = Vector2.zero;
        ActiveLayer(LayerName.AttackLayer);
        ani.SetFloat("AtkType", _weaponType);

        if (!IsDelay())
            StartCoroutine(AttackState());
    }
    private IEnumerator AttackState()
    {
        // 공격 애니메이션 실행
        ani.SetTrigger("AtkTrigger");
        delayTime = 0f;
        isAtk = true;
        while (!ani.GetCurrentAnimatorStateInfo(2).IsName("PlayerAttack") )
        {
            //전환 중일 때 실행되는 부분
            if(isAtk)
                DamageEnemy(AttackRange());
            yield return null;
        }
        isAtk = false;
    }
    public void DamageEnemy(RaycastHit2D[] hits)
    {
        // 범위안에 있는 적들에게 데미지
        for (int i =0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();
            enemy.CurHp -= ReviseDamage(AttackTypeDamage(), enemy.DefensivePower);
            if (enemy.CurHp <= 0)
                character.CurExp += enemy.DefeatExp;
        }
    }
    public RaycastHit2D[] AttackRange()
    {
        // 히트박스를 만들어내고 범위안에 들어온 적들을 반환
        var hits = Physics2D.CircleCastAll(this.transform.position, character.AtkRange, lookDir, 1f,LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, lookDir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus hitsEnemy = hits[i].rigidbody.GetComponent<EnemyStatus>();
                hitsEnemy.IsDamaged = true;
            }
        }
        else
            Debug.Log("아무것도 없음");
        return hits;
    }
    private void AquireRay()
    {
        itemSight = Physics2D.CircleCastAll(this.transform.position, 1f, Vector2.up, 0, LayerMask.GetMask("Item"));
        for (int i = 0; i < itemSight.Length; i++)
        {
            if (itemSight[i])
            {
                DropItem dropItem = itemSight[i].collider.GetComponent<DropItem>();
                InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(dropItem.CurItemKey));
                DropManager.Instance.ReturnItem(dropItem.gameObject);
            }
        }
    }
    public bool IsDelay()
    {
        float atkSpeed = character.AtkSpeed;
        if (delayTime >= atkSpeed)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void AnimationDirection()
    {
        if(characterState != CharacterState.Died)
        {
            if (dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
            else if (dir.x < 0) transform.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private IEnumerator Blink()
    {
         character.IsDamaged = false;        
         bodySprites.color = new Color(1f,1f,1f,155/255f);
         yield return new WaitForSeconds(0.5f);
         bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }

    private IEnumerator Died()
    {
        rig.velocity = Vector2.zero;
        col.enabled = false;
        dir = Vector2.zero;
        ActiveLayer(LayerName.DieLayer); 
        yield return new WaitForSeconds(revivalTime);
        Rivive();
    }

    private void Rivive()
    {
        rig.isKinematic = false;
        col.enabled = true;
        character.CurHp = character.MaxHp;
        characterState = CharacterState.Idle;
    }

    public void ActiveLayer(LayerName layerName)
    {
        for(int i = 0; i < ani.layerCount; i++)
        {
            ani.SetLayerWeight(i, 0);
        }
        ani.SetLayerWeight((int)layerName, 1);
    }
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rig.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
}
    

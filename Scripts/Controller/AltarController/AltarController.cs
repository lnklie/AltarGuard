using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : AltarController.cs
==============================
*/
public class AltarController : BaseController
{
    private AltarStatus altar = null;
    private TextMesh[] txtMesh = null;
    private SpriteRenderer[] spriteRenderers = null;
    private List<CharacterStatus> characters = new List<CharacterStatus>();

    void Awake()
    {
        altar = this.GetComponent<AltarStatus>();
        txtMesh = this.GetComponentsInChildren<TextMesh>();
        spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        FindAlly();
        UpdateBuffRange();
    }
    void Update()
    {
        ChangeState();
        State();
        BuffCharacters();
    }

    public void ChangeState()
    {
        // ���ǿ� �°� ���º���
        if (IsDestroyed())
        {
            SetState(AltarState.Destroyed);
        }
        else
        {
            if (isDamaged)
            {
                SetState(AltarState.Damaged);
                txtMesh[1].text = altar.CurHp.ToString();
            }
            else
            {
                SetState(AltarState.Idle);     
            }
        }

    }
    public bool IsDestroyed()
    {
        // �ı��Ǿ����� Ȯ��
        if (altar.CurHp < 0f)
            return true;
        else
            return false;
    }
    public void SetState(AltarState _alterState)
    {
        // ���� �Ҵ�
        isStateChange = true;
        altar.AltarState = _alterState;
    }
    public void State()
    {
        // ���º� �ൿ ��Ÿ��
        if(isStateChange)
            CheckState();
        switch (altar.AltarState)
        {
            case AltarState.Idle:
                Idle();
                break;
            case AltarState.Damaged:
                Damaged();
                break;
            case AltarState.Destroyed:
                Destroyed();
                break;
            default:
                Debug.Log("No State");
                break;
        }
    }
    public void CheckState()
    {
        // ���� üũ
        txtMesh[0].text = altar.AltarState.ToString();
    }
    private void Idle()
    {
        isStateChange = false;
    }
    private void Damaged()
    {
        isStateChange = false;
        IsDamaged = false;
    }
    private void Destroyed()
    {
        isStateChange = false;
    }
    public void UpgradeAltar(AltarAbility _altarAbility)
    {
        // ���� ���׷��̵�
        switch(_altarAbility)
        {
            case AltarAbility.Hp:
                altar.MaxHp += 10;
                break;
            case AltarAbility.DefensivePower:
                altar.DefensivePower += 5;
                break;
            case AltarAbility.Buff_Damage:
                altar.Buff_Damage++;
                break;
            case AltarAbility.Buff_DefensivePower:
                altar.Buff_DefensivePower++;
                break;
            case AltarAbility.Buff_Speed:
                altar.Buff_Speed++;
                break;
            case AltarAbility.Buff_Healing:
                altar.Buff_Healing++;
                break;
        }
    }
    public void BuffCharacters()
    {
        // ĳ�������� ���� �ο�
        for (int i = 0; i < characters.Count; i++)
        {
            if (GetDistance(this.transform.position, characters[i].transform.position) <= altar.BuffRange)
            {
                if (characters[i].IsAlterBuff == false)
                {
                    characters[i].IsAlterBuff = true;
                    characters[i].BuffPhysicalDamage += altar.Buff_Damage;
                    characters[i].BuffMagicalDamage += altar.Buff_Damage;
                    characters[i].BuffDefensivePower += altar.Buff_DefensivePower;
                    characters[i].BuffSpeed += 1 * altar.Buff_Speed;
                    Debug.Log(characters[i].name + " �� ĳ���� ���� ���� �޴� ��");
                }
            }
            else
            {
                if (characters[i].IsAlterBuff == true)
                {
                    characters[i].IsAlterBuff = false;
                    characters[i].BuffPhysicalDamage -= altar.Buff_Damage;
                    characters[i].BuffMagicalDamage -= altar.Buff_Damage;
                    characters[i].BuffDefensivePower -= altar.Buff_DefensivePower;
                    characters[i].BuffSpeed -= altar.Buff_Speed;
                    Debug.Log(characters[i].name + " �� ĳ���� ���� ���� ���� ");
                }
            }
        }
    }
    private float GetDistance(Vector2 _start, Vector2 _end)
    {
        // ������ �Ÿ� ����
        float x1 = _start.x;
        float y1 = _start.y;
        float x2 = _end.x;
        float y2 = _end.y;
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }
    private void UpdateBuffRange()
    {
        // ���� �Ÿ� ������Ʈ
        spriteRenderers[2].transform.localScale = new Vector2(altar.BuffRange / 10, altar.BuffRange / 10);
    }
    public void FindAlly()
    {
        // ���� ã��
        var hits = Physics2D.CircleCastAll(this.transform.position, 100f, Vector2.zero, 3f, LayerMask.GetMask("Ally"));
        ExceptThisObjectLayer(hits); 
    }
    public void ExceptThisObjectLayer(RaycastHit2D[] _raycastHit2Ds)
    {
        // ���� ĳ���͵��� ����Ʈ�� �ִ� ��
        for (int i = 0; i < _raycastHit2Ds.Length; i++)
        {
            if (_raycastHit2Ds[i].collider.gameObject != this.gameObject)
            {
                characters.Add(_raycastHit2Ds[i].collider.GetComponent<CharacterStatus>());
            }
        }
    }
}

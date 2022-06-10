using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-10
 * �ۼ��� : Inklie
 * ���ϸ� : AltarController.cs
==============================
*/
public class AltarController : BaseController
{
    private AltarStatus altar = null;
    private TextMesh[] txtMesh = null;
    [SerializeField]
    private SpriteRenderer[] spriteRenderers = null;
    [SerializeField]
    private List<CharacterStatus> characters = new List<CharacterStatus>();
    void Awake()
    {
        altar = this.GetComponent<AltarStatus>();
        txtMesh = this.GetComponentsInChildren<TextMesh>();
        spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        UpdateHp();
        UpdateBuffRange();
    }
    void Update()
    {
        FindInBuffRangeAlly();
        ChangeState();
        State();
        if (altar.IsAltarStatusChange)
        {
            BuffUpdate();
            altar.IsAltarStatusChange = false;
        }
    }
    public void UpdateHp()
    {
        if (altar != null)
        {
            altar.MaxHp = altar.HpLevel * 100;
            altar.CurHp = altar.MaxHp;
        }
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

    public void BuffUpdate()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].IsAlterBuff = true;
            characters[i].BuffPhysicalDamage = altar.BuffDamageLevel * 10;
            characters[i].BuffMagicalDamage = altar.BuffDamageLevel * 10;
            characters[i].BuffDefensivePower = altar.BuffDefensivePowerLevel * 10;
            characters[i].BuffSpeed = altar.BuffSpeedLevel * 0.1f;
            characters[i].UpdateAbility();
            UpdateBuffRange();
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
        spriteRenderers[2].transform.localScale = new Vector2(altar.BuffRangeLevel * 1f / 10, altar.BuffRangeLevel * 1f / 10);
    }
    public void FindInBuffRangeAlly()
    {
        // ���� ã��
        var hits = Physics2D.CircleCastAll(this.transform.position, altar.BuffRangeLevel * 1f, Vector2.zero, 0f, LayerMask.GetMask("Ally"));
        if(hits != null)
            AddBuffCharacterList(hits);
        RemoveBuffCharacterList(hits);
    }
    public void AddBuffCharacterList(RaycastHit2D[] _raycastHit2Ds)
    {
        // ���� ĳ���͵��� ����Ʈ�� �ִ� ��
        for (int i = 0; i < _raycastHit2Ds.Length; i++)
        {
            if (_raycastHit2Ds[i].collider.gameObject != this.gameObject && !_raycastHit2Ds[i].collider.GetComponent<CharacterStatus>().IsAlterBuff)
            {
                characters.Add(_raycastHit2Ds[i].collider.GetComponent<CharacterStatus>());
                Debug.Log("�����ȿ� ���� " + _raycastHit2Ds[i].collider.GetComponent<CharacterStatus>().ObjectName);
                BuffUpdate();
            }
        }
    }
    public void RemoveBuffCharacterList(RaycastHit2D[] _raycastHit2Ds)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (GetDistance(this.transform.position, characters[i].transform.position) >= altar.BuffRangeLevel * 1f)
            {
                characters[i].IsAlterBuff = false;
                characters.Remove(characters[i]);
            }
        }
    }
}

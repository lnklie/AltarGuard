using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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



    [SerializeField]
    private List<AllyStatus> characters = new List<AllyStatus>();
    public override void Awake()
    {
        altar = this.GetComponent<AltarStatus>();
        

    }
    private void Start()
    {
        SetHp();
        UpdateBuffRange();
    }
    void Update()
    {
        FindInBuffRangeAlly();
        ChangeState();
        State();
        if (altar.IsAltarStatusChange)
        {
            UpdateBuff();
            altar.IsAltarStatusChange = false;
        }
    }
    public void SetHp()
    {
        if (altar != null)
        {
            altar.MaxHp = altar.Hp;
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

            SetState(AltarState.Idle);     
            
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
        altar.TriggerStateChange = true;
        altar.AltarState = _alterState;
    }
    public void State()
    {
        // ���º� �ൿ ��Ÿ��
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

    private void Idle()
    {
        altar.TriggerStateChange = false;
    }
    private void Damaged()
    {
        altar.TriggerStateChange = false;
        altar.IsDamaged = false;
    }
    private void Destroyed()
    {
        altar.TriggerStateChange = false;
    }

    public void UpdateBuff()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].IsAlterBuff = true;
            characters[i].BuffPhysicalDamage = altar.BuffDamage;
            characters[i].BuffMagicalDamage = altar.BuffDamage;
            characters[i].BuffDefensivePower = altar.BuffDefensivePower;
            characters[i].BuffSpeed = altar.BuffSpeed;
            characters[i].BuffHpRegenValue = altar.BuffHpRegen;
            characters[i].UpdateAbility();
            UpdateBuffRange();
        }
    }
    public void RemoveBuff(AllyStatus _ally)
    {
        _ally.IsAlterBuff = false;
        _ally.BuffPhysicalDamage = 0;
        _ally.BuffMagicalDamage = 0;
        _ally.BuffDefensivePower = 0;
        _ally.BuffSpeed = 0;
        _ally.BuffHpRegenValue = 0;
        _ally.UpdateAbility();
    }
    private void UpdateBuffRange()
    {
        // ���� �Ÿ� ������Ʈ
        altar.BuffRangeSprite.transform.localScale = new Vector2(altar.BuffRange * 1f / 10, altar.BuffRange * 1f / 10);
    }

    public void FindInBuffRangeAlly()
    {
        // ���� ã��
        var hits = Physics2D.CircleCastAll(this.transform.position, altar.BuffRange * 1f, Vector2.zero, 0f, LayerMask.GetMask("Ally"));
        if (hits.Length > 0)
        {
            
            AddBuffCharacterList(hits);
        }
        RemoveBuffCharacterList();
    }
    public void AddBuffCharacterList(RaycastHit2D[] _raycastHit2Ds)
    {
        // ���� ĳ���͵��� ����Ʈ�� �ִ� ��
        for (int i = 0; i < _raycastHit2Ds.Length; i++)
        {
            if (!_raycastHit2Ds[i].collider.CompareTag("Altar") && !_raycastHit2Ds[i].collider.GetComponent<AllyStatus>().IsAlterBuff)
            {
                Debug.Log(_raycastHit2Ds[i].collider.GetComponent<AllyStatus>() + "����");
                characters.Add(_raycastHit2Ds[i].collider.GetComponent<AllyStatus>());
                UpdateBuff();
            }
        }
    }
    public void RemoveBuffCharacterList()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (GetDistance(this.transform.position, characters[i].transform.position) >= altar.BuffRange * 1f && characters[i].IsAlterBuff)
            {
                RemoveBuff(characters[i]);
                characters.Remove(characters[i]);
                Debug.Log("����");
            }
        }
    }
}

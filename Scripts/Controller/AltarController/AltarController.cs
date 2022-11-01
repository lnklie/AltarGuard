using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarController : MonoBehaviour
{
    [SerializeField] private List<AllyStatus> characters = new List<AllyStatus>();
    private AltarStatus altar = null;

    private void Awake()
    {
        altar = this.GetComponent<AltarStatus>();
    }
    private void Start()
    {
        SetHp();
        //UpdateHp();
        UpdateBuffRange();
    }
    void Update()
    {

        ChangeState();
        State();

    }
    public void SetHp()
    {
        if (altar != null)
        {
            //
        }
    }

    public void ChangeState()
    {
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

        if (altar != null && altar.CurHp < 0f)
            return true;
        else
            return false;
    }
    public void SetState(AltarState _alterState)
    {
        if(altar != null)
            altar.AltarState = _alterState;
    }
    public void State()
    {
        // 상태별 행동 나타냄
        switch (altar.AltarState)
        {
            case AltarState.Idle:
                Idle();
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
        FindAllyInBuffRange();
        UpdateBuff();
    }

    private void Destroyed()
    {

    }

    public void UpdateBuff()
    {
        if (altar.TriggerStatusUpdate)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].IsAlterBuff = true;
                characters[i].BuffStatus[(int)EStatus.PhysicalDamage] = altar.BuffDamageLevel;
                characters[i].BuffStatus[(int)EStatus.MagicalDamage] = altar.BuffDamageLevel;
                characters[i].BuffStatus[(int)EStatus.DefensivePower] = altar.BuffDefensivePowerLevel;
                characters[i].BuffStatus[(int)EStatus.Speed] = altar.BuffSpeedLevel;
                characters[i].BuffStatus[(int)EStatus.HpRegenValue] = altar.BuffHpRegenLevel;
                characters[i].UpdateAllStatus();
                UpdateBuffRange();
            }
            altar.TriggerStatusUpdate = false;
        }
    }
    public void RemoveBuff(AllyStatus _ally)
    {
        _ally.IsAlterBuff = false;
        _ally.RemoveBuff();
        //_ally.UpdateTotalAbility();
    }
    private void UpdateBuffRange()
    {
        // 버프 거리 업데이트
        float _diameter = altar.BuffRangeLevel * 1f / 10;
        altar.BuffRangeSprite.transform.localScale = new Vector2(_diameter, _diameter);
    }

    public void FindAllyInBuffRange()
    {
        // 동맹 찾기
        var hits = Physics2D.CircleCastAll(this.transform.position, altar.BuffRangeLevel * 1f, Vector2.zero, 0f, LayerMask.GetMask("Ally"));
        if (hits.Length > 0)
        {
            AddBuffCharacterList(hits);
        }
        RemoveBuffCharacterList();
    }
    public void AddBuffCharacterList(RaycastHit2D[] _raycastHit2Ds)
    {
        // 닿은 캐릭터들을 리스트에 넣는 것
        for (int i = 0; i < _raycastHit2Ds.Length; i++)
        {
            if (!_raycastHit2Ds[i].collider.CompareTag("Altar") && !_raycastHit2Ds[i].collider.GetComponent<AllyStatus>().IsAlterBuff)
            {
                AllyStatus _targetAlly = _raycastHit2Ds[i].collider.GetComponent<AllyStatus>();
                Debug.Log(_targetAlly.ObjectName + " 들어옴");
                characters.Add(_targetAlly);
                UpdateBuff();
            }
        }
    }
    public void RemoveBuffCharacterList()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (altar.GetDistance(characters[i].transform.position) >= altar.BuffRangeLevel * 1f && characters[i].IsAlterBuff)
            {
                characters[i].RemoveBuff();
                characters[i].IsAlterBuff = false;
                characters.Remove(characters[i]);
                Debug.Log(characters[i].ObjectName + "버프 빠짐");
            }
        }
    }
}

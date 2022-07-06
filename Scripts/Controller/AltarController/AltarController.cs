using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-10
 * 작성자 : Inklie
 * 파일명 : AltarController.cs
==============================
*/
public class AltarController : BaseController
{
    private AltarStatus altar = null;
    private TextMesh[] txtMesh = null;
    private Image[] images = null;
    private SpriteRenderer[] spriteRenderers = null;

    [SerializeField]
    private List<AllyStatus> characters = new List<AllyStatus>();
    void Awake()
    {
        altar = this.GetComponent<AltarStatus>();
        txtMesh = this.GetComponentsInChildren<TextMesh>();
        spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        images = this.GetComponentsInChildren<Image>();
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
        // 조건에 맞게 상태변경
        if (IsDestroyed())
        {
            SetState(AltarState.Destroyed);
        }
        else
        {
            if (altar.IsDamaged)
            {
                SetState(AltarState.Damaged);
                UpdateEnemyHp();
            }
            else
            {
                SetState(AltarState.Idle);     
            }
        }

    }
    public void UpdateEnemyHp()
    {
        images[1].fillAmount = altar.CurHp / (float)altar.MaxHp;
    }
    public bool IsDestroyed()
    {
        // 파괴되었는지 확인
        if (altar.CurHp < 0f)
            return true;
        else
            return false;
    }
    public void SetState(AltarState _alterState)
    {
        // 상태 할당
        altar.IsStateChange = true;
        altar.AltarState = _alterState;
    }
    public void State()
    {
        // 상태별 행동 나타냄
        if(altar.IsStateChange)
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
        // 상태 체크
        txtMesh[0].text = altar.AltarState.ToString();
    }
    private void Idle()
    {
        altar.IsStateChange = false;
    }
    private void Damaged()
    {
        altar.IsStateChange = false;
        altar.IsDamaged = false;
    }
    private void Destroyed()
    {
        altar.IsStateChange = false;
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
            characters[i].BuffHpRegenValue = altar.BuffHpRegenLevel * 5;
            characters[i].UpdateAbility();
            UpdateBuffRange();
        }

    }

    private void UpdateBuffRange()
    {
        // 버프 거리 업데이트
        spriteRenderers[2].transform.localScale = new Vector2(altar.BuffRangeLevel * 1f / 10, altar.BuffRangeLevel * 1f / 10);
    }
    public void FindInBuffRangeAlly()
    {
        // 동맹 찾기
        var hits = Physics2D.CircleCastAll(this.transform.position, altar.BuffRangeLevel * 1f, Vector2.zero, 0f, LayerMask.GetMask("Ally"));
        if(hits != null)
            AddBuffCharacterList(hits);
        RemoveBuffCharacterList(hits);
    }
    public void AddBuffCharacterList(RaycastHit2D[] _raycastHit2Ds)
    {
        // 닿은 캐릭터들을 리스트에 넣는 것
        for (int i = 0; i < _raycastHit2Ds.Length; i++)
        {
            if (_raycastHit2Ds[i].collider.gameObject != this.gameObject && !_raycastHit2Ds[i].collider.GetComponent<AllyStatus>().IsAlterBuff)
            {
                characters.Add(_raycastHit2Ds[i].collider.GetComponent<AllyStatus>());
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

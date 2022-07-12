using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-10
 * 작성자 : Inklie
 * 파일명 : UIManager.cs
==============================
*/
public class UIManager : SingletonManager<UIManager>
{
    [Header("EnemySpawner")]
    [SerializeField]
    private EnemySpawner enemySpawner = null;

    [Header("StageManager")]
    [SerializeField]
    private StageManager stageManager = null;

    [Header("MercenaryManager")]
    [SerializeField]
    private MercenaryManager mercenaryManager = null;

    [Header("GraceManager")]
    [SerializeField]
    private GraceManager graceManager = null;

    [Header("Player")]
    [SerializeField]
    private PlayerStatus player = null;
    [SerializeField]
    private SkillController playerSkillController = null;

    [Header("Boss")]
    [SerializeField]
    private BossEnemyStatus bossEnemy = null;

    [Header("Altar")]
    [SerializeField]
    private AltarStatus altar = null;
    [SerializeField]
    private List<CharacterStatus> mercenary = new List<CharacterStatus>();
    [SerializeField]
    private List<EquipmentController> characterList = new List<EquipmentController>();

    [Header("Panels")]
    [SerializeField]
    private InventoryPanelController inventoryPanelController = null;    
    [SerializeField]
    private ProfilePanelController profilePanelController = null;
    [SerializeField]
    private StatusPanelController statusPanelController = null;
    [SerializeField]
    private AltarInfoPanelController altarInfoPanelController = null;
    [SerializeField]
    private SkillInfoPanelController skillInfoPanelController = null;
    [SerializeField]
    private GracePanelController gracePanelController = null;

    [Header("NoticeText")]
    [SerializeField]
    private Text noticeText = null;

    [SerializeField]
    private bool isNotice = false;

    [SerializeField]
    private Coroutine preNotice = null;
    private void Awake()
    {
        characterList.Add(player.GetComponent<EquipmentController>());
    }
    private void Start()
    {
        ChangePlayerUIItemImage();
        UpdatePlayerProfile();
        for (int i = 0; i < mercenary.Count; i++)
        {
            if(mercenary[i] != null)
            {
                ChangeMercenaryUIItemImage(i);
                UpdateMercenaryProfile(i);
                AddMercenaryEquipmentController(mercenary[i]);
            }
        }

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if(mercenaryManager.Mercenarys.Count < 4)
                mercenaryManager.AddNewMercenary();
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Notice("Game Start");
        }
        if (player.IsStatusUpdate)
            UpdatePlayerProfile();
        if(bossEnemy)
        {
            if(bossEnemy.IsDamaged || bossEnemy.IsStatusUpdate)
            {
                UpdateBossInfo();
            }
        }
        for(int i = 0; i < mercenary.Count; i++)
        {
            if (mercenary[i].IsStatusUpdate)
            {
                UpdateMercenaryProfile(i);
                mercenary[i].IsStatusUpdate = false;
            }
        }
    }
    public void SetBossInfo(bool _bool)
    {
        profilePanelController.SetBossProfile(_bool);
    }
    public void Notice(string _notice)
    {
        if (isNotice)
        {
            StopCoroutine(preNotice);
        }
        preNotice = StartCoroutine(NoticeCoroutine(_notice));
    }
    public IEnumerator NoticeCoroutine(string _notice)
    {
        isNotice = true;
        noticeText.gameObject.SetActive(true);
        noticeText.text = _notice;
        yield return new WaitForSeconds(2f);
        isNotice = false;
        for (float i = 1; i >= 0; i -= 0.05f)
        {
            noticeText.color = new Vector4(noticeText.color.r, noticeText.color.g, noticeText.color.b, i);
            yield return new WaitForFixedUpdate();
        }
        noticeText.gameObject.SetActive(false);
        noticeText.color = new Vector4(noticeText.color.r, noticeText.color.g, noticeText.color.b, 1);
    }

    public void UpdateGracePanel()
    {
        Debug.Log("그레이스 업데이트");
        gracePanelController.UpdateSlots(graceManager.CheckIsActive);
    }
    public void AddMercenary(CharacterStatus _mercenary)
    {
        mercenary.Add(_mercenary);
        AddMercenaryEquipmentController(_mercenary);
    }
    public void AddMercenaryEquipmentController(CharacterStatus _mercenary)
    {
        characterList.Add(_mercenary.GetComponent<EquipmentController>());
    }
    public void SetBossEnemy()
    {
        bossEnemy = enemySpawner.CurBoss;
    }
    public int GetMercenaryNum()
    {
        return mercenary.Count;
    }
    public void SelectSlotItem(Item _item)
    {
        // 슬롯에 선택한 아이템 
        inventoryPanelController.SelectSlotItem(_item);
    }
    #region Profile
    public void UpdateBossInfo()
    {
        if (bossEnemy != null)
        {
            profilePanelController.BossUpdate(bossEnemy);
        }
    }
    public void ChangePlayerUIItemImage()
    {
        profilePanelController.ChangePlayerUIItemImage(characterList);
    }
    public void ChangeMercenaryUIItemImage(int _index)
    {
        profilePanelController.ChangeMercenaryUIItemImage(characterList, _index);
    }
    public void UpdatePlayerProfile()
    {
        profilePanelController.UpdatePlayerProfile(player);
    }
    public void UpdateMercenaryProfile(int _index)
    {
        profilePanelController.UpdateMercenaryProfile(mercenary[_index], _index);
    }
    public void SetActiveCharactersProfile(int _index, bool _bool)
    {
        profilePanelController.Profiles[_index].SetActive(_bool);
    }
    #endregion

    #region Button

    #region Inventory Panel
    public void SetActiveItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        inventoryPanelController.SetActiveItemInfo(_bool);
    }
    public void InventorySlotChange(int _index)
    {
        // 인벤토리 슬롯 변경
        inventoryPanelController.InventorySlotChange(_index);
    }
    public void EquipBtn(int _character)
    {
        // 아이템 장착
        inventoryPanelController.Equip(characterList, _character);
    }
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        // 아이템 장착 캐릭터 선택 활성화 여부 
        inventoryPanelController.SetActiveEquipCharacterBox(_bool);
    }
    public void TakeOffSelectItemBtn()
    {
        // 선택한 아이템 해제
        inventoryPanelController.TakeOff(characterList);
    }
    public void UseSelectItemBtn()
    {
        // 아이템 사용
        inventoryPanelController.UseSelectItem(player);
    }
    public void DiscardSelectItemBtn()
    {
        // 아이템 버리기
        inventoryPanelController.DiscardSelectItem();
    }
    public void DiscardSelectAmountItem()
    {
        // 아이템 수량으로 버리기
        inventoryPanelController.DiscardSelectAmountItem();
    }
    public void SetActiveCheckDiscard(bool _bool)
    {
        // 아이템 버리기 확인창 활성화 여부
        inventoryPanelController.SetActiveCheckDiscard(_bool);
    }
    public void SetActiveCheckDiscardAmount(bool _bool)
    {
        // 아이템 수량 버리기 확인창 활성화 여부
        inventoryPanelController.SetActiveCheckDiscardAmount(_bool);
    }
    public void SelectCharacterInEquipmentBtn(bool _isUp)
    {
        // 장비창에서 캐릭터 선택
        inventoryPanelController.SelectCharacterInEquipment(characterList,_isUp);
    }
    public void UpdateEquipmentName()
    {
        // 장비창 캐릭터 이름 업데이트
        inventoryPanelController.UpdateEquipmentName();
    }

    #endregion

    #region Status Panel
    public void UpdateStatus()
    {
        statusPanelController.UpdateStatusText();

    }
    public void SelectCharacterInStatus(bool _isUp)
    {
        statusPanelController.SelectCharacterInStatus(characterList,_isUp);
    }
    public void StatusUp(int _index)
    {
        // 스텟 업
        statusPanelController.StatusUp(_index);
    }
    #endregion

    #region Altar Panel
    public void UpdateAltarInfo()
    {
        altarInfoPanelController.UpdateAltarInfo();
    }
    public void UpgradeAltar(int _index)
    {
        altarInfoPanelController.UpgradeAltarStatus(_index);
    }
    #endregion

    #region SkillPanel
    public void LearnPassiveSkillBtn(int _skillKey)
    {
        if (playerSkillController.GetPassiveSkill(_skillKey) == null)
            skillInfoPanelController.LearnSkill(playerSkillController, _skillKey);
        else
            skillInfoPanelController.LevelUpSkill(playerSkillController, _skillKey);
    }
    #endregion

    #region GracePanel
    public void AquireGrace()
    {
        gracePanelController.AquireGrace(graceManager.AquireGrace);
        UpdateGracePanel();
        ActiveGraceInfo(false);
    }
    public void SelectGrace(int _index)
    {
        gracePanelController.SelectGrace(_index,graceManager.CheckIsActive);
        ActiveGraceInfo(true);
    }
    public void ActiveGraceInfo(bool _bool)
    {
        gracePanelController.ActiveGraceInfo(_bool);
    }
    public void SetGraceSlot(int _egraceType)
    {
        gracePanelController.SetSlotGrace(_egraceType);
        UpdateGracePanel();
    }
    #endregion

    #region MainUI
    public void ActiveUIBtn(int _index)
    {
        // UI 활성화 
        if(_index == 0)
        {
            inventoryPanelController.SetPlayer(player);
            inventoryPanelController.ActiveInventoryPanel(true);
        }
        else if(_index == 1)
        {
            statusPanelController.SetPlayer(player);
            statusPanelController.ActiveStatusPanel(true);
        }
        else if(_index == 2)
        {
            altarInfoPanelController.SetAltar(altar);
            altarInfoPanelController.ActiveAltarInfo(true);
        }
        else if(_index == 3)
        {
            skillInfoPanelController.ActiveSkillPanel(true);
        }
        else if (_index == 4)
        {
            gracePanelController.ActiveGracePanel(true);
            UpdateGracePanel();
        }
    }

    public void DeactiveUIBtn(int _index)
    {
        // UI 비활성화
        if (_index == 0)
        {
            inventoryPanelController.ActiveInventoryPanel(false);
        }
        else if (_index == 1)
        {
            statusPanelController.ActiveStatusPanel(false);
        }
        else if (_index == 2)
        {
            altarInfoPanelController.ActiveAltarInfo(false);
        }
        else if (_index == 3)
        {
            skillInfoPanelController.ActiveSkillPanel(false);
        }
        else if (_index == 4)
        {
            gracePanelController.ActiveGracePanel(false);
        }
    }
    #endregion

    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("EnemySpawner")]
    [SerializeField] private EnemySpawner enemySpawner = null;

    [Header("MercenaryManager")]
    [SerializeField] private MercenaryManager mercenaryManager = null;

    [Header("GraceManager")]
    [SerializeField] private GraceManager graceManager = null;


    [Header("Player")]
    [SerializeField] private PlayerStatus player = null;
    [SerializeField] private SkillController playerSkillController = null;

    [Header("Boss")]
    [SerializeField] private BossEnemyStatus bossEnemy = null;

    [Header("Altar")]
    [SerializeField] private AltarStatus altar = null;
    [SerializeField] private List<CharacterStatus> mercenaries = new List<CharacterStatus>();
    [SerializeField] private List<EquipmentController> characterList = new List<EquipmentController>();
    [SerializeField] private List<SkillController> skillControllerList = new List<SkillController>();

    [Header("Panels")]
    [SerializeField] private SetUpPanelController setUpPanelController = null;
    [SerializeField] private InventoryPanelController inventoryPanelController = null;
    [SerializeField] private EquipmentPanelController equipmentPanelController = null;
    [SerializeField] private ProfilePanelController profilePanelController = null;
    [SerializeField] private StatusPanelController statusPanelController = null;
    [SerializeField] private AltarInfoPanelController altarInfoPanelController = null;
    [SerializeField] private SkillPanelController skillPanelController = null;
    [SerializeField] private GracePanelController gracePanelController = null;
    [SerializeField] private LogPanelController logPanelController = null;
    [SerializeField] private GameObject shopSelectPanel = null;
    [SerializeField] private BuyPanelController buyPanelController = null;
    [SerializeField] private SellPanelController sellPanelController = null;
    [SerializeField] private GameObject forgeSelectPanel = null;
    [SerializeField] private CraftPanelController craftPanelController = null;
    [SerializeField] private DisassemblePanelController disassemblePanelController = null;
    [SerializeField] private BattleSupportPanelController battleSupportPanel = null;
    [SerializeField] private UserControlPanelController userControlPanelController = null;
    [SerializeField] private GameOverPanel gameOverPanel = null;
    [Header("NoticeText")]
    [SerializeField] private TextMeshProUGUI noticeText = null;

    [SerializeField] private bool isNotice = false;
    [SerializeField] private bool isUIOn = false;


    private Coroutine preNotice = null;
    public static UIManager Instance = null;

    public bool IsUIOn { get { return isUIOn; } set { isUIOn = value; } }
    public bool IsLogScrolling { get { return logPanelController.ScrollController.IsScrolling; } }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(13000,100));
        Item item = InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(8002));
        item.skills[0] = DatabaseManager.Instance.SelectSkill(0);
        item.skills[1] = DatabaseManager.Instance.SelectSkill(1);
        item.skills[2] = DatabaseManager.Instance.SelectSkill(2);
        characterList[0].ChangeEquipment(item);
        UpdatePlayerProfile();
        for (int i = 0; i < mercenaries.Count; i++)
        {
            if(mercenaries[i] != null)
            {
                UpdateMercenaryProfile(i);
            }
        }

    }

    private void Update()
    {
            
        if (player.TriggerStatusUpdate)
        {
            UpdatePlayerProfile();
            player.TriggerStatusUpdate = false;
        }
        for(int i = 0; i < mercenaries.Count; i++)
        {
            if (mercenaries[i].TriggerStatusUpdate)
            {
                UpdateMercenaryProfile(i);
                mercenaries[i].TriggerStatusUpdate = false;
            }
        }
        if(bossEnemy)
        {
            if(bossEnemy.IsDamaged || bossEnemy.TriggerStatusUpdate)
            {
                UpdateBossInfo();
            }
        }
        if (altar.IsDied && !altar.TriggerDestroyed)
        {
            StartCoroutine(AltarDestroyed());
        }
    }

    public IEnumerator AltarDestroyed()
    {
        altar.TriggerDestroyed = true;
        gameOverPanel.gameObject.SetActive(true);
        Notice("제단이 파괴되었습니다.", 90, Color.red);
        yield return new WaitForSeconds(1f);
        if(gameOverPanel)
            gameOverPanel.StartGameOver();
    }

    public void SetBossInfo(bool _bool)
    {
        profilePanelController.SetBossProfile(_bool);
    }
    public void SetLog(string _log)
    {
        logPanelController.SetLog(_log);
    }
    public void NoticeMode(string _notice)
    {
        noticeText.gameObject.SetActive(true);
        noticeText.text = _notice;
    }
    public void Notice(string _notice, int _fontSize = 36, Color _fontColor = new Color())
    {
        if (isNotice)
        {
            StopCoroutine(preNotice);
        }
        preNotice = StartCoroutine(NoticeCoroutine(_notice, _fontSize, _fontColor));
    }
    public IEnumerator NoticeCoroutine(string _notice, int _fontSize = 36, Color _fontColor = new Color())
    {
        isNotice = true;
        noticeText.gameObject.SetActive(true);
        noticeText.text = _notice;
        noticeText.fontSize = _fontSize;
        noticeText.color = _fontColor;
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
        gracePanelController.UpdateSlots();
        UpdateGracePoint();
    }
    public void UpdateGracePoint()
    {
        gracePanelController.UpdateGracePoint(player.GracePoint);
    }
    public void AddMercenary(CharacterStatus _mercenary)
    {
        mercenaries.Add(_mercenary);
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
        return mercenaries.Count;
    }
    public void SelectSlotItem(Item _item, InventorySlot _slot = null)
    {
        // 슬롯에 선택한 아이템 
        inventoryPanelController.SelectSlotItem(_item, _slot);
    }
    public void SelectEquipmenttSlotItem(Item _item)
    {
        // 슬롯에 선택한 아이템 
        equipmentPanelController.SelectSlotItem(_item);
    }
    public void SelectSlotSellItem(Item _item)
    {
        sellPanelController.SelectSlotSellItem(_item);
    }
    public void SelectSlotBuyItem(Item _item)
    {
        buyPanelController.SelectSlotBuyItem(_item);
    }

    public void UseSkill(Skill _skill)
    {
        skillControllerList[0].UseSkill(_skill);
    }
    #region Profile
    public void UpdateBossInfo()
    {
        if (bossEnemy != null)
        {
            profilePanelController.BossUpdate(bossEnemy);
        }
    }
    //public void ChangePlayerUIItemImage()
    //{
    //    profilePanelController.ChangePlayerUIItemImage(characterList);
    //}
    //public void ChangeMercenaryUIItemImage(int _index)
    //{
    //    profilePanelController.ChangeMercenaryUIItemImage(characterList, _index);
    //}
    public void UpdatePlayerProfile()
    {
        profilePanelController.UpdatePlayerProfile(player);
    }
    public void UpdateMercenaryProfile(int _index)
    {
        profilePanelController.UpdateMercenaryProfile(mercenaries[_index], _index);
    }

    #endregion

    #region Button
    #region Set-Up Panel

    public void SetSleepMode(int _index)
    {
        setUpPanelController.SetSleepMode(_index);
    }
    public void SetSleeModeImmediately()
    {
        setUpPanelController.SetSleeModeImmediately();
    }
    #endregion

    #region Log Panel
    public void SetLogSizeLevel(bool _bool)
    {
        logPanelController.SetLogSizeLevel(_bool);
    }
    #endregion

    #region Profile Panel
    public void ActiveEquipmentPanel(int _index)
    {
        equipmentPanelController.ActiveEquipmentPanel(true);
        equipmentPanelController.SelectCharacter(_index);
        equipmentPanelController.SetEquipmentSlotImage(_index);
        
    }
    #endregion
    #region Inventory Panel
    public void SetActiveItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        inventoryPanelController.SetActiveItemInfo(_bool);
    }
    public void SetActiveInventoryEquipedItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        inventoryPanelController.SetActiveEquipedItemInfo(_bool);
    }
    public void InventorySlotChange(int _index)
    {
        // 인벤토리 슬롯 변경
        inventoryPanelController.ChangeInventorySlot(_index);
    }
    public void EquipBtn(int _character)
    {
        // 아이템 장착
        inventoryPanelController.EquipInventoryItem(characterList, _character);
        graceManager.ActiveGrace();
        userControlPanelController.SetSkillSlot(player.GetComponent<SkillController>().Skills);
    }
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        // 아이템 장착 캐릭터 선택 활성화 여부 
        inventoryPanelController.SetActiveEquipCharacterBox(_bool);
    }
    public void TakeOffSelectItemBtn()
    {
        // 선택한 아이템 해제
        inventoryPanelController.TakeOffInventoryItem(characterList);
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
    public void SetItemQuickSlot(int _index)
    {
        inventoryPanelController.SetItemQuickSlot(_index);
        battleSupportPanel.SetQuickSlots(_index);
    }
    public void SetActiveQuickSlotSelectButtons()
    {
        
        inventoryPanelController.SetActiveQuickSlotSelectButtons();
    }
    public void SetActiveItemSkillInfo(int _index)
    {
        inventoryPanelController.SetItemSkillExplain(_index);
    }
    public void SetActiveEquipedItemSkillInfo(int _index)
    {
        inventoryPanelController.SetEquipedItemSkillExplain(_index);
    }
    public void SelectCharacterEquipment(bool _isUp)
    {
        inventoryPanelController.SelectCharacterEquipment(_isUp);
    }
    #endregion

    #region Equipment Panel
    public void SelectCharacterInEquipmentBtn(bool _isUp)
    {
        // 장비창에서 캐릭터 선택
        equipmentPanelController.SelectCharacterInEquipment(characterList,_isUp);
    }
    public void UpdateEquipmentName()
    {
        // 장비창 캐릭터 이름 업데이트
        equipmentPanelController.UpdateEquipmentName();
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
    public void LevelUpAltarProperty(int _index)
    {
        altarInfoPanelController.UpgradeAltarStatus(_index);
    }
    #endregion

    #region SkillPanel
    public void SelectCharacterInSkillController(bool _isUp)
    {
        skillPanelController.SelectCharacterInSkillController(skillControllerList, _isUp);
    }
    #endregion

    #region GracePanel
    public void AquireGrace()
    {

        if(player.GracePoint > 0)
        {
            gracePanelController.AquireGrace();
            ActiveGraceInfo(false);
            player.GracePoint--;
            UpdateGracePanel();
        }
        else
        {
            Notice("은총 포인트가 부족합니다.");
        }
    }
    public void SelectGrace(int _index)
    {
        gracePanelController.SelectGrace(_index);
        ActiveGraceInfo(true);
    }
    public void ActiveGraceInfo(bool _bool)
    {
        gracePanelController.ActiveGraceInfo(_bool);
    }
    public void SetGraceSlot(int _egraceType)
    {
        //gracePanelController.SetSlotGrace(_egraceType);
        UpdateGracePanel();
    }
    #endregion

    #region SellingPanel

    public void UpdateBuyInventorySlots(int _index)
    {
        buyPanelController.UpdateBuyingInventorySlotChange(_index);
    }
    public void BuyItem()
    {
        buyPanelController.BuyItem();
    }
    public void UpdateBuyingMoneyText()
    {
        buyPanelController.UpdateBuyMoneyText();
    }
    public void SetActiveBuyingItemInfo(bool _bool)
    {
        buyPanelController.SetActiveShopItemInfo(_bool);
    }
    public void SetActiveEquipedItemInfo(bool _bool)
    {
        buyPanelController.SetActiveEquipedItemInfo(_bool);
    }

    #endregion

    #region BuyingPanel
    public void ChangeSellInventorySlots(int _index)
    {
        sellPanelController.UpdateSellInventorySlot(_index);
    }
    public void RegisterSellItem()
    {
        sellPanelController.RegisterItem();
    }
    public void RegisterSellAmountItem()
    {
        sellPanelController.RegisterAmountItem();
    }
    public void SetActiveSellAmountItem(bool _bool)
    {
        sellPanelController.SetActiveSellItemAmount(_bool);
    }
    public void SetActiveRegisterEasyPanel(bool _bool)
    {
        sellPanelController.SetActiveRegisterEasyPanel(_bool);
    }
    public void SellItem()
    {
        sellPanelController.SellItem();
    }
    public void CancelRegisteredItem(Item _item)
    {
        sellPanelController.CancelRegisteredItem(_item);
    }
    public void RegisterItemEasy()
    {
        sellPanelController.RegisterItemEasy();
        SetActiveRegisterEasyPanel(false);
    }
    public void SetActiveSellItemInfo(bool _bool)
    {
        sellPanelController.SetActiveSellItemInfo(_bool);
    }
    #endregion

    #region CraftPanel
    public void SelectCraftRecipe(CraftRecipe _craftRecipe)
    {
        craftPanelController.SelectCraftRecipe = _craftRecipe;
        craftPanelController.IsSelected = true;
    }
    public void SelectNecessaryItemInfo(Item _item)
    {
        craftPanelController.SelectNeedItemInfo = _item;
        craftPanelController.IsNeedItemInfoSelected = true;
    }
    public void SelectRegisterNecessaryItem(Item _item, int _index)
    {
        craftPanelController.SelectRegisterItem = _item;
        craftPanelController.IsRegisterNecessaryItemSelect = true;
        craftPanelController.SelectNecessaryItemIndex = _index;
    }
    public void SelectRegisterNecessaryItemPanel(Item _item)
    {
        SetActiveRegisterNecessaryItemPanel(true);
        craftPanelController.SelectRegisterInventoryItem = _item;
    }
    public void SetActiveRegisterNecessaryItemPanel(bool _bool)
    {
        craftPanelController.SetActiveRegisterNecessaryItemPanel(_bool);
    }
    public void SetActivenecessaryIteminfoPanel(bool _bool)
    {
        craftPanelController.SetActiveNecessaryItemInfoPanel(_bool);
    }
    public void SetActiveNecessaryItemInventoryPanel(bool _bool)
    {
        craftPanelController.SetActiveCraftNecessaryItemInventoryPanel(_bool);
    }
    public void Register()
    {
        craftPanelController.Register();
    }
    public void Craft()
    {
        craftPanelController.Craft();
    }
    #endregion
    
    #region DisassemblePanel
    public void SelectDisassembleItem(Item _item)
    {
        disassemblePanelController.SelectDisassembleItem = _item;
        disassemblePanelController.IsDisassembleItemSelect = true;
    }
    public void SetActiveDisassembleItemInfo()
    {
        disassemblePanelController.SetActiveDisassembleItemInfo(true);
    }
    public void UpdateDisassembleInventory(int _index)
    {
        disassemblePanelController.UpdateDisassembleInventorySlot(_index);
    }
    public void Disassemble()
    {
        disassemblePanelController.DisassembleItem();
    }
    public void UpdateDisassembleCheckBox()
    {
        disassemblePanelController.UpdateDisassembleCheckBox();
    }
    public void RegisterDisassembleItem()
    {
        disassemblePanelController.RegisterItem();
    }
    public void SetActiveDisassembleItemCheckBox(bool _bool)
    {
        disassemblePanelController.SetActiveDisassembleItemCheckBox(_bool);
    }
    public void SelectDisassembleRegisteredSlot(Item _item)
    {
        disassemblePanelController.CancelRegisteredItem(_item);
    }
    #endregion

    #region BattleSupportPanel
    public void UseQuickSlotItem(Item _item, int _slotIndex)
    {
        battleSupportPanel.UseQuickSlotItem(_item, _slotIndex);
    }
    public void SetPlayerAutoPlay()
    {
        battleSupportPanel.SetAutoPlay();
    }
    #endregion

    #region UserControlPanel
    #endregion

    #region MainUI
    public void ActiveUIBtn(int _index)
    {
        // UI 활성화 

        isUIOn = true;
        if (_index == 0)
        {
            inventoryPanelController.ActiveInventoryPanel(true);
            inventoryPanelController.ChangeInventorySlot(0);

        }
        else if (_index == 2)
        {
            statusPanelController.SetPlayer(player);
            statusPanelController.ActiveStatusPanel(true);
            statusPanelController.UpdateStatusText();
        }
        else if (_index == 3)
        {
            altarInfoPanelController.SetActiveAltarInfo(true);
            altarInfoPanelController.UpdateAltarInfo();
        }
        else if (_index == 4)
        {
            skillPanelController.ActiveSkillPanel(true);
            skillPanelController.SelectCharacter(skillControllerList[0], player);
            skillPanelController.SetSkillInfo();
        }
        else if (_index == 5)
        {
            gracePanelController.ActiveGracePanel(true);
            UpdateGracePanel();
        }
        else if (_index == 6)
        {
            shopSelectPanel.SetActive(true);
        }
        else if (_index == 7)
        {
            buyPanelController.SetActivebuyPanel(true);

        }
        else if (_index == 8)
        {
            sellPanelController.SetActiveSellPanel(true);
        }
        else if (_index == 9)
        {
            forgeSelectPanel.SetActive(true);
        }
        else if (_index == 10)
        {
            craftPanelController.SetActiveCraftPanel(true);
        }
        else if (_index == 11)
        {
            disassemblePanelController.SetActiveDisassemblePanel(true);
            disassemblePanelController.UpdateDisassembleInventorySlot(0);
        }
        else if(_index == 12)
        {
            setUpPanelController.SetActiveSetUpPanel(true);
        }
    }

    public void DeactiveUIBtn(int _index)
    {
        // UI 비활성화
        isUIOn = false;
        if (_index == 0)
        {
            inventoryPanelController.ActiveInventoryPanel(false);
        }
        if (_index == 1)
        {
            equipmentPanelController.ActiveEquipmentPanel(false);
        }
        else if (_index == 2)
        {
            statusPanelController.ActiveStatusPanel(false);
        }
        else if (_index == 3)
        {
            altarInfoPanelController.SetActiveAltarInfo(false);
        }
        else if (_index == 4)
        {
            skillPanelController.ActiveSkillPanel(false);
        }
        else if (_index == 5)
        {
            gracePanelController.ActiveGracePanel(false);
        }
        else if (_index == 6)
        {
            shopSelectPanel.SetActive(false);
        }
        else if (_index == 7)
        {
            buyPanelController.SetActivebuyPanel(false);
        }
        else if (_index == 8)
        {

            sellPanelController.SetActiveSellPanel(false);
            sellPanelController.CancelAllRegisteredItem();
            
        }
        else if (_index == 9)
        {
            forgeSelectPanel.SetActive(false);
        }
        else if (_index == 10)
        {
            craftPanelController.SetActiveCraftPanel(false);
        }
        else if (_index == 11)
        {

            disassemblePanelController.SetActiveDisassemblePanel(false);
            disassemblePanelController.CancelAllRegisteredItem();
        }
        else if(_index == 12)
        {
            setUpPanelController.SetActiveSetUpPanel(false);
        }
    }
    #endregion

    #endregion
    #region Slider

    #region Set-Up Panel
    public void SetSoundVolume(int _index)
    {
        setUpPanelController.SetSoundVolume(_index);
    }
    public void SetPortionUseCondition(int _index)
    {
        setUpPanelController.SetPortionUseCondition(_index);
    }
    #endregion

    #endregion

    #region Toggle
    #region Set-Up Panel
    public void SetActiveConversation(bool _bool)
    {
        profilePanelController.ChooseTriggerConversationActive(_bool);
    }
    public void SetCheckControlOnAutoPlay(bool _bool)
    {
        setUpPanelController.SetCheckControlOnAutoPlay(_bool);
    }
    #endregion
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
==============================
 * 최종수정일 : 2022-06-09
 * 작성자 : Inklie
 * 파일명 : InventoryPanelController.cs
==============================
*/
public class InventoryPanelController : MonoBehaviour
{
    [Header("ItemInfo")]
    [SerializeField]
    private GameObject itemInfo = null;

    [Header("CheckDiscard")]
    [SerializeField]
    private GameObject checkDiscard = null;
    [SerializeField]
    private GameObject checkAmount = null;
    [SerializeField]
    private InputField amount = null;

    [Header("UIImages")]
    [SerializeField]
    private GameObject UIImages = null;

    [Header("Buttons")]
    [SerializeField]
    private Button[] inventoryButtons = null;

    [Header("Equip")]
    [SerializeField]
    private Button[] equipCharactersBtn = null;
    [SerializeField]
    private TextMeshProUGUI equipmentNameText = null;

    [Header("MoenyText")]
    [SerializeField]
    private TextMeshProUGUI moneyText = null;

    [Header("Default")]
    [SerializeField]
    private Sprite UIMask = null;

    private PlayerStatus playerStatus = null;
    private bool isItemSelect = false;
    private bool isQuickSlotsOpen = false;
    [SerializeField]
    private int selectCharNum = 0;
    private int selectInventoryIndex = 0;
    [SerializeField]
    private Item selectItem = null;
    [SerializeField]
    private EquipmentController selectCharacterEqipment = null;
    [SerializeField]
    private AllyStatus selectCharStatus = null;
    [SerializeField]
    private InventorySlot[] inventorySlots = null;
    [SerializeField]
    private InventorySlot selectInventorySlot = null;

    [SerializeField]
    private EquipmentSlot[] equipmentSlots = null;
    private TextMeshProUGUI[] iteminfoText = null;
    [SerializeField]
    private GameObject quickSlotSelectButtons = null;
    
    private void Awake()
    {
        iteminfoText = itemInfo.GetComponentsInChildren<TextMeshProUGUI>();
        inventorySlots = this.GetComponentsInChildren<InventorySlot>();
        equipmentSlots = this.GetComponentsInChildren<EquipmentSlot>();
        
    }
    private void Update()
    {
        if (isItemSelect)
            UpdateItemInfo();
    }
    public void SetPlayer(PlayerStatus _player)
    {
        playerStatus = _player;
        selectCharacterEqipment = _player.GetComponent<EquipmentController>();
        selectCharStatus = playerStatus;
    }
    public void InventoryReset()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SlotReset();
        }
    }
    public void MoneyUpdate()
    {
        moneyText.text = playerStatus.Money.ToString("N0"); 
    }
    public void InventorySlotChange(int _index)
    {
        // 인벤토리 슬롯 바꾸기 
        InventoryReset();
        SetActiveItemInfo(false);
        MoneyUpdate();
        SetActiveFalseQuickSlotSelectButtons();
        if (_index == 0)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyWeaponItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyWeaponItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyWeaponItems[i];
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        if (_index == 1)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyEquipmentItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyEquipmentItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyEquipmentItems[i];
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        if (_index == 2)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyConsumableItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyConsumableItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyConsumableItems[i];
                inventorySlots[i].SlotSetting();
            }
        }
        if (_index == 3)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyMiscellaneousItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyMiscellaneousItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyMiscellaneousItems[i];
                inventorySlots[i].SlotSetting();

            }
        }
        if (_index == 4)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyDecorationItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyDecorationItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyDecorationItems[i];
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        selectInventoryIndex = _index;
    }
    public void SetActiveItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        itemInfo.SetActive(_bool);
        if(!_bool)
            SetActiveEquipCharacterBox(false);
    }
    public void SetActiveCheckDiscard(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        checkDiscard.SetActive(_bool);
    }
    public void SetActiveCheckDiscardAmount(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        checkAmount.SetActive(_bool);
    }
    public void UpdateItemInfo()
    {
        // 아이템 정보창 업데이트
        isItemSelect = false;
        InventoryButtonReset();
        SetActiveItemInfo(true);
        inventoryButtons[3].gameObject.SetActive(true);
        if (selectItem.itemType == (int)ItemType.Consumables)
        {
            inventoryButtons[2].gameObject.SetActive(true);
        }
        else
        {
            if (selectItem.isEquip)
            {
                inventoryButtons[1].gameObject.SetActive(true);
            }
            else
            {
                inventoryButtons[0].gameObject.SetActive(true);
            }
        }
        iteminfoText[0].text = selectItem.itemName;
        iteminfoText[1].text = KeyToItemType(selectItem.itemKey);
        switch (selectItem.itemKey / 1000)
        {
            case 0:
                iteminfoText[2].text = "This is Hair";
                break;
            case 1:
                iteminfoText[2].text = "This is FaceHair";
                break;
            case 2:
                iteminfoText[2].text = "방어력: " + selectItem.defensivePower;
                break;
            case 3:
                iteminfoText[2].text = "방어력: " + selectItem.defensivePower;
                break;
            case 4:
                iteminfoText[2].text = "방어력: " + selectItem.defensivePower;
                break;
            case 5:
                iteminfoText[2].text = "방어력: " + selectItem.defensivePower;
                break;
            case 6:
                iteminfoText[2].text = "방어력: " + selectItem.defensivePower;
                break;
            case 7:
                iteminfoText[2].text =
                    "물리 공격력: " + selectItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectItem).weaponType;
                break;
            case 8:
                iteminfoText[2].text =
                    "물리 공격력: " + selectItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectItem).weaponType + "\n" +
                    "방어력: " + selectItem.defensivePower;
                break;
            case 9:
                iteminfoText[2].text =
                    "물리 공격력: " + selectItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectItem).weaponType;
                break;
            case 10:
                iteminfoText[2].text =
                    "물리 공격력: " + selectItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectItem).weaponType;
                break;
            case 11:
                iteminfoText[2].text =
                    "회복량 : " + selectItem.value + "\n";
                break;
            case 12:
                iteminfoText[2].text = "이것은 퀘스트 아이템";
                break;
        }
    }
    public void InventoryButtonReset()
    {
        // 인벤토리 버튼 리셋
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            inventoryButtons[i].gameObject.SetActive(false);
        }
    }
    public string KeyToItemType(int _key)
    {
        // 키를 아이템 타입으로 변경
        string _itemtype = null;
        switch (_key / 1000)
        {
            case 0:
                _itemtype = "Hair";
                break;
            case 1:
                _itemtype = "FaceHair";
                break;
            case 2:
                _itemtype = "Cloth";
                break;
            case 3:
                _itemtype = "Pant";
                break;
            case 4:
                _itemtype = "Helmet";
                break;
            case 5:
                _itemtype = "Armor";
                break;
            case 6:
                _itemtype = "Back";
                break;
            case 7:
                _itemtype = "Sword";
                break;
            case 8:
                _itemtype = "Shield";
                break;
            case 9:
                _itemtype = "Bow";
                break;
            case 10:
                _itemtype = "Wand";
                break;
        }
        return _itemtype;
    }
    public void EquipInventoryItem(List<EquipmentController> _characterList,int _character)
    {
        if (_characterList[_character].GetComponent<CharacterStatus>().CurLevel >= selectItem.equipLevel)
        {
            // 장착하기 버튼
            if (_characterList[_character].CheckEquipItems[selectItem.itemType])
            {
                _characterList[_character].TakeOffEquipment(_characterList[_character].EquipItems[selectItem.itemType]);
            }
            selectItem.equipCharNum = _character;
            SelectCharacter(_characterList);
            SetActiveEquipCharacterBox(false);
            _characterList[_character].ChangeEquipment(selectItem);
            InventorySlotChange(selectInventoryIndex);
            UpdateEquipmentName();
            ChangeAllEquipmentImage();
            SetActiveItemInfo(false);
            selectItem = null;
        }
        else
            Debug.Log("레벨이 부족합니다.");
    }

    public void TakeOffInventoryItem(List<EquipmentController> _characterList)
    {
        // 장착해제
        SelectCharacter(_characterList);
        if (selectItem.isEquip)
        {
            _characterList[selectItem.equipCharNum].TakeOffEquipment(selectItem);
        }
        else
            Debug.Log("착용중이 아님");
        SetActiveItemInfo(false);
        ChangeAllEquipmentImage();
        InventorySlotChange(selectInventoryIndex);
        UpdateEquipmentName();
        selectItem = null;
    }
    public void SelectCharacter(List<EquipmentController> _characterList)
    {
        selectCharNum = selectItem.equipCharNum;
        selectCharacterEqipment = _characterList[selectCharNum];
        selectCharStatus = _characterList[selectCharNum].GetComponent<AllyStatus>();
    }
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        // 장착 캐릭터 선택하기 버튼 활성화
        equipCharactersBtn[0].gameObject.SetActive(_bool);
        for (int i = 0; i < UIManager.Instance.GetMercenaryNum(); i++)
            equipCharactersBtn[i + 1].gameObject.SetActive(_bool);
    }

    public void SelectSlotItem(Item _item, InventorySlot _slot = null)
    {
        // 슬롯에 선택한 아이템 
        selectItem = _item;
        isItemSelect = true;
        selectInventorySlot = _slot;
    }
    public void ChangeAllEquipmentImage()
    { 
        // 장비창 이미지 바꾸기
        for (int i = 0; i < selectCharacterEqipment.EquipItems.Length; i++)
        {
            if (selectCharacterEqipment.CheckEquipItems[i])
            {
                SetEquipmentSlotImage(i);
            }
            else
            {
                InitEquipmentSlotImage(i);
            }
        }
    }
    public void SetEquipmentSlotImage(int _index)
    {
        equipmentSlots[_index].CurItem = selectCharacterEqipment.EquipItems[_index];
        equipmentSlots[_index].ItemImages[1].sprite = selectCharacterEqipment.EquipItems[_index].singleSprite;
        equipmentSlots[_index].SlotSetting(selectCharacterEqipment.EquipItems[_index]);
    }
    public void InitEquipmentSlotImage(int _index)
    {
        // 장비가 없을 시 장비 슬롯 비우기
        equipmentSlots[_index].CurItem = null;
        equipmentSlots[_index].ItemImages[1].sprite = UIMask;
        equipmentSlots[_index].InitImageSize();
    }
    public void SelectCharacterInEquipment(List<EquipmentController> _charaterList, bool _isUp)
    {
        // 장비창에서 캐릭터 선택
        InitEquipment();
        if (_isUp)
        {
            if (selectCharNum >= _charaterList.Count - 1)
                selectCharNum = 0;
            else
                selectCharNum++;
        }
        else
        {
            if (selectCharNum <= 0)
                selectCharNum = _charaterList.Count - 1;
            else
                selectCharNum--;

        }
        selectCharacterEqipment = _charaterList[selectCharNum];
        selectCharStatus = _charaterList[selectCharNum].GetComponent<AllyStatus>();
        UpdateEquipmentName();
        ChangeAllEquipmentImage();
    }
    public void UpdateEquipmentName()
    {
        // 장비창 캐릭터 이름 변경
        equipmentNameText.text = selectCharStatus.ObjectName.ToString();
    }
    public void InitEquipment()
    {
        // 장비창 리셋
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].CurItem = null;
            equipmentSlots[i].ItemImages[1].sprite = UIMask;
        }
    }
    public void DiscardSelectItem()
    {
        // 아이템 버리기
        if (selectItem.itemType > 8)
        {
            SetActiveCheckDiscardAmount(true);
            SetActiveCheckDiscard(false);
        }
        else
        {
            InventoryManager.Instance.DiscardItem(selectItem);
            InventorySlotChange(selectInventoryIndex);
        }
    }
    public void DiscardSelectAmountItem()
    {
        if (selectItem.count >= int.Parse(amount.text))
        {
            InventoryManager.Instance.DiscardItem(selectItem, int.Parse(amount.text));
            InventorySlotChange(selectInventoryIndex);
            SetActiveCheckDiscardAmount(false);
            amount.text = null;
        }
        else
            Debug.Log("버리려는 값을 초과함");
    }
    public void UseSelectItem(PlayerStatus _player)
    {
        // 아이템 사용
        if (!selectItem.isCoolTime)
        {
            InventoryManager.Instance.UseItem(_player, selectItem);
            SetActiveItemInfo(false);
            InventorySlotChange(2);
            selectItem.isCoolTime = true;
        }
        else
            Debug.Log("쿨타임 중");
    }
    public void SetItemQuickSlot(int _index)
    {
        playerStatus.QuickSlotItems[_index] = selectItem;
    }
    public void SetActiveQuickSlotSelectButtons()
    {
        isQuickSlotsOpen = !isQuickSlotsOpen;
        quickSlotSelectButtons.SetActive(isQuickSlotsOpen);
    }
    public void SetActiveFalseQuickSlotSelectButtons()
    {
        isQuickSlotsOpen = false;
        quickSlotSelectButtons.SetActive(false);
    }
    public void ActiveInventoryPanel(bool _bool)
    {
        // UI 활성화 
        UIImages.SetActive(_bool);
        if(_bool)
            ChangeAllEquipmentImage();
        else
        {
            SetActiveItemInfo(_bool);
            SetActiveFalseQuickSlotSelectButtons();
        }
    }
}

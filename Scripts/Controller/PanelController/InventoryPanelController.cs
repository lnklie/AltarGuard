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
    [SerializeField] private GameObject itemInfo = null;

    [Header("CheckDiscard")]
    [SerializeField] private GameObject checkDiscard = null;
    [SerializeField] private GameObject checkAmount = null;
    [SerializeField] private TMP_InputField amount = null;

    [Header("UIImages")]
    [SerializeField] private GameObject UIImages = null;

    [Header("Buttons")]
    [SerializeField] private Button[] inventoryButtons = null;

    [Header("Equip")]
    [SerializeField] private Button[] equipCharactersBtn = null;
    [SerializeField] private TextMeshProUGUI equipmentNameText = null;

    [Header("MoenyText")]
    [SerializeField] private TextMeshProUGUI moneyText = null;

    [Header("Default")]
    [SerializeField] private Sprite UIMask = null;

    private PlayerStatus playerStatus = null;
    private bool isItemSelect = false;
    private bool isQuickSlotsOpen = false;
    [SerializeField] private TextMeshProUGUI iteminfoNameText = null;
    [SerializeField] private TextMeshProUGUI iteminfoTypeText = null;
    [SerializeField] private TextMeshProUGUI iteminfoExplainText = null;
    [SerializeField] private TextMeshProUGUI iteminfoRankText = null;

    private int selectInventoryIndex = 0;
    [SerializeField] private int selectCharNum = 0;
    [SerializeField] private Item selectItem = null;
    [SerializeField] private EquipmentController selectCharacterEqipment = null;
    [SerializeField] private AllyStatus selectCharStatus = null;
    [SerializeField] private List<InventorySlot> inventorySlotList = new List<InventorySlot>();
    [SerializeField] private InventorySlot selectInventorySlot = null;
    [SerializeField] private EquipmentSlot[] equipmentSlots = null;
    [SerializeField] private GameObject quickSlotSelectButtons = null;

    
    private void Awake()
    {
        equipmentSlots = this.GetComponentsInChildren<EquipmentSlot>();
        inventorySlotList.AddRange(this.GetComponentsInChildren<InventorySlot>());
        
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
    public void ResetInventory()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            inventorySlotList[i].SlotReset();
        }
    }
    public void UpdateMoney()
    {
        moneyText.text = ConvertMoney(playerStatus.Money);
    }

    public string ConvertMoney(int _money)
    {
        string _moneyText = null;
        int _tempMoney = _money;
        int _mark = 0;
        //switch()
        
        while(_mark < 2)
        {
            if(_tempMoney >= Mathf.Pow(10, 12))
            {
                _moneyText += ((int)(_tempMoney / Mathf.Pow(10, 12))).ToString() + "조 ";
                _tempMoney = (int)(_tempMoney % Mathf.Pow(10, 12));
            }
            else if (_tempMoney >= 100000000)
            {
                _moneyText += ((_tempMoney / 100000000)).ToString() + "억 ";
                _tempMoney = (_tempMoney % 100000000);
            }
            else if (_tempMoney >= 10000)
            {

                _moneyText += ((_tempMoney / 10000)).ToString() + "만 ";
                _tempMoney = (_tempMoney % 10000);

            }
            else if (_tempMoney >= 1)
            {

                _moneyText += _tempMoney.ToString();
                _tempMoney = 0;
            }
            _mark++;
        }
        return _moneyText;
    }
    public void SortInventoryByKeyAndInventoryIndex(List<Item> _inventory)
    {
        // 리스트 정렬
        _inventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            else return a.inventoryIndex.CompareTo(b.inventoryIndex);
        });
    }
    public void ChangeInventorySlot(int _index)
    {
        // 인벤토리 슬롯 바꾸기 
        ResetInventory();
        SetActiveItemInfo(false);
        UpdateMoney();
        SetActiveFalseQuickSlotSelectButtons();
        List<Item> _inventory = new List<Item>();
        if (_index == 0)
        {
            _inventory.AddRange(InventoryManager.Instance.InventroyWeaponItems);
            SortInventoryByKeyAndInventoryIndex(_inventory);


            for (int i = 0; i < _inventory.Count; i++)
            {
                inventorySlotList[i].CurItem = _inventory[i];
                inventorySlotList[i].SlotSetting();
                inventorySlotList[i].EnableItemCount(false);
            }
        }
        if (_index == 1)
        {
            _inventory.AddRange(InventoryManager.Instance.InventroyEquipmentItems);
            SortInventoryByKeyAndInventoryIndex(_inventory);

            for (int i = 0; i < _inventory.Count; i++)
            {
                inventorySlotList[i].CurItem = _inventory[i];
                inventorySlotList[i].SlotSetting();
                inventorySlotList[i].EnableItemCount(false);
            }
        }
        if (_index == 2)
        {
            _inventory.AddRange(InventoryManager.Instance.InventroyConsumableItems);
            SortInventoryByKeyAndInventoryIndex(_inventory);
            for (int i = 0; i < _inventory.Count; i++)
            {
                inventorySlotList[i].CurItem = _inventory[i];
                inventorySlotList[i].SlotSetting();
            }
        }
        if (_index == 3)
        {
            _inventory.AddRange(InventoryManager.Instance.InventroyMiscellaneousItems);
            SortInventoryByKeyAndInventoryIndex(_inventory);
            for (int i = 0; i < _inventory.Count; i++)
            {
                inventorySlotList[i].CurItem = _inventory[i];
                inventorySlotList[i].SlotSetting();

            }
        }
        if (_index == 4)
        {
            _inventory.AddRange(InventoryManager.Instance.InventroyDecorationItems);
            SortInventoryByKeyAndInventoryIndex(_inventory);
            for (int i = 0; i < _inventory.Count; i++)
            {
                inventorySlotList[i].CurItem = _inventory[i];
                inventorySlotList[i].SlotSetting();
                inventorySlotList[i].EnableItemCount(false);
            }
        }
        //SortItemKeyInventorySlots(inventorySlotList);
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
        checkDiscard.SetActive(_bool);
    }
    public void SetActiveCheckDiscardAmount(bool _bool)
    {
        checkAmount.SetActive(_bool);
        amount.text = "";
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
        iteminfoNameText.text = selectItem.itemKorName;
        iteminfoTypeText.text = KeyToItemType(selectItem.itemKey);
        iteminfoRankText.text = IntRankToStringRank(selectItem.itemRank);
        switch (selectItem.itemKey / 1000)
        {
            case 0:
                iteminfoExplainText.text = "This is Hair";
                break;
            case 1:
                iteminfoExplainText.text = "This is FaceHair";
                break;
            case 2:
                iteminfoExplainText.text = "방어력: " + selectItem.defensivePower;
                break;
            case 3:
                iteminfoExplainText.text = "방어력: " + selectItem.defensivePower;
                break;
            case 4:
                iteminfoExplainText.text = "방어력: " + selectItem.defensivePower;
                break;
            case 5:
                iteminfoExplainText.text = "방어력: " + selectItem.defensivePower;
                break;
            case 6:
                iteminfoExplainText.text = "방어력: " + selectItem.defensivePower;
                break;
            case 7:
                iteminfoExplainText.text = "방어력: " + selectItem.defensivePower;
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                iteminfoExplainText.text =
                    "물리 공격력: " + selectItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectItem).weaponType;
                break;
            case 13:
                iteminfoExplainText.text =
                    "회복량 : " + selectItem.value + "\n";
                break;
            case 14:
                iteminfoExplainText.text = "이것은 퀘스트 아이템";
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

    public string IntRankToStringRank(int _Rank)
    {
        string _rank = null;
        switch((EItemRank)_Rank)
        {
            case EItemRank.Common:
                _rank =  "Common";
                break;
            case EItemRank.UnCommon:
                _rank = "UnCommon";
                break;
            case EItemRank.Rare:
                _rank = "Rare";
                break;
            case EItemRank.Unique:
                _rank = "Unique";
                break;
        }
        return _rank;
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
        if(!selectItem.isCoolTime)
        {
            selectItem.isCoolTime = true;
            if (selectItem.itemType < 7)
                InventoryManager.Instance.IsEquipmentCoolTime = true;
            else
                InventoryManager.Instance.IsWeaponCoolTime = true;

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
                ChangeInventorySlot(selectInventoryIndex);
                UpdateEquipmentName();
                ChangeAllEquipmentImage();
                SetActiveItemInfo(false);
                selectItem = null;
            }
            else
                Debug.Log("레벨이 부족합니다.");
        }
        else
        {
            Debug.Log("쿨타임 중");
        }
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
        ChangeInventorySlot(selectInventoryIndex);
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
            ChangeInventorySlot(selectInventoryIndex);
        }
    }
    public void DiscardSelectAmountItem()
    {
        if(amount.text != "")
        {
            if (selectItem.count >= int.Parse(amount.text))
            {
                InventoryManager.Instance.DiscardItem(selectItem, int.Parse(amount.text));
                ChangeInventorySlot(selectInventoryIndex);
                SetActiveCheckDiscardAmount(false);
                amount.text = null;
            }
            else
                Debug.Log("버리려는 값을 초과함");
        }
        else
        {
            UIManager.Instance.Notice("올바른 값을 입력해주세요.");
        }
    }
    public void UseSelectItem(PlayerStatus _player)
    {
        // 아이템 사용
        if (!selectItem.isCoolTime)
        {
            InventoryManager.Instance.IsConsumaableCoolTime = true;
            InventoryManager.Instance.UseItem(_player, selectItem);
            SetActiveItemInfo(false);
            ChangeInventorySlot(2);
            selectItem.isCoolTime = true;
        }
        else
            Debug.Log("쿨타임 중");
    }
    public void SetItemQuickSlot(int _index)
    {
        playerStatus.QuickSlotItems[_index] = selectItem;
        SetActiveFalseQuickSlotSelectButtons();
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

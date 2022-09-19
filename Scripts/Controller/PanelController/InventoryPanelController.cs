using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
==============================
 * ���������� : 2022-06-09
 * �ۼ��� : Inklie
 * ���ϸ� : InventoryPanelController.cs
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
        // �κ��丮 ���� ����
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
                _moneyText += ((int)(_tempMoney / Mathf.Pow(10, 12))).ToString() + "�� ";
                _tempMoney = (int)(_tempMoney % Mathf.Pow(10, 12));
            }
            else if (_tempMoney >= 100000000)
            {
                _moneyText += ((_tempMoney / 100000000)).ToString() + "�� ";
                _tempMoney = (_tempMoney % 100000000);
            }
            else if (_tempMoney >= 10000)
            {

                _moneyText += ((_tempMoney / 10000)).ToString() + "�� ";
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
        // ����Ʈ ����
        _inventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            else return a.inventoryIndex.CompareTo(b.inventoryIndex);
        });
    }
    public void ChangeInventorySlot(int _index)
    {
        // �κ��丮 ���� �ٲٱ� 
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
        // ������ ����â Ȱ��ȭ ����
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
        // ������ ����â ������Ʈ
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
                iteminfoExplainText.text = "����: " + selectItem.defensivePower;
                break;
            case 3:
                iteminfoExplainText.text = "����: " + selectItem.defensivePower;
                break;
            case 4:
                iteminfoExplainText.text = "����: " + selectItem.defensivePower;
                break;
            case 5:
                iteminfoExplainText.text = "����: " + selectItem.defensivePower;
                break;
            case 6:
                iteminfoExplainText.text = "����: " + selectItem.defensivePower;
                break;
            case 7:
                iteminfoExplainText.text = "����: " + selectItem.defensivePower;
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                iteminfoExplainText.text =
                    "���� ���ݷ�: " + selectItem.physicalDamage + "\n" +
                    "���� ���ݷ�: " + selectItem.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)selectItem).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)selectItem).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)selectItem).weaponType;
                break;
            case 13:
                iteminfoExplainText.text =
                    "ȸ���� : " + selectItem.value + "\n";
                break;
            case 14:
                iteminfoExplainText.text = "�̰��� ����Ʈ ������";
                break;
        }
    }
    public void InventoryButtonReset()
    {
        // �κ��丮 ��ư ����
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
        // Ű�� ������ Ÿ������ ����
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
                // �����ϱ� ��ư
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
                Debug.Log("������ �����մϴ�.");
        }
        else
        {
            Debug.Log("��Ÿ�� ��");
        }
    }

    public void TakeOffInventoryItem(List<EquipmentController> _characterList)
    {
        // ��������
        SelectCharacter(_characterList);
        if (selectItem.isEquip)
        {
            _characterList[selectItem.equipCharNum].TakeOffEquipment(selectItem);
        }
        else
            Debug.Log("�������� �ƴ�");
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
        // ���� ĳ���� �����ϱ� ��ư Ȱ��ȭ
        equipCharactersBtn[0].gameObject.SetActive(_bool);
        for (int i = 0; i < UIManager.Instance.GetMercenaryNum(); i++)
            equipCharactersBtn[i + 1].gameObject.SetActive(_bool);
    }

    public void SelectSlotItem(Item _item, InventorySlot _slot = null)
    {
        // ���Կ� ������ ������ 
        selectItem = _item;
        isItemSelect = true;
        selectInventorySlot = _slot;
    }
    public void ChangeAllEquipmentImage()
    { 
        // ���â �̹��� �ٲٱ�
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
        // ��� ���� �� ��� ���� ����
        equipmentSlots[_index].CurItem = null;
        equipmentSlots[_index].ItemImages[1].sprite = UIMask;
        equipmentSlots[_index].InitImageSize();
    }
    public void SelectCharacterInEquipment(List<EquipmentController> _charaterList, bool _isUp)
    {
        // ���â���� ĳ���� ����
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
        // ���â ĳ���� �̸� ����
        equipmentNameText.text = selectCharStatus.ObjectName.ToString();
    }
    public void InitEquipment()
    {
        // ���â ����
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].CurItem = null;
            equipmentSlots[i].ItemImages[1].sprite = UIMask;
        }
    }
    public void DiscardSelectItem()
    {
        // ������ ������
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
                Debug.Log("�������� ���� �ʰ���");
        }
        else
        {
            UIManager.Instance.Notice("�ùٸ� ���� �Է����ּ���.");
        }
    }
    public void UseSelectItem(PlayerStatus _player)
    {
        // ������ ���
        if (!selectItem.isCoolTime)
        {
            InventoryManager.Instance.IsConsumaableCoolTime = true;
            InventoryManager.Instance.UseItem(_player, selectItem);
            SetActiveItemInfo(false);
            ChangeInventorySlot(2);
            selectItem.isCoolTime = true;
        }
        else
            Debug.Log("��Ÿ�� ��");
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
        // UI Ȱ��ȭ 
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

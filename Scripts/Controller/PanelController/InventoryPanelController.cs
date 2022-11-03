using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPanelController : MonoBehaviour
{
    [Header("ItemInfo")]
    [SerializeField] private GameObject itemInfo = null;
    [SerializeField] private Button[] itemSkillButtons = null;
    [SerializeField] private Image[] itemSkillIconImages = null;
    [SerializeField] private GameObject itemSkillExplainPanel = null;
    [SerializeField] private TextMeshProUGUI itemSkillExplain = null;

    [Header("EquipedItemInfo")]
    [SerializeField] private GameObject equipedItemInfo = null;
    [SerializeField] private Button[] equipedItemSkillButtons = null;
    [SerializeField] private Image[] equipedItemSkillIconImages = null;
    [SerializeField] private GameObject equipedItemSkillExplainPanel = null;
    [SerializeField] private TextMeshProUGUI equipedItemSkillExplain = null;
    [SerializeField] private TextMeshProUGUI equipedItemCharacterName = null;


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


    [Header("MoenyText")]
    [SerializeField] private TextMeshProUGUI moneyText = null;

    [Header("Default")]
    [SerializeField] private Sprite UIMask = null;

    [SerializeField] private PlayerStatus playerStatus = null;
    [SerializeField] private AllyStatus[] characterStatus = null;
    [SerializeField] private EquipmentController[] characterEquipmentController = null;
    private bool isItemSelect = false;
    private bool isQuickSlotsOpen = false;
    [SerializeField] private TextMeshProUGUI iteminfoNameText = null;
    [SerializeField] private TextMeshProUGUI iteminfoTypeText = null;
    [SerializeField] private TextMeshProUGUI iteminfoExplainText = null;
    [SerializeField] private TextMeshProUGUI iteminfoRankText = null;


    [SerializeField] private TextMeshProUGUI equipedIteminfoNameText = null;
    [SerializeField] private TextMeshProUGUI equipedIteminfoTypeText = null;
    [SerializeField] private TextMeshProUGUI equipedIteminfoExplainText = null;
    [SerializeField] private TextMeshProUGUI equipedIteminfoRankText = null;

    private int selectInventoryIndex = 0;
    [SerializeField] private int selectCharNum = 0;
    [SerializeField] private Item selectItem = null;


    [SerializeField] private List<InventorySlot> inventorySlotList = new List<InventorySlot>();
    [SerializeField] private InventorySlot selectInventorySlot = null;
    [SerializeField] private List<EquipmentController> selectEquipmentController = new List<EquipmentController>();
    [SerializeField] private GameObject quickSlotSelectButtons = null;
    [SerializeField] private bool isItemSkillInfo = false;
    [SerializeField] private bool isEquipedItemSkillInfo = false;
    
    private void Awake()
    {
        inventorySlotList.AddRange(this.GetComponentsInChildren<InventorySlot>());
    }
    private void Update()
    {
        if (isItemSelect)
            UpdateItemInfo();

        if(isItemSkillInfo && Input.anyKey)
        {
            isItemSkillInfo = false;
            itemSkillExplainPanel.SetActive(false);
        }

        if(isEquipedItemSkillInfo && Input.anyKey)
        {
            isEquipedItemSkillInfo = false;
            equipedItemSkillExplainPanel.SetActive(false);
        }
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
        Debug.Log("���絷� " + playerStatus.Money); ;
    }

    public string ConvertMoney(int _money)
    {
        string _moneyText = null;
        int _tempMoney = _money;
        int _mark = 0;
        
        while(_mark < 2)
        {
            if(_tempMoney >= Mathf.Pow(10, 12))
            {
                _moneyText += ((int)(_tempMoney / Mathf.Pow(10, 12))).ToString() + "� ";
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
        // ����Ʈ ���
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
        SetActiveEquipedItemInfo(false);
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
        // ������ ���â Ȱ��ȭ ����
        itemInfo.SetActive(_bool);
        if(!_bool)
        {
            SetActiveEquipCharacterBox(false);
        }
    }
    public void SetActiveEquipedItemInfo(bool _bool)
    {
        // ������ ���â Ȱ��ȭ ����
        equipedItemInfo.SetActive(_bool);
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
    public bool CheckCharacterEquiped()
    {
        bool _bool = false;
        for (int i = 0; i < characterEquipmentController.Length; i++)
        {
            if (characterEquipmentController[i].CheckEquipItems[selectItem.itemType])
                _bool = true;
        }
        return _bool;
    }
    public void SelectCharacterEquiped()
    {
        for (int i = 0; i < characterEquipmentController.Length; i++)
        {
            if (characterEquipmentController[i].CheckEquipItems[selectItem.itemType])
            {
                selectEquipmentController.Add(characterEquipmentController[i]);
            }
        }

    }
    public void SelectCharacterEquipment(bool _isUp)
    {
        ResetEquipedItemInfoSkillButtons();
        if(_isUp)
        {
            selectCharNum++;
            if (selectCharNum == selectEquipmentController.Count)
                selectCharNum = 0;
        }
        else
        {
            selectCharNum--;
            if (selectCharNum == -1)
                selectCharNum = selectEquipmentController.Count - 1;
        }
        SetEquipedItemInfoPanel();
    }

    public void UpdateItemInfo()
    {
        // ������ ���â ����Ʈ
        selectCharNum = 0;
        isItemSelect = false;
        selectEquipmentController.Clear();
        ResetItemInfoSkillButtons();
        ResetInventoryButtons();
        SetActiveEquipCharacterBox(false);
        SetActiveItemInfo(true);
        SetActiveEquipedItemInfo(false);
        Debug.Log("�̾������� Ÿ��� " + selectItem.itemType);
        if(selectItem.itemType != 9 && selectItem.itemType != 10)
        {
            Debug.Log("�̾������� Ÿ���2 " + selectItem.itemType);
            if (CheckCharacterEquiped())
            {
                ResetEquipedItemInfoSkillButtons();
                SetActiveEquipedItemInfo(true);
                SelectCharacterEquiped();
                SetEquipedItemInfoPanel();
            }
        }

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
        SetItemInfoPanel();
    }
    public void SetItemInfoPanel()
    {
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
                    "���� ��ݷ�: " + selectItem.physicalDamage + "\n" +
                    "���� ��ݷ�: " + selectItem.magicalDamage + "\n" +
                    "��� ���: " + selectItem.atkRange + "\n" +
                    "��� �Ÿ�: " + selectItem.atkDistance + "\n" +
                    "���� ���: " + selectItem.weaponType + "\n";
                if (selectItem.grace1 != null)
                    iteminfoExplainText.text += "ù ��° ���: " + selectItem.grace1.explain;
                if (selectItem.grace2 != null)
                    iteminfoExplainText.text += "�� ��° ���: " + selectItem.grace2.explain;
                if (selectItem.grace3 != null)
                    iteminfoExplainText.text += "�� ��° ���: " + selectItem.grace3.explain;
                SetItemSkillIcon();

                break;
            case 13:
                iteminfoExplainText.text =
                    "ȸ���� : " + selectItem.value + "\n";
                break;
            case 14:
                iteminfoExplainText.text = "�̰�� ��Ʈ ������";
                break;
        }
    }
    public void SetEquipedItemInfoPanel()
    {
        equipedIteminfoNameText.text = selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].itemKorName;
        equipedIteminfoTypeText.text = KeyToItemType(selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].itemKey);
        equipedIteminfoRankText.text = IntRankToStringRank(selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].itemRank);
        equipedItemCharacterName.text = selectEquipmentController[selectCharNum].Status.ObjectName;
        switch (selectItem.itemKey / 1000)
        {
            case 0:
                equipedIteminfoExplainText.text = "This is Hair";
                break;
            case 1:
                equipedIteminfoExplainText.text = "This is FaceHair";
                break;
            case 2:
                equipedIteminfoExplainText.text = "����: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].defensivePower;
                break;
            case 3:
                equipedIteminfoExplainText.text = "����: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].defensivePower;
                break;
            case 4:
                equipedIteminfoExplainText.text = "����: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].defensivePower;
                break;
            case 5:
                equipedIteminfoExplainText.text = "����: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].defensivePower;
                break;
            case 6:
                equipedIteminfoExplainText.text = "����: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].defensivePower;
                break;
            case 7:
                equipedIteminfoExplainText.text = "����: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].defensivePower;
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                equipedIteminfoExplainText.text =
                    "���� ��ݷ�: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].physicalDamage + "\n" +
                    "���� ��ݷ�: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].magicalDamage + "\n" +
                    "��� ���: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].atkRange + "\n" +
                    "��� �Ÿ�: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].atkDistance + "\n" +
                    "���� ���: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].weaponType + "\n";
                if (selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].grace1 != null)
                    equipedIteminfoExplainText.text += "ù ��° ���: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].grace1.explain;
                if (selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].grace2 != null)
                    equipedIteminfoExplainText.text += "�� ��° ���: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].grace2.explain;
                if (selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].grace3 != null)
                    equipedIteminfoExplainText.text += "�� ��° ���: " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].grace3.explain;
                SetEquipedItemSkillIcon();
                break;
            case 13:
                equipedIteminfoExplainText.text =
                    "ȸ���� : " + selectEquipmentController[selectCharNum].EquipItems[selectItem.itemType].value + "\n";
                break;
            case 14:
                equipedIteminfoExplainText.text = "�̰�� ��Ʈ ������";
                break;
        }
    }
    public void SetItemSkillIcon()
    {
        for (int i = 0; i < 3; i++)
        {
            if (selectItem.skills[i] != null)
            {
                itemSkillButtons[i].gameObject.SetActive(true);
                itemSkillIconImages[i].sprite = selectItem.skills[i].singleSprite;
            }
        }
    }
    public void SetEquipedItemSkillIcon()
    {
        for (int i = 0; i < 3; i++)
        {
            if (selectEquipmentController[selectCharNum].EquipItems[8].skills[i] != null)
            {
                equipedItemSkillButtons[i].gameObject.SetActive(true);
                equipedItemSkillIconImages[i].sprite = selectEquipmentController[selectCharNum].EquipItems[8].skills[i].singleSprite;
            }
        }
    }
    public void ResetInventoryButtons()
    {
        // �κ��丮 ��ư ����
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            inventoryButtons[i].gameObject.SetActive(false);
        }
    }
    public void ResetItemInfoSkillButtons()
    {
        for (int i = 0; i < itemSkillButtons.Length; i++)
        {
            itemSkillButtons[i].gameObject.SetActive(false);
        }
    }
    public void ResetEquipedItemInfoSkillButtons()
    {
        for (int i = 0; i < itemSkillButtons.Length; i++)
        {
            equipedItemSkillButtons[i].gameObject.SetActive(false);
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
        // Ű�� ������ Ÿ����� ����
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
                SetActiveEquipCharacterBox(false);
                _characterList[_character].ChangeEquipment(selectItem);
                ChangeInventorySlot(selectInventoryIndex);
                SetActiveItemInfo(false);
                SetActiveEquipedItemInfo(false);

                selectItem = null;
            }
            else
                Debug.Log("������ ����մϴ�.");
        }
        else
        {
            Debug.Log("��Ÿ�� ��");
        }
    }

    public void TakeOffInventoryItem(List<EquipmentController> _characterList)
    {
        // �������
        if (selectItem.isEquip)
        {
            _characterList[selectItem.equipCharNum].TakeOffEquipment(selectItem);
        }
        else
            Debug.Log("������� �ƴ�");
        SetActiveItemInfo(false);
        ChangeInventorySlot(selectInventoryIndex);

        selectItem = null;
    }
 
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        // ���� ĳ���� �����ϱ� ��ư Ȱ��ȭ
        for (int i = 0; i < equipCharactersBtn.Length; i++)
            equipCharactersBtn[i].gameObject.SetActive(_bool);
    }
    public void SetActiveSkillIcon(bool _bool)
    {

    }
    public void SelectSlotItem(Item _item, InventorySlot _slot = null)
    {
        // ���Կ� ������ ������ 
        selectItem = _item;
        isItemSelect = true;
        selectInventorySlot = _slot;
    }



    public void DiscardSelectItem()
    {
        // ������ ���
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
                Debug.Log("���� ��� �ʰ���");
        }
        else
        {
            UIManager.Instance.Notice("�ùٸ� ��� �Է����ּ���.");
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
        SetActiveItemInfo(_bool);
        SetActiveFalseQuickSlotSelectButtons();
    }
    public void SetItemSkillExplain(int _index)
    {
        itemSkillExplainPanel.SetActive(true);
        itemSkillExplain.text = selectItem.skills[_index].skillExplain;
        isItemSkillInfo = true;
    }
    public void SetEquipedItemSkillExplain(int _index)
    {
        equipedItemSkillExplainPanel.SetActive(true);
        equipedItemSkillExplain.text = selectEquipmentController[selectCharNum].EquipItems[8].skills[_index].skillExplain;
        isEquipedItemSkillInfo = true;
    }
}

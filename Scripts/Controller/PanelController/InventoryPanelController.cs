using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private Text equipmentNameText = null;

    [Header("MoenyText")]
    [SerializeField]
    private Text moenyText;

    [Header("Default")]
    [SerializeField]
    private Sprite UIMask;

    private PlayerStatus playerStatus = null;
    private bool isItemSelect = false;
    [SerializeField]
    private int selectCharNum = 0;
    private int selectInventoryIndex = 0;
    [SerializeField]
    private Item selectItem = null;
    [SerializeField]
    private EquipmentController selectCharacterEqipment = null;
    [SerializeField]
    private CharacterStatus selectCharStatus = null;
    [SerializeField]
    private InventorySlot[] inventorySlots = null;
    [SerializeField]
    private EquipmentSlot[] equipmentSlots = null;
    private Text[] iteminfoText = null;

    private void Awake()
    {
        iteminfoText = itemInfo.GetComponentsInChildren<Text>();
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
        // �κ��丮 ���� ����
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SlotReset();
        }
    }
    public void MoneyUpdate()
    {
        moenyText.text = playerStatus.Money.ToString("N0"); 
    }
    public void InventorySlotChange(int _index)
    {
        // �κ��丮 ���� �ٲٱ� 
        InventoryReset();
        SetActiveItemInfo(false);
        MoneyUpdate();
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
        // ������ ����â Ȱ��ȭ ����
        itemInfo.SetActive(_bool);
        if(!_bool)
            SetActiveEquipCharacterBox(false);
    }
    public void SetActiveCheckDiscard(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        checkDiscard.SetActive(_bool);
    }
    public void SetActiveCheckDiscardAmount(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        checkAmount.SetActive(_bool);
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
            if (selectItem.equipCharNum != -1)
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
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 3:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 4:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 5:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 6:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 7:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
            case 8:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType +
                    "DefensivePower : " + selectItem.defensivePower;
                break;
            case 9:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
            case 10:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
            case 11:
                iteminfoText[2].text =
                    "Value : " + selectItem.value + "\n";
                break;
            case 12:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
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
    public void Equip(List<EquipmentController> _characterList,int _character)
    {
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
            InventorySlotChange(selectInventoryIndex);
            UpdateEquipmentName();
            ChangeAllEquipmentImage();
            SetActiveItemInfo(false);
            selectItem = null;
            if (_character == 0)
                UIManager.Instance.ChangePlayerUIItemImage();
            else
                UIManager.Instance.ChangeMercenaryUIItemImage(_character);
        }
        else
            Debug.Log("������ �����մϴ�.");
    }

    public void TakeOff(List<EquipmentController> _characterList)
    {
        // ��������
        if (selectItem.equipCharNum != -1)
        {
            Debug.Log("����>");
            _characterList[selectCharNum].TakeOffEquipment(selectItem);
            if (selectCharNum == 0)
                UIManager.Instance.ChangePlayerUIItemImage();
            else
                UIManager.Instance.ChangeMercenaryUIItemImage(selectCharNum);
        }
        else
            Debug.Log("�������� �ƴ�");
        SetActiveItemInfo(false);
        InitEquipmentSlotImage(selectItem.itemType);
        InventorySlotChange(selectInventoryIndex);
        UpdateEquipmentName();
    }
    public void SelectCharacter(List<EquipmentController> _characterList)
    {
        selectCharNum = selectItem.equipCharNum;
        selectCharacterEqipment = _characterList[selectCharNum];
        selectCharStatus = _characterList[selectCharNum].GetComponent<CharacterStatus>();
    }
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        // ���� ĳ���� �����ϱ� ��ư Ȱ��ȭ
        equipCharactersBtn[0].gameObject.SetActive(_bool);
        for (int i = 0; i < UIManager.Instance.GetMercenaryNum(); i++)
            equipCharactersBtn[i + 1].gameObject.SetActive(_bool);
    }

    public void SelectSlotItem(Item _item)
    {
        // ���Կ� ������ ������ 
        selectItem = _item;
        isItemSelect = true;
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
        equipmentSlots[_index].CurItem = selectItem;
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
        selectCharStatus = _charaterList[selectCharNum].GetComponent<CharacterStatus>();
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
            Debug.Log("�������� ���� �ʰ���");
    }
    public void UseSelectItem(PlayerStatus _player)
    {
        // ������ ���
        InventoryManager.Instance.UseItem(_player, selectItem);
        SetActiveItemInfo(false);
        InventorySlotChange(2);
    }
    public void ActiveInventoryPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        UIImages.SetActive(_bool);
        if(_bool)
            ChangeAllEquipmentImage();
        else
            SetActiveItemInfo(_bool);
    }
}

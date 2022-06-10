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

    [Header("Default")]
    [SerializeField]
    private Sprite UIMask;

    private bool isItemSelect = false;
    [SerializeField]
    private int selectNum = 0;
    private int selectInventoryIndex = 0;
    [SerializeField]
    private Item selectItem = null;
    [SerializeField]
    private EquipmentController selectCharacterEqipment = null;
    [SerializeField]
    private CharacterStatus selectCharStatus = null;
    private InventorySlot[] inventorySlots = null;
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
        selectCharacterEqipment = _player.GetComponent<EquipmentController>();
        selectCharStatus = _player;
    }
    public void InventoryReset()
    {
        // �κ��丮 ���� ����
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SlotReset();
        }
    }

    public void InventorySlotChange(int _index)
    {
        // �κ��丮 ���� �ٲٱ� 
        InventoryReset();
        SetActiveItemInfo(false);

        if (_index == 0)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyWeaponItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyWeaponItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyWeaponItems[i];
                inventorySlots[i].IsItemChange = true;
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        if (_index == 1)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyEquipmentItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyEquipmentItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyEquipmentItems[i];
                inventorySlots[i].IsItemChange = true;
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        if (_index == 2)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyConsumableItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyConsumableItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyConsumableItems[i];
                inventorySlots[i].IsItemChange = true;
                inventorySlots[i].SlotSetting();
            }
        }
        if (_index == 3)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyMiscellaneousItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyMiscellaneousItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyMiscellaneousItems[i];
                inventorySlots[i].IsItemChange = true;
                inventorySlots[i].SlotSetting();

            }
        }
        if (_index == 4)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyDecorationItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyDecorationItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyDecorationItems[i];
                inventorySlots[i].IsItemChange = true;
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
            SetActiveEquipCharacterBox( false);
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
        // �����ϱ� ��ư
        if (_characterList[_character].CheckEquipItems[selectItem.itemType])
        {
            inventorySlots[InventoryManager.Instance.IndexOfItem(_characterList[_character].EquipItems[selectItem.itemType])].IsItemChange = true;
            _characterList[_character].TakeOffEquipment(_characterList[_character].EquipItems[selectItem.itemType]);
        }
        selectItem.equipCharNum = _character;
        SelectCharacter(_characterList);
        _characterList[_character].ChangeEquipment(selectItem);
        inventorySlots[InventoryManager.Instance.IndexOfItem(selectItem)].IsItemChange = true;
        SetActiveEquipCharacterBox(false);
        ChangeEquipmentImage();
        UpdateEquipmentName();
        SetActiveItemInfo(false);
        selectItem = null;
        if (_character == 0)
            UIManager.Instance.ChangePlayerUIItemImage();
        else
            UIManager.Instance.ChangeMercenaryUIItemImage(_character - 1);
    }

    public bool IsEquipingCharacter(EquipmentController _char, Item _equipItem)
    {
        // ���� ĳ���Ͱ� �ش� �������� ������ ���������� üũ
        if (_char.CheckEquipItems[_equipItem.itemType])
            return true;
        else
            return false;
    }
    public void TakeOff(List<EquipmentController> _characterList)
    {
        // ��������
        if (selectItem.isEquip)
        {
            SelectCharacter(_characterList);
            inventorySlots[InventoryManager.Instance.IndexOfItem(selectItem)].IsItemChange = true;
            _characterList[selectNum].TakeOffEquipment(selectItem);
            if (selectNum == 0)
                UIManager.Instance.ChangePlayerUIItemImage();
            else
                UIManager.Instance.ChangeMercenaryUIItemImage(selectNum - 1);
        }
        else
            Debug.Log("�������� �ƴ�");
        SetActiveItemInfo(false);
        ChangeEquipmentImage();
        UpdateEquipmentName();
    }
    public void SelectCharacter(List<EquipmentController> _characterList)
    {
        selectNum = selectItem.equipCharNum;
        selectCharacterEqipment = _characterList[selectNum];
        selectCharStatus = _characterList[selectNum].GetComponent<CharacterStatus>();
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
    public void ChangeEquipmentImage()
    { 
        // ���â �̹��� �ٲٱ�
        for (int i = 0; i < selectCharacterEqipment.EquipItems.Length; i++)
        {
            if (selectCharacterEqipment.CheckEquipItems[i])
            {
                equipmentSlots[i].ItemImages[1].sprite = selectCharacterEqipment.EquipItems[i].singleSprite;
                equipmentSlots[i].SlotSetting(selectCharacterEqipment.EquipItems[i]);
            }
            else
            {
                TakeOffEquipmentImage(i);
            }
        }
    }
    public void TakeOffEquipmentImage(int _index)
    {
        // ��� ������ ���â �̹��� ����
        equipmentSlots[_index].ItemImages[1].sprite = UIMask;
        equipmentSlots[_index].InitImageSize();
    }
    public void SelectCharacterInEquipment(List<EquipmentController> _charaterList, bool _isUp)
    {
        // ���â���� ĳ���� ����
        InitEquipment();
        if (_isUp)
        {
            selectNum++;
            if (selectNum == _charaterList.Count)
                selectNum = 0;
        }
        else
        {
            selectNum--;
            if (selectNum < 0)
                selectNum = _charaterList.Count;
        }
        selectCharacterEqipment = _charaterList[selectNum];
        selectCharStatus = _charaterList[selectNum].GetComponent<CharacterStatus>();
        UpdateEquipmentName();
        ChangeEquipmentImage();
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
    public void ActiveInventory()
    {
        // UI Ȱ��ȭ 
        UIImages.SetActive(true);
        ChangeEquipmentImage();
        
    }

    public void DeactiveInventory()
    {
        // UI ��Ȱ��ȭ
        UIImages.SetActive(false);
        SetActiveItemInfo(false);
    }
}

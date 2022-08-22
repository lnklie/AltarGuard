using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisassemblePanelController : MonoBehaviour
{
    [Header("DisassembleItemInfo")]
    [SerializeField]
    private GameObject DisassembleItemInfo = null;

    [Header("DisassembleCheckPanel")]
    [SerializeField]
    private GameObject disassembleCheckPanel = null;

    [SerializeField]
    private List<DisassembleSlot> disassembleSlots = new List<DisassembleSlot>();

    [SerializeField]
    private Item selectDisassembleItem = null;
    [SerializeField]
    private List<DisassembleInventorySlot> disassembleInventorySlots = new List<DisassembleInventorySlot>();
    private TextMeshProUGUI[] disassembleItemInfoText = null;

    [SerializeField]
    private TextMeshProUGUI disassembleCheckPanelAquireResourcesText = null;

    [SerializeField]
    private List<Item> disassembleItemList = new List<Item>();
    [SerializeField]
    private List<Item> disassembleItemResourcesList = new List<Item>();

    private bool isDisassembleItemSelect = false;
    private int selectInventoryIndex = 0;
    public bool IsDisassembleItemSelect
    {
        get { return isDisassembleItemSelect; }
        set { isDisassembleItemSelect = value; }
    }
    public Item SelectDisassembleItem
    {
        get { return selectDisassembleItem; }
        set { selectDisassembleItem = value; }
    }
    public List<Item> DisassembleItemList
    {
        get { return disassembleItemList; }
    }
    private void Awake()
    {
        disassembleItemInfoText = DisassembleItemInfo.GetComponentsInChildren<TextMeshProUGUI>();
        disassembleSlots.AddRange(this.GetComponentsInChildren<DisassembleSlot>());
        disassembleInventorySlots.AddRange(GetComponentsInChildren<DisassembleInventorySlot>());

    }
    private void Update()
    {
        if (isDisassembleItemSelect)
            UpdateItemInfo();
    }


    public void ResetDisassembleInventory()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < disassembleInventorySlots.Count; i++)
        {
            disassembleInventorySlots[i].SlotReset();
        }
    }
    public void UpdateDisassembleInventorySlot(int _index)
    {
        // 인벤토리 슬롯 바꾸기 
        ResetDisassembleInventory();
        SetActiveDisassembleItemInfo(false);
        if (_index == 0)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyWeaponItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyWeaponItems);
                disassembleInventorySlots[i].CurItem = InventoryManager.Instance.InventroyWeaponItems[i];
                disassembleInventorySlots[i].SlotSetting();

            }
        }
        if (_index == 1)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyEquipmentItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyEquipmentItems);
                disassembleInventorySlots[i].CurItem = InventoryManager.Instance.InventroyEquipmentItems[i];
                disassembleInventorySlots[i].SlotSetting();
            }
        }
        selectInventoryIndex = _index;
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
    public void UpdateItemInfo()
    {
        // 아이템 정보창 업데이트
        isDisassembleItemSelect = false;
        SetActiveDisassembleItemInfo(true);

        disassembleItemInfoText[0].text = selectDisassembleItem.itemName;
        disassembleItemInfoText[1].text = KeyToItemType(selectDisassembleItem.itemKey);
        switch (selectDisassembleItem.itemKey / 1000)
        {
            case 0:
                disassembleItemInfoText[2].text = "This is Hair";
                break;
            case 1:
                disassembleItemInfoText[2].text = "This is FaceHair";
                break;
            case 2:
                disassembleItemInfoText[2].text = "DefensivePower : " + selectDisassembleItem.defensivePower;
                break;
            case 3:
                disassembleItemInfoText[2].text = "DefensivePower : " + selectDisassembleItem.defensivePower;
                break;
            case 4:
                disassembleItemInfoText[2].text = "DefensivePower : " + selectDisassembleItem.defensivePower;
                break;
            case 5:
                disassembleItemInfoText[2].text = "DefensivePower : " + selectDisassembleItem.defensivePower;
                break;
            case 6:
                disassembleItemInfoText[2].text = "DefensivePower : " + selectDisassembleItem.defensivePower;
                break;
            case 7:
                disassembleItemInfoText[2].text =
                    "PysicalDamage : " + selectDisassembleItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectDisassembleItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectDisassembleItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectDisassembleItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectDisassembleItem).weaponType;
                break;
            case 8:
                disassembleItemInfoText[2].text =
                    "PysicalDamage : " + selectDisassembleItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectDisassembleItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectDisassembleItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectDisassembleItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectDisassembleItem).weaponType +
                    "DefensivePower : " + selectDisassembleItem.defensivePower;
                break;
            case 9:
                disassembleItemInfoText[2].text =
                    "PysicalDamage : " + selectDisassembleItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectDisassembleItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectDisassembleItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectDisassembleItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectDisassembleItem).weaponType;
                break;
            case 10:
                disassembleItemInfoText[2].text =
                    "PysicalDamage : " + selectDisassembleItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectDisassembleItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectDisassembleItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectDisassembleItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectDisassembleItem).weaponType;
                break;
            case 11:
                disassembleItemInfoText[2].text =
                    "Value : " + selectDisassembleItem.value + "\n";
                break;
            case 12:
                disassembleItemInfoText[2].text =
                    "PysicalDamage : " + selectDisassembleItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectDisassembleItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectDisassembleItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectDisassembleItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectDisassembleItem).weaponType;
                break;
        }
    }
    public void SetActiveDisassembleItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        DisassembleItemInfo.SetActive(_bool);
    }
    public void SetActiveDisassemblePanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public void SelectSlotDisassembleItem(Item _item)
    {
        // 슬롯에 선택한 아이템 
        selectDisassembleItem = _item;
        isDisassembleItemSelect = true;
    }
    public void RegisterItem()
    {

        if (!CheckIsRegistered())
        {
            disassembleItemList.Add(selectDisassembleItem);
            UpdateDisassembleInventorySlot(selectInventoryIndex);
            UpdateDisassembleSlot();
            SetActiveDisassembleItemCheckBox(false);
            if(CheckSameResources(selectDisassembleItem))
            {
                GetSameResources(selectDisassembleItem).count += selectDisassembleItem.disassembleItemAmount;
            }
            else
            {
                Item _item = DatabaseManager.Instance.SelectItem(selectDisassembleItem.disassembleItemKey);
                _item.count = selectDisassembleItem.disassembleItemAmount;
                disassembleItemResourcesList.Add(_item);
            }
        }
        else
            Debug.Log("이미 등록되어 있음");
    }
    public void SetActiveDisassembleItemCheckBox(bool _bool)
    {
        disassembleCheckPanel.SetActive(_bool);
    }
    public void UpdateDisassembleCheckBox()
    {
        SetActiveDisassembleItemCheckBox(true);

        string _AquireResourcesText = null;
        for(int i = 0; i < disassembleItemResourcesList.Count; i++)
        {
            _AquireResourcesText += disassembleItemResourcesList[i].itemName + " (" + disassembleItemResourcesList[i].count + ")" + "\n";
        }

        disassembleCheckPanelAquireResourcesText.text = _AquireResourcesText;
    }

    public void ResetDisassembleList()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < disassembleSlots.Count; i++)
        {
            disassembleSlots[i].SlotReset();
        }

    }
    public void UpdateDisassembleSlot()
    {
        // 인벤토리 슬롯 바꾸기 
        ResetDisassembleList();
        for (int i = 0; i < disassembleItemList.Count; i++)
        {
            disassembleSlots[i].CurItem = disassembleItemList[i];
            disassembleSlots[i].SlotSetting();
        }
    }
    public void DisassembleItem()
    {
        ResetDisassembleList();
        for (int i = 0; i < disassembleItemList.Count; i++)
        {
            Item _item = InventoryManager.Instance.SelectItem(disassembleItemList[i]);
            InventoryManager.Instance.DiscardItem(_item);
        }
        for(int i = 0; i < disassembleItemResourcesList.Count; i++)
        {
            Item _item = disassembleItemResourcesList[i];
            InventoryManager.Instance.AcquireItem(_item);
        }
        UpdateDisassembleInventorySlot(selectInventoryIndex);
        disassembleItemList.Clear();
        SetActiveDisassembleItemCheckBox(false);
    }
    public void CancelAllRegisteredItem()
    {
        disassembleItemList.Clear();
        disassembleItemResourcesList.Clear();
        ResetDisassembleList();
    }
    public void CancelRegisteredItem(Item _item)
    {
        disassembleItemList.Remove(_item);
        if(CheckSameResources(_item))
        {
            GetSameResources(_item).count -= _item.disassembleItemAmount;
            if (GetSameResources(_item).count <= 0)
                disassembleItemResourcesList.Remove(GetSameResources(_item));
        }
        UpdateDisassembleSlot();
        UpdateDisassembleInventorySlot(selectInventoryIndex);
    } 

    public bool CheckIsRegistered()
    {
        bool _bool = false;
        for (int i = 0; i < disassembleItemList.Count; i++)
        {
            if (selectDisassembleItem == disassembleItemList[i])
            {
                _bool = true;
                break;
            }
            else
            {
                _bool = false;
            }
        }
        return _bool;
    }
    public bool CheckSameResources(Item _item)
    {
        bool _bool = false;
        for(int i =0; i < disassembleItemResourcesList.Count; i++)
        {
            if(_item.disassembleItemKey == disassembleItemResourcesList[i].itemKey)
            {
                _bool = true;
                break;
            }
            else
            {
                _bool = false;
                Debug.Log("없던 것");
            }
        }
        return _bool; 
    }
    public Item GetSameResources(Item _item)
    {
        Item __item = null;
        for (int i = 0; i < disassembleItemResourcesList.Count; i++)
        {
            if (_item.disassembleItemKey == disassembleItemResourcesList[i].itemKey)
            {
                __item = disassembleItemResourcesList[i];
                break;
            }
        }
        return __item;
    }
}

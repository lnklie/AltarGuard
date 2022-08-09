using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject craftInfo = null;
    [SerializeField]
    private TextMeshProUGUI craftItemName = null;
    [SerializeField]
    private TextMeshProUGUI craftItemInfo = null;

    [SerializeField]
    private GameObject necessaryIteminfoPanel = null;
    [SerializeField]
    private TextMeshProUGUI necessaryItemName = null;
    [SerializeField]
    private TextMeshProUGUI necessaryItemType = null;
    [SerializeField]
    private TextMeshProUGUI necessaryItemInfo = null;



    [SerializeField]
    private CraftRecipe selectCraftRecipe = null;
    [SerializeField]
    private Item selectNeedItem = null;

    [SerializeField]
    private GameObject needsItems = null;
    private CraftNecessaryItemSlot[] craftNecessaryItemSlots = null;
    private TextMeshProUGUI[] needsItemTexts = null;
    private bool isSelected = false;
    private bool isNeedItemSelected = false;
    private bool[] isAbleCraft = new bool[4];

    [SerializeField]
    private Sprite defalutSprite = null;
    public CraftRecipe SelectCraftRecipe
    {
        get { return selectCraftRecipe; }
        set { selectCraftRecipe = value; }
    }
    public Item SelectNeedItem
    {
        get { return selectNeedItem; }
        set { selectNeedItem = value; }
    }
    public bool IsNeedItemSelected
    {
        get { return isNeedItemSelected; }
        set { isNeedItemSelected = value; }
    }
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }
    private void Awake()
    {
        craftNecessaryItemSlots = needsItems.GetComponentsInChildren<CraftNecessaryItemSlot>();
        needsItemTexts = needsItems.GetComponentsInChildren<TextMeshProUGUI>();
        //texts = craftInfo.GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (isSelected)
            UpdateCraftRecipe();

        if (isNeedItemSelected)
            UpdateNecessaryItemInfo();

    }
    public void UpdateCraftRecipe()
    {
        isSelected = false;
        SetActiveNecessaryItemInfoPanel(false);
        InitNecessaryItem();
        InitNecessaryItemCount();
        Item _item = DatabaseManager.Instance.SelectItem(selectCraftRecipe.completeItemKey);

        craftItemName.text = _item.itemName;
        switch (_item.itemKey / 1000)
        {
            case 0:
                craftItemInfo.text = "This is Hair";
                break;
            case 1:
                craftItemInfo.text = "This is FaceHair";
                break;
            case 2:
                craftItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 3:
                craftItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 4:
                craftItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 5:
                craftItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 6:
                craftItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 7:
                craftItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType;
                break;
            case 8:
                craftItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType + "\n" +
                    "방어력: " + _item.defensivePower;
                break;
            case 9:
                craftItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType;
                break;
            case 10:
                craftItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType;
                break;
            case 11:
                craftItemInfo.text =
                    "회복량 : " + _item.value + "\n";
                break;
            case 12:
                craftItemInfo.text = "이것은 퀘스트 아이템";
                break;
        }
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey1,0);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount1, 0);
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey2, 1);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount2, 1);
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey3, 2);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount3, 2);
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey4, 3);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount4, 3);
    }
    public bool CheckPossessedItem(Item _Item, int _amount = 1)
    {
        bool _bool = false;
        switch (_Item.itemType)
        {
            case 0:
            case 1:
                for (int i = 0; i < InventoryManager.Instance.InventroyDecorationItems.Count; i++)
                {
                    if (_Item.itemKey == InventoryManager.Instance.InventroyDecorationItems[i].itemKey)
                        _bool = true;
                }
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                for (int i = 0; i < InventoryManager.Instance.InventroyEquipmentItems.Count; i++)
                {
                    if (_Item.itemKey == InventoryManager.Instance.InventroyEquipmentItems[i].itemKey)
                        _bool = true;
                }
                break;
            case 7:
            case 8:
                for (int i = 0; i < InventoryManager.Instance.InventroyWeaponItems.Count; i++)
                {
                    if (_Item.itemKey == InventoryManager.Instance.InventroyWeaponItems[i].itemKey)
                        _bool = true;
                }
                break;
            case 9:
                for (int i = 0; i < InventoryManager.Instance.InventroyConsumableItems.Count; i++)
                {
                    if (_Item.itemKey == InventoryManager.Instance.InventroyConsumableItems[i].itemKey 
                        && InventoryManager.Instance.InventroyConsumableItems[i].count >= _amount )
                        _bool = true;
                }
                break;
            case 10:
                for (int i = 0; i < InventoryManager.Instance.InventroyMiscellaneousItems.Count; i++)
                {
                    if (_Item.itemKey == InventoryManager.Instance.InventroyMiscellaneousItems[i].itemKey 
                        && InventoryManager.Instance.InventroyMiscellaneousItems[i].count >= _amount)
                        _bool = true;
                }
                break;
        }
        return _bool;
    }
    public void UpdateNecessaryItemInfo()
    {
        isNeedItemSelected = false;
        SetActiveNecessaryItemInfoPanel(true);
        Item _item = selectNeedItem;
        necessaryItemName.text = _item.itemName;
        necessaryItemType.text = KeyToItemType(_item.itemKey);
        switch (_item.itemKey / 1000)
        {
            case 0:
                necessaryItemInfo.text = "This is Hair";
                break;
            case 1:
                necessaryItemInfo.text = "This is FaceHair";
                break;
            case 2:
                necessaryItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 3:
                necessaryItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 4:
                necessaryItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 5:
                necessaryItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 6:
                necessaryItemInfo.text = "방어력: " + _item.defensivePower;
                break;
            case 7:
                necessaryItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType;
                break;
            case 8:
                necessaryItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType + "\n" +
                    "방어력: " + _item.defensivePower;
                break;
            case 9:
                necessaryItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType;
                break;
            case 10:
                necessaryItemInfo.text =
                    "물리 공격력: " + _item.physicalDamage + "\n" +
                    "마법 공격력: " + _item.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)_item).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)_item).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)_item).weaponType;
                break;
            case 11:
                necessaryItemInfo.text =
                    "회복량 : " + _item.value + "\n";
                break;
            case 12:
                necessaryItemInfo.text = "이것은 퀘스트 아이템";
                break;
        }

    }
    public void SetActiveNecessaryItemInfoPanel(bool _bool)
    {
        necessaryIteminfoPanel.SetActive(_bool);
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
    public void SetNecessaryItem(int _itemKey, int _Index)
    {
        if (_itemKey != -1)
        {
            craftNecessaryItemSlots[_Index].IsNecessaryItem = true;
            craftNecessaryItemSlots[_Index].NecessaryItem = DatabaseManager.Instance.SelectItem(_itemKey);
            craftNecessaryItemSlots[_Index].GetComponentsInChildren<Image>()[1].sprite = DatabaseManager.Instance.SelectItem(_itemKey).singleSprite;
        }
    }
    public void InitNecessaryItem()
    {
        for(int i = 0; i < craftNecessaryItemSlots.Length; i++)
        {
            craftNecessaryItemSlots[i].IsNecessaryItem = false;
            craftNecessaryItemSlots[i].NecessaryItem = null;
            craftNecessaryItemSlots[i].GetComponentsInChildren<Image>()[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
            craftNecessaryItemSlots[i].GetComponentsInChildren<Image>()[1].sprite = defalutSprite;
        }
    }
    public void SetNecessaryItemCount(int _itemCount, int _textIndex)
    {
        if (_itemCount != -1)
        {
            needsItemTexts[_textIndex].text = _itemCount.ToString();
            Debug.Log("_itemCount는 " + _itemCount);
            if (CheckPossessedItem(craftNecessaryItemSlots[_textIndex].NecessaryItem, _itemCount))
            {
                needsItemTexts[_textIndex].color = new Color(0f, 0f, 255f);
                isAbleCraft[_textIndex] = true;
            }
            else
            {
                needsItemTexts[_textIndex].color = new Color(255f, 0f, 0f);
                isAbleCraft[_textIndex] = false; 
            }
        }
        else
        {
            needsItemTexts[_textIndex].color = new Color(255f, 0f, 0f);
            isAbleCraft[_textIndex] = true;
        }
    }
    public void InitNecessaryItemCount()
    {
        for (int i = 0; i < needsItemTexts.Length; i++)
        {
            needsItemTexts[i].text = 00.ToString();
        }
        
    }
    public void SetActiveCraftPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public bool IsAbleCraft()
    {
        bool _bool = false;
        for (int i = 0; i < isAbleCraft.Length; i++)
        {
            if (isAbleCraft[i])
            {
                _bool = true;
            }
            else
            {
                _bool = false;
                break;
            }
        }
        return _bool;
    }
    public void Craft()
    {
        if(IsAbleCraft())
        {
            //if (craftNecessaryItemSlots[1].IsNecessaryItem)
            //{
            //    InventoryManager.Instance.DiscardItem( )
            //}
        }
        else
        {

        }
    }
}

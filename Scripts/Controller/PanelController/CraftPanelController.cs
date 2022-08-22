using TMPro;
using UnityEngine;
public class CraftPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject craftInfo = null;
    [SerializeField]
    private GameObject necessaryItemRegisterInventory = null; 
    [SerializeField]
    private GameObject necessaryIteminfoPanel = null;
    [SerializeField]
    private GameObject necessaryItemRegiterPanel = null;


    [SerializeField]
    private TextMeshProUGUI craftItemName = null;
    [SerializeField]
    private TextMeshProUGUI craftItemInfo = null;

    [SerializeField]
    private TextMeshProUGUI necessaryItemName = null;
    [SerializeField]
    private TextMeshProUGUI necessaryItemType = null;
    [SerializeField]
    private TextMeshProUGUI necessaryItemInfo = null;



    [SerializeField]
    private CraftRecipe selectCraftRecipe = null;
    [SerializeField]
    private Item selectNeedItemInfo = null;
    [SerializeField]
    private Item selectRegisterItem = null;
    [SerializeField]
    private Item selectRegisterInventoryItem = null;

    [SerializeField]
    private GameObject needsItems = null;
    [SerializeField]
    private CraftNecessaryItemSlot[] craftNecessaryItemSlots = null;
    [SerializeField]
    private CraftNecessaryItemInventorySlot[] craftNecessaryItemInventorySlots = null;

    //[SerializeField]
    //private TextMeshProUGUI[] needsItemCountTexts = null;
    private bool isSelected = false;
    private bool isNeedItemInfoSelected = false;
    private bool isRegisterNecessaryItemSelect = false;
    private bool isRegisterInventoryItemSelect = false;
    [SerializeField]
    private bool[] isAbleCraft = new bool[4];
    [SerializeField]
    private Item[] craftResources = new Item[4];

    [SerializeField]
    private int selectNecessaryItemIndex = -1; 

    public int SelectNecessaryItemIndex
    {
        get { return selectNecessaryItemIndex; }
        set { selectNecessaryItemIndex = value; }
    }
    public CraftRecipe SelectCraftRecipe
    {
        get { return selectCraftRecipe; }
        set { selectCraftRecipe = value; }
    }
    public Item SelectNeedItemInfo
    {
        get { return selectNeedItemInfo; }
        set { selectNeedItemInfo = value; }
    }
    public Item SelectRegisterItem
    {
        get { return selectRegisterItem; } 
        set{ selectRegisterItem = value; }
            
    }
    public Item SelectRegisterInventoryItem
    {
        get { return selectRegisterInventoryItem; }
        set { selectRegisterInventoryItem = value;}
    }
    public bool IsNeedItemInfoSelected
    {
        get { return isNeedItemInfoSelected; }
        set { isNeedItemInfoSelected = value; }
    }
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }
    public bool IsRegisterNecessaryItemSelect
    {
        get { return isRegisterNecessaryItemSelect; }
        set { isRegisterNecessaryItemSelect = value; }
    }
    public bool IsRegisterInventoryItemSelect
    {
        get { return isRegisterInventoryItemSelect; }
        set { isRegisterInventoryItemSelect = value; }
    }
    private void Awake()
    {
    }
    private void Update()
    {
        if (isSelected)
            UpdateCraftRecipe();

        if (isNeedItemInfoSelected)
            UpdateNecessaryItemInfo();

        if (IsRegisterNecessaryItemSelect)
            UpdateNecessaryItemInventory();

            
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
        for(int i = 0; i < 4; i++)
        {
            SetNecessaryItem(selectCraftRecipe.necessaryItemKeies[i],i);
            //SetNecessaryItemCount(selectCraftRecipe.necessaryItemCounts[i], i);
        }
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
    public bool CheckRegisteredItem(int _index)
    {
        bool _bool = false;
        if (isAbleCraft[_index])
        {
            _bool = true;
        }
        return _bool;
    }
    public void UpdateNecessaryItemInfo()
    {
        isNeedItemInfoSelected = false;
        
        SetActiveNecessaryItemInfoPanel(true);
        Item _item = selectNeedItemInfo;
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
    public void UpdateNecessaryItemInventory()
    {
        if (!isAbleCraft[selectNecessaryItemIndex])
        {
            SetActiveCraftNecessaryItemInventoryPanel(true);
            isRegisterNecessaryItemSelect = false;
            if(InventoryManager.Instance.KeyToItems(selectRegisterItem.itemKey).Count <= 0)
            {
                InitcraftNecessaryItemInventorySlots();
            }
            else
            {
                for (int i = 0; i < InventoryManager.Instance.KeyToItems(selectRegisterItem.itemKey).Count;i++)
                {
                    craftNecessaryItemInventorySlots[i].InitSlot();
                    craftNecessaryItemInventorySlots[i].SetSlot(InventoryManager.Instance.KeyToItems(selectRegisterItem.itemKey)[i]);
                }
            }
        }
        else
        {
            isRegisterNecessaryItemSelect = false;
            if (necessaryItemRegisterInventory.activeSelf)
                necessaryItemRegisterInventory.SetActive(false);
            Debug.Log("이미 등록된 물품입니다.");
        }
    }
    public void InitcraftNecessaryItemInventorySlots()
    {
        for (int i = 0; i < craftNecessaryItemInventorySlots.Length; i++)
        {
            craftNecessaryItemInventorySlots[i].InitSlot();
        }
    }
    public void SetActiveNecessaryItemInfoPanel(bool _bool)
    {
        necessaryIteminfoPanel.SetActive(_bool);
        if (necessaryItemRegisterInventory.activeSelf)
            necessaryItemRegisterInventory.SetActive(false);
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
            craftNecessaryItemSlots[_Index].SetNecessaryItem(_itemKey);
        }
    }
    public void InitNecessaryItem()
    {
        for(int i = 0; i < craftNecessaryItemSlots.Length; i++)
        {
            craftNecessaryItemSlots[i].InitNecessaryItem();
            craftResources[i] = null;
            isAbleCraft[i] = false;
        }
    }
    public void SetNecessaryItemCount(int _itemCount, int _textIndex)
    {
        if (_itemCount != -1)
        {
            craftNecessaryItemSlots[_textIndex].SetNecessaryItemCount(_itemCount);
            if (CheckRegisteredItem(_textIndex))
            {
                craftNecessaryItemSlots[_textIndex].SetNecessaryItemCountColor(true);
            }
            else
            {
                craftNecessaryItemSlots[_textIndex].SetNecessaryItemCountColor(false);
            }
        }
        else
        {
            craftNecessaryItemSlots[_textIndex].SetNecessaryItemCountColor(false);
        }
    }
    public void InitNecessaryItemCount()
    {
        for (int i = 0; i < craftNecessaryItemSlots.Length; i++)
        {
            craftNecessaryItemSlots[i].InitNecessaryItemCount();
            SetNecessaryItemCount(selectCraftRecipe.necessaryItemCounts[i], i);
        }
    }
    public void SetActiveCraftPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public void SetActiveRegisterNecessaryItemPanel(bool _bool)
    {
        necessaryItemRegiterPanel.SetActive(_bool);
    }
    public void SetActiveCraftNecessaryItemInventoryPanel(bool _bool)
    {
        necessaryItemRegisterInventory.SetActive(_bool);
        if(necessaryIteminfoPanel.activeSelf)
            necessaryIteminfoPanel.SetActive(false);
    }
    public bool IsAbleCraft()
    {
        bool _bool = false;
        for (int i = 0; i < isAbleCraft.Length; i++)
        {
            if (isAbleCraft[i] || selectCraftRecipe.necessaryItemKeies[i] == -1)
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
    public void Register()
    {

        if (selectRegisterInventoryItem.itemKey == selectCraftRecipe.necessaryItemKeies[selectNecessaryItemIndex])
        {
            craftResources[selectNecessaryItemIndex] = selectRegisterInventoryItem;
            isAbleCraft[selectNecessaryItemIndex] = true;
        }

        SetActiveRegisterNecessaryItemPanel(false);
        SetActiveCraftNecessaryItemInventoryPanel(false);
        InitNecessaryItemCount();


    }
    public void Craft()
    {
        if(IsAbleCraft())
        {
            for(int i = 0; i < craftResources.Length; i++)
            {
                InventoryManager.Instance.DiscardItem(craftResources[i], craftResources[i].count);
                craftResources[i] = null;
                isAbleCraft[i] = false;
            }
            InitNecessaryItemCount();
            InitcraftNecessaryItemInventorySlots();
            InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(selectCraftRecipe.completeItemKey));
        }
        else
        {
            Debug.Log("제작 불가");
        }
    }
}

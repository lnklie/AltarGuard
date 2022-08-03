using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanelController : MonoBehaviour
{
    [Header("SellItemInfo")]
    [SerializeField]
    private GameObject sellItemInfo = null;

    [Header("SellItemAmount")]
    [SerializeField]
    private GameObject sellItemAmount = null;

    [SerializeField]
    private SellSlot[] sellSlots = null;

    [SerializeField]
    private Item selectSellItem = null;
    [SerializeField]
    private ShopInventorySlot[] shopInventorySlots = null;
    private Text[] sellItemInfoText = null;
    [SerializeField]
    private List<Item> sellItemList = new List<Item>(); 
    [Header("PlayerMoenyText")]
    [SerializeField]
    private Text moneyText = null;
    [Header("SellMoenyText")]
    [SerializeField]
    private Text sellMoneyText = null;
    [SerializeField]
    private PlayerStatus playerStatus = null;
    private bool isSellItemSelect = false;
    private Button registerButton = null;
    [SerializeField]
    private InputField sellAmount = null;
    private int selectInventoryIndex = 0;
    private int sellMoney = 0;
    public List<Item> SellItemList
    {
        get { return sellItemList; }
    }
    private void Awake()
    {
        sellItemInfoText = sellItemInfo.GetComponentsInChildren<Text>();
        sellSlots = this.GetComponentsInChildren<SellSlot>();
        shopInventorySlots = this.GetComponentsInChildren<ShopInventorySlot>();

    }
    private void Update()
    {
        if (isSellItemSelect)
            UpdateItemInfo();
    }

    public void MoneyUpdate()
    {
        moneyText.text = playerStatus.Money.ToString("N0");
    }

    public void ResetSellInventory()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < shopInventorySlots.Length; i++)
        {
            shopInventorySlots[i].SlotReset();
        }
    }
    public void UpdateSellInventorySlot(int _index)
    {
        // 인벤토리 슬롯 바꾸기 
        ResetSellInventory();
        SetActiveSellItemInfo(false);
        MoneyUpdate();
        if (_index == 0)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyWeaponItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyWeaponItems);
                shopInventorySlots[i].CurItem = InventoryManager.Instance.InventroyWeaponItems[i];
                shopInventorySlots[i].SlotSetting();
                shopInventorySlots[i].EnableItemCount(false);
            }
        }
        if (_index == 1)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyEquipmentItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyEquipmentItems);
                shopInventorySlots[i].CurItem = InventoryManager.Instance.InventroyEquipmentItems[i];
                shopInventorySlots[i].SlotSetting();
                shopInventorySlots[i].EnableItemCount(false);
            }
        }
        if (_index == 2)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyConsumableItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyConsumableItems);
                shopInventorySlots[i].CurItem = InventoryManager.Instance.InventroyConsumableItems[i];
                shopInventorySlots[i].SlotSetting();
            }
        }
        if (_index == 3)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyMiscellaneousItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyMiscellaneousItems);
                shopInventorySlots[i].CurItem = InventoryManager.Instance.InventroyMiscellaneousItems[i];
                shopInventorySlots[i].SlotSetting();

            }
        }
        if (_index == 4)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyDecorationItems.Count; i++)
            {
                InventoryManager.Instance.SortItemKeyInventory(InventoryManager.Instance.InventroyDecorationItems);
                shopInventorySlots[i].CurItem = InventoryManager.Instance.InventroyDecorationItems[i];
                shopInventorySlots[i].SlotSetting();
                shopInventorySlots[i].EnableItemCount(false);
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
        isSellItemSelect = false;
        SetActiveSellItemInfo(true);

        sellItemInfoText[0].text = selectSellItem.itemName;
        sellItemInfoText[1].text = KeyToItemType(selectSellItem.itemKey);
        sellItemInfoText[3].text = "판매 가격: " + selectSellItem.sellPrice.ToString();
        switch (selectSellItem.itemKey / 1000)
        {
            case 0:
                sellItemInfoText[2].text = "This is Hair";
                break;
            case 1:
                sellItemInfoText[2].text = "This is FaceHair";
                break;
            case 2:
                sellItemInfoText[2].text = "DefensivePower : " + selectSellItem.defensivePower;
                break;
            case 3:
                sellItemInfoText[2].text = "DefensivePower : " + selectSellItem.defensivePower;
                break;
            case 4:
                sellItemInfoText[2].text = "DefensivePower : " + selectSellItem.defensivePower;
                break;
            case 5:
                sellItemInfoText[2].text = "DefensivePower : " + selectSellItem.defensivePower;
                break;
            case 6:
                sellItemInfoText[2].text = "DefensivePower : " + selectSellItem.defensivePower;
                break;
            case 7:
                sellItemInfoText[2].text =
                    "PysicalDamage : " + selectSellItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectSellItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectSellItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectSellItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectSellItem).weaponType;
                break;
            case 8:
                sellItemInfoText[2].text =
                    "PysicalDamage : " + selectSellItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectSellItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectSellItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectSellItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectSellItem).weaponType +
                    "DefensivePower : " + selectSellItem.defensivePower;
                break;
            case 9:
                sellItemInfoText[2].text =
                    "PysicalDamage : " + selectSellItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectSellItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectSellItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectSellItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectSellItem).weaponType;
                break;
            case 10:
                sellItemInfoText[2].text =
                    "PysicalDamage : " + selectSellItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectSellItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectSellItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectSellItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectSellItem).weaponType;
                break;
            case 11:
                sellItemInfoText[2].text =
                    "Value : " + selectSellItem.value + "\n";
                break;
            case 12:
                sellItemInfoText[2].text =
                    "PysicalDamage : " + selectSellItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectSellItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectSellItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectSellItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectSellItem).weaponType;
                break;
        }
    }
    public void SetActiveSellItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        sellItemInfo.SetActive(_bool);
    }
    public void SetActiveSellPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public void SelectSlotSellItem(Item _item)
    {
        // 슬롯에 선택한 아이템 
        selectSellItem = _item;
        isSellItemSelect = true;
    }
    public void RegisterItem()
    {
        if(selectSellItem.itemKey / 1000 == 11 || selectSellItem.itemKey / 1000 == 12)
        {
            SetActiveSellItemAmount(true);
        }   
        else
        {
            
            AddSellMoney(selectSellItem.sellPrice);
            sellItemList.Add(InventoryManager.Instance.DiscardItem(selectSellItem));
            UpdateSellInventorySlot(selectInventoryIndex);
            UpdateSellSlot();
        }
    }
    public void SetActiveSellItemAmount(bool _bool)
    {
        sellItemAmount.SetActive(_bool);
    }
    public void RegisterAmountItem()
    {
        if (int.Parse(sellAmount.text) > selectSellItem.count)
            Debug.Log("수량 뛰어 넘음");
        else
        {
            AddSellMoney(selectSellItem.sellPrice * int.Parse(sellAmount.text));
            sellItemList.Add(InventoryManager.Instance.DiscardItem(selectSellItem, int.Parse(sellAmount.text)));
            UpdateSellInventorySlot(selectInventoryIndex);
            UpdateSellSlot();
            SetActiveSellItemAmount(false);
        }
    }
    public void AddSellMoney(int _money)
    {
        sellMoney += _money;
        sellMoneyText.text = sellMoney.ToString();
    }
    public void ResetSellList()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < shopInventorySlots.Length; i++)
        {
            sellSlots[i].SlotReset();
        }

    }
    public void UpdateSellSlot()
    {
        // 인벤토리 슬롯 바꾸기 
        ResetSellList();
        for (int i = 0; i < sellItemList.Count; i++)
        {
            sellSlots[i].CurItem = sellItemList[i];
            sellSlots[i].SlotSetting();
        }
    }
    public void SellItem()
    {
        playerStatus.Money += sellMoney;
        ResetSellList();
        MoneyUpdate();
        sellItemList.Clear();
        sellMoney = 0;
        sellMoneyText.text = sellMoney.ToString();
    }
    public void CancelAllRegisteredItem()
    {
        for(int i = 0; i < sellItemList.Count; i++)
        {
            InventoryManager.Instance.AcquireItem(sellItemList[i]);
        }
        sellItemList.Clear();
        sellMoney = 0;
        ResetSellList();
    }
    public void CancelRegisteredItem(Item _item)
    {
        InventoryManager.Instance.AcquireItem(_item);
        sellItemList.Remove(_item);
        AddSellMoney(-_item.sellPrice);
        UpdateSellSlot();
        UpdateSellInventorySlot(selectInventoryIndex);
    }
}

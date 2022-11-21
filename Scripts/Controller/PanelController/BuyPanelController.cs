using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPanelController : MonoBehaviour
{
    [Header("BuyingItemInfo")]
    [SerializeField] private GameObject shopItemInfo = null;
    [SerializeField] private GameObject equipedItemInfo = null;
    [SerializeField] private BuySlot[] buySlots = null;
    [SerializeField] private TextMeshProUGUI buyingItemName = null;
    [SerializeField] private TextMeshProUGUI buyingItemType = null;
    [SerializeField] private TextMeshProUGUI buyingItemExplain = null;
    [SerializeField] private TextMeshProUGUI buyingItemPrice = null;
    [SerializeField] private TMP_InputField buyAmount = null;
    [SerializeField] private TextMeshProUGUI moneyText = null;

    [Header("EquipedItemInfo")]
    [SerializeField] private TextMeshProUGUI equipedItemName = null;
    [SerializeField] private TextMeshProUGUI equipedItemType = null;
    [SerializeField] private TextMeshProUGUI equipedItemExplain = null;


    [Header("Character Information")]
    [SerializeField] private PlayerStatus playerStatus = null;
    [SerializeField] private EquipmentController playerEquipmentController = null;

    [Header("Current")]
    [SerializeField] private Item selectBuyingItem = null;
    [SerializeField] private int selectBuyShopPanelIndex = 0;


    private bool isBuyingItemSelect = false;

    private void Awake()
    {
        buySlots = this.GetComponentsInChildren<BuySlot>();
    }
    private void Update()
    {
        if (isBuyingItemSelect)
            UpdateBuyingItemInfo();
    }
    public void UpdateBuyingInventorySlotChange(int _index)
    {
        // 인벤토리 슬롯 바꾸기 
        SellSlotsReset();
        SetActiveShopItemInfo(false);
        SetActiveEquipedItemInfo(false);
        UpdateMoney();
        if (_index == 0)
        {
            for (int i = 0; i < ShopManager.Instance.shopInventroyWeaponItems.Count; i++)
            {
                ShopManager.Instance.SortItemKeyInventory(ShopManager.Instance.shopInventroyWeaponItems);
                buySlots[i].CurItem = ShopManager.Instance.shopInventroyWeaponItems[i];
                buySlots[i].SlotSetting();
            }
        }
        if (_index == 1)
        {
            for (int i = 0; i < ShopManager.Instance.shopInventroyEquipmentItems.Count; i++)
            {
                ShopManager.Instance.SortItemKeyInventory(ShopManager.Instance.shopInventroyEquipmentItems);
                buySlots[i].CurItem = ShopManager.Instance.shopInventroyEquipmentItems[i];
                buySlots[i].SlotSetting();
            }
        }
        if (_index == 2)
        {
            for (int i = 0; i < ShopManager.Instance.shopInventroyConsumableItems.Count; i++)
            {
                ShopManager.Instance.SortItemKeyInventory(ShopManager.Instance.shopInventroyConsumableItems);
                buySlots[i].CurItem = ShopManager.Instance.shopInventroyConsumableItems[i];
                buySlots[i].SlotSetting();
            }
        }
        if (_index == 3)
        {
            for (int i = 0; i < ShopManager.Instance.shopInventroyMiscellaneousItems.Count; i++)
            {
                ShopManager.Instance.SortItemKeyInventory(ShopManager.Instance.shopInventroyMiscellaneousItems);
                buySlots[i].CurItem = ShopManager.Instance.shopInventroyMiscellaneousItems[i];
                buySlots[i].SlotSetting();

            }
        }
        if (_index == 4)
        {
            for (int i = 0; i < ShopManager.Instance.shopInventroyDecorationItems.Count; i++)
            {
                ShopManager.Instance.SortItemKeyInventory(ShopManager.Instance.shopInventroyDecorationItems);
                buySlots[i].CurItem = ShopManager.Instance.shopInventroyDecorationItems[i];
                buySlots[i].SlotSetting();
            }
        }
        selectBuyShopPanelIndex = _index;
    }
    public string KeyToItemTypeText(int _key)
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
    public void UpdateBuyingItemInfo()
    {
        // 아이템 정보창 업데이트
        isBuyingItemSelect = false;
        if (selectBuyingItem.itemType == EItemType.Consumables || selectBuyingItem.itemType == EItemType.Miscellaneous)
        {
            buyAmount.gameObject.SetActive(true);
        }
        else
        {
            if (playerEquipmentController.CheckEquipItems[CastTo<int>.From(selectBuyingItem.itemType)])
            {
                SetActiveEquipedItemInfo(true);
                UpdateEquipedItemInfo();
            }
            buyAmount.gameObject.SetActive(false);
        }
        SetActiveShopItemInfo(true);


        buyingItemName.text = selectBuyingItem.itemKorName;
        buyingItemType.text = KeyToItemTypeText(selectBuyingItem.itemKey);
        buyingItemPrice.text = "구매 가격: " + selectBuyingItem.buyPrice;
        switch (selectBuyingItem.itemKey / 1000)
        {
            case 0:
                buyingItemExplain.text = "머리 장식품";
                break;
            case 1:
                buyingItemExplain.text = "안면 장식품";
                break;
            case 2:
                buyingItemExplain.text = "방어력: " + selectBuyingItem.defensivePower;
                break;
            case 3:
                buyingItemExplain.text = "방어력: " + selectBuyingItem.defensivePower;
                break;
            case 4:
                buyingItemExplain.text = "방어력: " + selectBuyingItem.defensivePower;
                break;
            case 5:
                buyingItemExplain.text = "방어력: " + selectBuyingItem.defensivePower;
                break;
            case 6:
                buyingItemExplain.text = "방어력: " + selectBuyingItem.defensivePower;
                break;
            case 7:
                buyingItemExplain.text =
                    "물리 공격력: " + selectBuyingItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectBuyingItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectBuyingItem).weaponType;
                break;
            case 8:
                buyingItemExplain.text =
                    "물리 공격력: " + selectBuyingItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectBuyingItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectBuyingItem).weaponType + "\n" +
                    "방어력: " + selectBuyingItem.defensivePower;
                break;
            case 9:
                buyingItemExplain.text =
                    "물리 공격력: " + selectBuyingItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectBuyingItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectBuyingItem).weaponType;
                break;
            case 10:
                buyingItemExplain.text =
                    "물리 공격력: " + selectBuyingItem.physicalDamage + "\n" +
                    "마법 공격력: " + selectBuyingItem.magicalDamage + "\n" +
                    "공격 범위: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "공격 거리: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "무기 종류: " + ((Weapon)selectBuyingItem).weaponType;
                break;
            case 11:
                buyingItemExplain.text =
                    "회복량 : " + selectBuyingItem.value + "\n";
                break;
            case 12:
                buyingItemExplain.text =
                     "이것은 퀘스트 아이템";
                break;
        }
    }
    public void UpdateEquipedItemInfo()
    {
        // 아이템 정보창 업데이트
        int _index = CastTo<int>.From(selectBuyingItem.itemType);
        equipedItemName.text = playerEquipmentController.EquipItems[_index].itemKorName;
        equipedItemType.text = KeyToItemTypeText(playerEquipmentController.EquipItems[_index].itemKey);
        switch (playerEquipmentController.EquipItems[_index].itemKey / 1000)
        {
            case 0:
                equipedItemExplain.text = "머리 장식품";
                break;
            case 1:
                equipedItemExplain.text = "안면 장식품";
                break;
            case 2:
                equipedItemExplain.text = "방어력: " + playerEquipmentController.EquipItems[_index].defensivePower;
                break;
            case 3:
                equipedItemExplain.text = "방어력: " + playerEquipmentController.EquipItems[_index].defensivePower;
                break;
            case 4:
                equipedItemExplain.text = "방어력: " + playerEquipmentController.EquipItems[_index].defensivePower;
                break;
            case 5:
                equipedItemExplain.text = "방어력: " + playerEquipmentController.EquipItems[_index].defensivePower;
                break;
            case 6:
                equipedItemExplain.text = "방어력: " + playerEquipmentController.EquipItems[_index].defensivePower;
                break;
            case 7:
                equipedItemExplain.text =
                    "물리 공격력: " + playerEquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "마법 공격력: " + playerEquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "공격 범위: " + (playerEquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "공격 거리: " + (playerEquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "무기 종류: " + (playerEquipmentController.EquipItems[_index]).weaponType;
                break;
            case 8:
                equipedItemExplain.text =
                    "물리 공격력: " + playerEquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "마법 공격력: " + playerEquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "공격 범위: " + (playerEquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "공격 거리: " + (playerEquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "무기 종류: " + (playerEquipmentController.EquipItems[_index]).weaponType + "\n" +
                    "방어력: " + playerEquipmentController.EquipItems[_index].defensivePower;
                break;
            case 9:
                equipedItemExplain.text =
                    "물리 공격력: " + playerEquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "마법 공격력: " + playerEquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "공격 범위: " + (playerEquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "공격 거리: " + (playerEquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "무기 종류: " + (playerEquipmentController.EquipItems[_index]).weaponType;
                break;
            case 10:
                equipedItemExplain.text =
                    "물리 공격력: " + playerEquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "마법 공격력: " + playerEquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "공격 범위: " + (playerEquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "공격 거리: " + (playerEquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "무기 종류: " + (playerEquipmentController.EquipItems[_index]).weaponType;
                break;
            case 11:
                equipedItemExplain.text =
                    "회복량 : " + selectBuyingItem.value + "\n";
                break;
            case 12:
                equipedItemExplain.text = "이것은 퀘스트 아이템";
                break;
        }
    }
    public void UpdateBuyMoneyText()
    {
        buyingItemPrice.text = "구매 가격: " + (selectBuyingItem.buyPrice * int.Parse(buyAmount.text)).ToString();
    }
    public void BuyItem()
    {
        int _amount = 1;
        if (selectBuyingItem.itemType == EItemType.Consumables || selectBuyingItem.itemType == EItemType.Miscellaneous)
        {
            _amount = int.Parse(buyAmount.text);
            buyAmount.text = "00";
        }
        selectBuyingItem.count = _amount;
        InventoryManager.Instance.AcquireItem(selectBuyingItem);
        playerStatus.Money -= selectBuyingItem.buyPrice * _amount;
        UpdateMoney();
    }
    public void UpdateMoney()
    {
        moneyText.text = playerStatus.Money.ToString("N0");
    }
    public void SellSlotsReset()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < buySlots.Length; i++)
        {
            buySlots[i].SlotReset();
        }
    }
    public void SetActiveShopItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        shopItemInfo.SetActive(_bool);
    }
    public void SetActiveEquipedItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        equipedItemInfo.SetActive(_bool);
    }
    public void SetActivebuyPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public void SelectSlotBuyItem(Item _item)
    {
        // 슬롯에 선택한 아이템 
        selectBuyingItem = _item;
        isBuyingItemSelect = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPanelController : MonoBehaviour
{
    [Header("ItemInfo")]
    [SerializeField]
    private GameObject shopItemInfo = null;
    [SerializeField]
    private GameObject equipedItemInfo = null;
    [SerializeField]
    private BuySlot[] buySlots = null;

    [Header("MoneyText")]
    [SerializeField]
    private TextMeshProUGUI moneyText = null;

    [SerializeField]
    private PlayerStatus playerStatus = null;
    [SerializeField]
    private Item selectBuyingItem = null;
    private int selectBuyShopPanelIndex = 0;
    private bool isBuyingItemSelect = false;
    [SerializeField]
    private TextMeshProUGUI[] buyingiteminfoText = null;
    [SerializeField]
    private TextMeshProUGUI[] equipediteminfoText = null;

    [SerializeField]
    private TMP_InputField buyAmount = null;

    private void Awake()
    {
        buySlots = this.GetComponentsInChildren<BuySlot>();
        buyingiteminfoText = shopItemInfo.GetComponentsInChildren<TextMeshProUGUI>();
        equipediteminfoText = equipedItemInfo.GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (isBuyingItemSelect)
            UpdateBuyingItemInfo();
    }
    public void UpdateBuyingInventorySlotChange(int _index)
    {
        // �κ��丮 ���� �ٲٱ� 
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
    public void UpdateBuyingItemInfo()
    {
        // ������ ����â ������Ʈ
        isBuyingItemSelect = false;
        if (selectBuyingItem.itemType == 9 || selectBuyingItem.itemType == 10)
        {
            buyAmount.gameObject.SetActive(true);
        }
        else
        {
            if (playerStatus.EquipmentController.CheckEquipItems[selectBuyingItem.itemType])
            {
                SetActiveEquipedItemInfo(true);
                UpdateEquipedItemInfo();
            }
            buyAmount.gameObject.SetActive(false);
        }
        SetActiveShopItemInfo(true);


        buyingiteminfoText[0].text = selectBuyingItem.itemName;
        buyingiteminfoText[1].text = KeyToItemTypeText(selectBuyingItem.itemKey);
        buyingiteminfoText[3].text = "���� ����: " + selectBuyingItem.buyPrice;
        switch (selectBuyingItem.itemKey / 1000)
        {
            case 0:
                buyingiteminfoText[2].text = "This is Hair";
                break;
            case 1:
                buyingiteminfoText[2].text = "This is FaceHair";
                break;
            case 2:
                buyingiteminfoText[2].text = "����: " + selectBuyingItem.defensivePower;
                break;
            case 3:
                buyingiteminfoText[2].text = "����: " + selectBuyingItem.defensivePower;
                break;
            case 4:
                buyingiteminfoText[2].text = "����: " + selectBuyingItem.defensivePower;
                break;
            case 5:
                buyingiteminfoText[2].text = "����: " + selectBuyingItem.defensivePower;
                break;
            case 6:
                buyingiteminfoText[2].text = "����: " + selectBuyingItem.defensivePower;
                break;
            case 7:
                buyingiteminfoText[2].text =
                    "���� ���ݷ�: " + selectBuyingItem.physicalDamage + "\n" +
                    "���� ���ݷ�: " + selectBuyingItem.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).weaponType;
                break;
            case 8:
                buyingiteminfoText[2].text =
                    "���� ���ݷ�: " + selectBuyingItem.physicalDamage + "\n" +
                    "���� ���ݷ�: " + selectBuyingItem.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).weaponType + "\n" +
                    "����: " + selectBuyingItem.defensivePower;
                break;
            case 9:
                buyingiteminfoText[2].text =
                    "���� ���ݷ�: " + selectBuyingItem.physicalDamage + "\n" +
                    "���� ���ݷ�: " + selectBuyingItem.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).weaponType;
                break;
            case 10:
                buyingiteminfoText[2].text =
                    "���� ���ݷ�: " + selectBuyingItem.physicalDamage + "\n" +
                    "���� ���ݷ�: " + selectBuyingItem.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)selectBuyingItem).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)selectBuyingItem).weaponType;
                break;
            case 11:
                buyingiteminfoText[2].text =
                    "ȸ���� : " + selectBuyingItem.value + "\n";
                break;
            case 12:
                buyingiteminfoText[2].text =
                     "�̰��� ����Ʈ ������";
                break;
        }
    }
    public void UpdateEquipedItemInfo()
    {
        // ������ ����â ������Ʈ
        int _index = selectBuyingItem.itemType;
        equipediteminfoText[0].text = playerStatus.EquipmentController.EquipItems[_index].itemName;
        equipediteminfoText[1].text = KeyToItemTypeText(playerStatus.EquipmentController.EquipItems[_index].itemKey);
        switch (playerStatus.EquipmentController.EquipItems[_index].itemKey / 1000)
        {
            case 0:
                equipediteminfoText[2].text = "This is Hair";
                break;
            case 1:
                equipediteminfoText[2].text = "This is FaceHair";
                break;
            case 2:
                equipediteminfoText[2].text = "����: " + playerStatus.EquipmentController.EquipItems[_index].defensivePower;
                break;
            case 3:
                equipediteminfoText[2].text = "����: " + playerStatus.EquipmentController.EquipItems[_index].defensivePower;
                break;
            case 4:
                equipediteminfoText[2].text = "����: " + playerStatus.EquipmentController.EquipItems[_index].defensivePower;
                break;
            case 5:
                equipediteminfoText[2].text = "����: " + playerStatus.EquipmentController.EquipItems[_index].defensivePower;
                break;
            case 6:
                equipediteminfoText[2].text = "����: " + playerStatus.EquipmentController.EquipItems[_index].defensivePower;
                break;
            case 7:
                equipediteminfoText[2].text =
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).weaponType;
                break;
            case 8:
                equipediteminfoText[2].text =
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).weaponType + "\n" +
                    "����: " + playerStatus.EquipmentController.EquipItems[_index].defensivePower;
                break;
            case 9:
                equipediteminfoText[2].text =
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).weaponType;
                break;
            case 10:
                equipediteminfoText[2].text =
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].physicalDamage + "\n" +
                    "���� ���ݷ�: " + playerStatus.EquipmentController.EquipItems[_index].magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)playerStatus.EquipmentController.EquipItems[_index]).weaponType;
                break;
            case 11:
                equipediteminfoText[2].text =
                    "ȸ���� : " + selectBuyingItem.value + "\n";
                break;
            case 12:
                equipediteminfoText[2].text = "�̰��� ����Ʈ ������";
                break;
        }
    }
    public void UpdateBuyMoneyText()
    {
        buyingiteminfoText[3].text = "���� ����: " + (selectBuyingItem.buyPrice * int.Parse(buyAmount.text)).ToString();
    }
    public void BuyItem()
    {
        int _amount = 1;
        if (selectBuyingItem.itemType == 9 || selectBuyingItem.itemType == 10)
        {
            _amount = int.Parse(buyAmount.text);
            buyAmount.text = "00";
        }    
        InventoryManager.Instance.AcquireItem(selectBuyingItem, _amount);
        playerStatus.Money -= selectBuyingItem.buyPrice * _amount;
        UpdateMoney();
    }
    public void UpdateMoney()
    {
        moneyText.text = playerStatus.Money.ToString("N0");
    }
    public void SellSlotsReset()
    {
        // �κ��丮 ���� ����
        for (int i = 0; i < buySlots.Length; i++)
        {
            buySlots[i].SlotReset();
        }
    }
    public void SetActiveShopItemInfo(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        shopItemInfo.SetActive(_bool);
    }
    public void SetActiveEquipedItemInfo(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        equipedItemInfo.SetActive(_bool);
    }
    public void SetActivebuyPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public void SelectSlotBuyItem(Item _item)
    {
        // ���Կ� ������ ������ 
        selectBuyingItem = _item;
        isBuyingItemSelect = true;
    }
}

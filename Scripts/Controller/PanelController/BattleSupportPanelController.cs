using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSupportPanelController : MonoBehaviour
{
    [SerializeField]
    private QuickSlot[] quickSlots = new QuickSlot[4];

    [Header("Player")]
    [SerializeField]
    private PlayerStatus player = null;
    [SerializeField]
    private EquipmentController playerEquipmentController = null;

    public void SetQuickSlots(int _index)
    {
        quickSlots[_index].SlotReset();
        quickSlots[_index].IsItemRegistered = true;
        quickSlots[_index].CurItem = player.QuickSlotItems[_index];
        quickSlots[_index].SlotSetting();
    }


    public void UseQuickSlotItem(Item _item, int _slotIndex)
    {
        // 아이템 사용
        if (_item.itemType == (int)ItemType.Consumables)
        {
            InventoryManager.Instance.UseItem(player, _item);

        }
        else if (_item.itemType < 9)
        {
            if (playerEquipmentController.EquipItems[_item.itemType] != _item)
                playerEquipmentController.ChangeEquipment(_item , 0);
            else
                Debug.Log("이미 장착 중인 아이템입니다.");
        }
        else
        {
            Debug.Log("사용하실 수 없는 아이템입니다.");
        }

        if (quickSlots[_slotIndex].CurItem.count <= 0)
            player.QuickSlotItems[_slotIndex] = null;
        quickSlots[_slotIndex].UpdateQuickSlotItem();
    }

}

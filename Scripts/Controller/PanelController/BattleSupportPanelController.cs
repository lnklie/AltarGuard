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


    private void Update()
    {
        if(player.CurHp / player.MaxHp * 1f <= 1.0f)
        {
            AutoUse();
        }
    }
    public void SetQuickSlots(int _index)
    {
        quickSlots[_index].ResetSlot();
        quickSlots[_index].IsItemRegistered = true;
        quickSlots[_index].CurItem = player.QuickSlotItems[_index];
        quickSlots[_index].SetSlot();
    }
    public void AutoUse()
    {
        for(int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].IsAutoUse)
            {
                quickSlots[i].UseItem();
            }
        }
    }
    public void UseQuickSlotItem(Item _item, int _slotIndex)
    {
        // ������ ���
        if (_item.itemType == (int)ItemType.Consumables)
        {
            InventoryManager.Instance.UseItem(player, _item);

        }
        else if (_item.itemType < 9)
        {
            if (!_item.isEquip)
            {
                if (playerEquipmentController.CheckEquipItems[_item.itemType])
                {
                    playerEquipmentController.TakeOffEquipment(playerEquipmentController.EquipItems[_item.itemType]);
                }
                playerEquipmentController.ChangeEquipment(_item);
                _item.equipCharNum = 0;
            }
            else
                Debug.Log("�̹� ���� ���� �������Դϴ�.");
        }
        else
        {
            Debug.Log("����Ͻ� �� ���� �������Դϴ�.");
        }

        if (quickSlots[_slotIndex].CurItem.count <= 0)
            player.QuickSlotItems[_slotIndex] = null;
        quickSlots[_slotIndex].UpdateQuickSlotItem();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleSupportPanelController : MonoBehaviour
{
    [SerializeField] private QuickSlot[] quickSlots = new QuickSlot[4];

    [Header("Player")]
    [SerializeField] private PlayerStatus player = null;
    [SerializeField] private EquipmentController playerEquipmentController = null;
    [SerializeField] private Image autoPlayTextImage = null;

    private void Update()
    {
        if(player.CurHp / player.TotalMaxHp * 1f <= 1.0f)
        {
            AutoUse();
        }
    }

    public void SetAutoPlay()
    {
        if(player.PlayerState != EPlayerState.AutoPlay)
        {
            player.IsAutoMode = true;
            player.PlayerState = EPlayerState.AutoPlay;
            autoPlayTextImage.color = Color.red;
        }
        else
        {
            player.IsPlayMode = true;
            player.PlayerState = EPlayerState.Play;
            autoPlayTextImage.color = Color.white;
        }
    }
    public void SetQuickSlots(int _index)
    {
        quickSlots[_index].InitSlot();
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
        // 아이템 사용
        if (_item.itemType == (int)ItemType.Consumables)
        {
            InventoryManager.Instance.UseItem(player, _item);

            if (quickSlots[_slotIndex].CurItem.count <= 0)
            {
                player.QuickSlotItems[_slotIndex] = null;
            }

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
                Debug.Log("이미 장착 중인 아이템입니다.");
        }
        else
        {
            Debug.Log("사용하실 수 없는 아이템입니다.");
        }

        quickSlots[_slotIndex].UpdateQuickSlotItem();
    }

}

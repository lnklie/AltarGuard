using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanelController : MonoBehaviour
{
    [Header("SellItemInfo")]
    [SerializeField]
    private GameObject sellItemInfo = null;

    [SerializeField]
    private SellSlot[] sellSlots = null;

    [SerializeField]
    private ShopInventorySlot[] shopInventorySlots = null;
    private Text[] sellItemInfoText = null;
    private void Awake()
    {
        sellItemInfoText = sellItemInfo.GetComponentsInChildren<Text>();
        sellSlots = this.GetComponentsInChildren<SellSlot>();
        shopInventorySlots = this.GetComponentsInChildren<ShopInventorySlot>();

    }
}

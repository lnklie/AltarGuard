using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    [SerializeField]
    private Image[] itemImages = null;
    [SerializeField]
    private Item curItem = null;
    private bool isItemChange = false;
    [SerializeField]
    private Sprite uiMask = null;
    #region Property
    public Image[] ItemImages
    {
        get { return itemImages; }
        set { itemImages = value; }
    }
    public Item CurItem
    {
        get { return curItem; }
        set { curItem = value; }
    }
    public bool IsItemChange
    {
        get { return isItemChange; }
        set { isItemChange = value; }
    }
    #endregion


    private void Awake()
    {
        itemImages = GetComponentsInChildren<Image>();
    }

    public void SlotReset()
    {
        // ½½·Ô ¸®¼Â
        curItem = null;
        ItemImages[1].sprite = uiMask;
    }
    public void SlotSetting()
    {
        // ½½·Ô ¼¼ÆÃ
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);

    }
    public void SelectSlotBuyingItem()
    {
        if (curItem != null)
            UIManager.Instance.SelectSlotBuyItem(curItem);
    }
}

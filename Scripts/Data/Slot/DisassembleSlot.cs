using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisassembleSlot : MonoBehaviour
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
        // 슬롯 리셋
        curItem = null;
        ItemImages[1].sprite = uiMask;
    }
    public void SlotSetting()
    {
        // 슬롯 세팅
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);

    }


    public void SelectDisassembleRegisteredSlotItem()
    {
        if (curItem != null)
            UIManager.Instance.SelectDisassembleRegisteredSlot(curItem);
        else
        {
            Debug.Log("어디고");
        }
    }
}

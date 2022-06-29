using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-07
 * 작성자 : Inklie
 * 파일명 : InventorySlot.cs
==============================
*/
public class InventorySlot : MonoBehaviour
{
    private Text itemCount = null;
    private ItemType itemType;

    [SerializeField]
    private Image[] itemImages = null;
    public Image[] ItemImages
    {
        get { return itemImages; }
        set { itemImages = value; }
    }
    [SerializeField]
    private Item curItem = null;
    public Item CurItem
    { 
        get { return curItem; }
        set { curItem = value; }
    }
    private bool isItemChange = false;
    public bool IsItemChange
    {
        get { return isItemChange; }
        set { isItemChange = value; }
    }

    [SerializeField]
    private Sprite[] itemTargetingSprites = null;
    [SerializeField]
    private Sprite uiMask = null;
    private void Awake()
    {
        itemImages = GetComponentsInChildren<Image>();
        itemCount = GetComponentInChildren<Text>();
    }

    public void SlotReset()
    {
        // 슬롯 리셋
        curItem = null;
        ItemImages[1].sprite = uiMask;
        ItemImages[2].sprite = uiMask;
        itemCount.text = "00";
        EnableItemCount(true);
    }
    public void SlotSetting()
    {
        // 슬롯 세팅
        itemType = (ItemType)(curItem.itemKey / 1000);
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(60f, 60f);
        switch (itemType)
        {
            case ItemType.Hair:
                break;
            case ItemType.FaceHair:
                break;
            case ItemType.Cloth:
                itemImages[1].rectTransform.sizeDelta = new Vector2(80f, 80f);
                break;
            case ItemType.Pant:
                itemImages[1].rectTransform.sizeDelta = new Vector2(40f, 40f);
                break;
            case ItemType.Helmet:
                break;
            case ItemType.Armor:
                itemImages[1].rectTransform.sizeDelta = new Vector2(80f, 80f);
                break;
            case ItemType.Back:
                itemImages[1].rectTransform.sizeDelta = new Vector2(20f, 60f);
                break;
            case ItemType.Weapon:
                itemImages[1].rectTransform.sizeDelta = new Vector2(40f, 40f);
                break;
            case ItemType.SubWeapon:
                itemImages[1].rectTransform.sizeDelta = new Vector2(80f, 80f);
                break;
        }
        ActiveItemCount();
    }
    private void ActiveItemCount()
    {
        // 소모품이나 쌓이는 아이템이면 count 텍스트를 활성화
        if (curItem.itemKey / 1000 < 9)
        {
            EnableItemCount(false);
        }
        else
        {
            EnableItemCount(true);
            UpdateItemCount();
        }
    }
    public void UpdateItemCount()
    {
        itemCount.text = curItem.count.ToString();
    }
    public void EnableItemCount(bool _bool)
    {
        itemCount.enabled = _bool;
    }
    public void SelectItem()
    {
        if(curItem != null)
            UIManager.Instance.SelectSlotItem(curItem);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
==============================
 * 최종수정일 : 2022-06-07
 * 작성자 : Inklie
 * 파일명 : InventorySlot.cs
==============================
*/
public class InventorySlot : MonoBehaviour
{
    private TextMeshProUGUI itemCount = null;
    [SerializeField]
    private Image[] itemImages = null;
    [SerializeField]
    private Item curItem = null;
    private bool isItemChange = false;
    [SerializeField]
    private Sprite uiMask = null;


    [SerializeField]
    private bool isItem = false;
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
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (isItem && curItem.isCoolTime)
        {
            itemImages[2].fillAmount = curItem.coolTime / curItem.maxCoolTime;
        }
    }
    public void SlotReset()
    {
        // 슬롯 리셋
        curItem = null;
        ItemImages[1].sprite = uiMask;
        itemCount.text = "00";
        itemImages[2].fillAmount = 0f;
        isItem = false;
        EnableItemCount(false);
    }
    public void SlotSetting()
    {
        // 슬롯 세팅
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
        isItem = true;
        SetItemCoolTime();
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
            UIManager.Instance.SelectSlotItem(curItem, this);
    }
    public void SetItemCoolTime()
    {
        if (isItem && !curItem.isCoolTime)
        {
            curItem.coolTime = curItem.maxCoolTime * 1f;
        }
    }
}

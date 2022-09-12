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
    [SerializeField] private Image itemImage = null;
    [SerializeField] private Image itemCoolTimeImage = null;
    [SerializeField] private Item curItem = null;
    [SerializeField] private Sprite uiMask = null;
    [SerializeField] private TextMeshProUGUI checkItemEquip = null;
    [SerializeField] private TextMeshProUGUI itemCount = null;
    [SerializeField] private bool isItem = false;
    private bool isItemChange = false;


    #region Property

    public Item CurItem { get { return curItem; } set { curItem = value; } }
    public bool IsItemChange { get { return isItemChange; } set { isItemChange = value; } }
    #endregion

    private void Update()
    {
        if (isItem && curItem.isCoolTime)
        {
            itemCoolTimeImage.gameObject.SetActive(true);
            itemCoolTimeImage.fillAmount = curItem.coolTime / curItem.maxCoolTime;
        }
        else
        {
            itemCoolTimeImage.gameObject.SetActive(false);
        }
    }
    public void SlotReset()
    {
        // 슬롯 리셋
        curItem = null;
        itemImage.sprite = uiMask;
        itemCount.text = "00";
        itemCoolTimeImage.fillAmount = 0f;
        isItem = false;
        EnableItemCount(false);
        checkItemEquip.gameObject.SetActive(false);
    }
    public void SlotSetting()
    {
        // 슬롯 세팅
        itemImage.sprite = curItem.singleSprite;
        itemImage.rectTransform.sizeDelta = new Vector2(100f, 100f);
        isItem = true;
        SetItemCoolTime();
        ActiveItemCount();
        ActiveCheckItemEquip();
    }
    public void ActiveCheckItemEquip()
    {
        if (curItem.isEquip)
        {
            checkItemEquip.gameObject.SetActive(true);
            if(curItem.equipCharNum == 0)
            {
                checkItemEquip.color = Color.white;
            }
            else if(curItem.equipCharNum == 1)
            {
                checkItemEquip.color = Color.red;
            }
            else if(curItem.equipCharNum == 2)
            {
                checkItemEquip.color = Color.blue;
            }
            else if (curItem.equipCharNum == 3)
            {
                checkItemEquip.color = Color.green;
            }
            else if (curItem.equipCharNum == 4)
            {
                checkItemEquip.color = Color.magenta;
            }
        }
        else
        {
            checkItemEquip.gameObject.SetActive(false);
        }
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

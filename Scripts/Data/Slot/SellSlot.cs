using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SellSlot : MonoBehaviour
{
    private Text itemCount = null;
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
        itemCount = GetComponentInChildren<Text>();
    }

    public void SlotReset()
    {
        // ���� ����
        curItem = null;
        ItemImages[1].sprite = uiMask;
        itemCount.text = "00";
        EnableItemCount(true);
    }
    public void SlotSetting()
    {
        // ���� ����
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);

        ActiveItemCount();
    }
    private void ActiveItemCount()
    {
        // �Ҹ�ǰ�̳� ���̴� �������̸� count �ؽ�Ʈ�� Ȱ��ȭ
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
}

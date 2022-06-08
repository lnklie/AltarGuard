using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * ���������� : 2022-06-07
 * �ۼ��� : Inklie
 * ���ϸ� : InventorySlot.cs
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
    private void Update()
    {
        if (curItem != null && isItemChange)
        {
            TargetingSprites();
        }
    }
    public void SlotReset()
    {
        // ���� ����
        curItem = null;
        ItemImages[1].sprite = uiMask;
        ItemImages[2].sprite = uiMask;
        itemCount.text = "00";
        EnableItemCount(true);
    }
    public void SlotSetting()
    {
        // ���� ����
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
    public void TargetingSprites()
    {
        // �ش� �������� �������� ĳ���� ���� �ٸ� ���� Ÿ���� �̹����� ǥ��

        if (curItem.isEquip)
        {
            itemImages[2].sprite = itemTargetingSprites[curItem.equipCharNum];
            Debug.Log("�����غ��� " + this.gameObject.name);
        }
        else
        {
            itemImages[2].sprite = uiMask;
            Debug.Log("������� ���� " + this.gameObject.name);
        }

        isItemChange = false;
    }
    public void EnableItemCount(bool _bool)
    {
        itemCount.enabled = _bool;
    }
    public void SelectItem()
    {
        UIManager.Instance.SelectSlotItem(curItem);
    }
}

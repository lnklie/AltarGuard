using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : EquipmentSlot.cs
==============================
*/
public class EquipmentSlot : MonoBehaviour
{
    private ItemType itemType = ItemType.Hair;

    private Image[] itemImages;
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

    private bool isEquipmentChange;
    public bool IsEquipmentChange
    {
        get { return isEquipmentChange; }
        set { isEquipmentChange = value; }
    }

    private void Awake()
    {
        itemImages = GetComponentsInChildren<Image>();
    }
    public void InitImageSize()
    {
        // �̹��� �ʱ�ȭ
        itemImages[1].rectTransform.sizeDelta = new Vector2(80f, 80f);
    }
    public void SlotSetting(Item _item)
    {
        // �̹��� ���� ũ�Ⱑ �ٸ��� ������ �ش� �̹����� ũ�⸦ ��������
        itemType = (ItemType)(_item.itemKey / 1000);
        InitImageSize();
        switch (itemType)
        {
            case ItemType.Hair:
                break;
            case ItemType.FaceHair:
                break;
            case ItemType.Cloth:
                itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
                break;
            case ItemType.Pant:
                itemImages[1].rectTransform.sizeDelta = new Vector2(60f, 60f);
                break;
            case ItemType.Helmet:
                break;
            case ItemType.Armor:
                itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
                break;
            case ItemType.Back:
                itemImages[1].rectTransform.sizeDelta = new Vector2(40f, 80f);
                break;
            case ItemType.Weapon:
                itemImages[1].rectTransform.sizeDelta = new Vector2(60f, 60f);
                break;
            case ItemType.SubWeapon:
                itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
                break;
        }
    }
    public void SelectItem()
    {
        if (curItem != null)
            UIManager.Instance.SelectSlotItem(curItem);
    }
}
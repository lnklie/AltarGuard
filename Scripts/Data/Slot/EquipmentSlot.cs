using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Item curItem = null;

    private EItemType itemType = EItemType.Hair;
    private Image[] itemImages = null;
    private bool isEquipmentChange = false;
    public Image[] ItemImages { get { return itemImages; } set { itemImages = value; } }
    public Item CurItem { get { return curItem; } set { curItem = value; } }
    public bool IsEquipmentChange { get { return isEquipmentChange; } set { isEquipmentChange = value; }}

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
        itemType = (EItemType)(_item.itemKey / 1000);
        InitImageSize();
        switch (itemType)
        {
            case EItemType.Hair:
                break;
            case EItemType.FaceHair:
                break;
            case EItemType.Cloth:
                itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
                break;
            case EItemType.Pant:
                itemImages[1].rectTransform.sizeDelta = new Vector2(60f, 60f);
                break;
            case EItemType.Helmet:
                break;
            case EItemType.Armor:
                itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
                break;
            case EItemType.Back:
                itemImages[1].rectTransform.sizeDelta = new Vector2(40f, 80f);
                break;
            case EItemType.Weapon:
                itemImages[1].rectTransform.sizeDelta = new Vector2(60f, 60f);
                break;
            case EItemType.SubWeapon:
                itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
                break;
        }
    }
    public void SelectItem()
    {
        if (curItem != null)
        {
            UIManager.Instance.SelectEquipmenttSlotItem(curItem);
        }
    }
}

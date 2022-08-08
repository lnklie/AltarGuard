using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject craftInfo = null;
    [SerializeField]
    private TextMeshProUGUI itemName = null;
    [SerializeField]
    private TextMeshProUGUI itemInfo = null;

    [SerializeField]
    private CraftRecipe selectCraftRecipe = null;

    [SerializeField]
    private GameObject needsItems = null;
    private Image[] needsItemImages = null;
    private TextMeshProUGUI[] needsItemTexts = null;
    private bool isSelected = false;
    public CraftRecipe SelectCraftRecipe
    {
        get { return selectCraftRecipe; }
        set { selectCraftRecipe = value; }
    }
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }
    private void Awake()
    {
        needsItemImages = needsItems.GetComponentsInChildren<Image>();
        needsItemTexts = needsItems.GetComponentsInChildren<TextMeshProUGUI>();
        //texts = craftInfo.GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (isSelected)
            UpdateCraftRecipe();

    }
    public void UpdateCraftRecipe()
    {
        Item _item = DatabaseManager.Instance.SelectItem(selectCraftRecipe.completeItemKey);
        itemName.text = _item.itemName;
        switch (_item.itemKey / 1000)
        {
            case 0:
                itemInfo.text = "This is Hair";
                break;
            case 1:
                itemInfo.text = "This is FaceHair";
                break;
            case 2:
                itemInfo.text = "����: " + _item.defensivePower;
                break;
            case 3:
                itemInfo.text = "����: " + _item.defensivePower;
                break;
            case 4:
                itemInfo.text = "����: " + _item.defensivePower;
                break;
            case 5:
                itemInfo.text = "����: " + _item.defensivePower;
                break;
            case 6:
                itemInfo.text = "����: " + _item.defensivePower;
                break;
            case 7:
                itemInfo.text =
                    "���� ���ݷ�: " + _item.physicalDamage + "\n" +
                    "���� ���ݷ�: " + _item.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)_item).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)_item).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)_item).weaponType;
                break;
            case 8:
                itemInfo.text =
                    "���� ���ݷ�: " + _item.physicalDamage + "\n" +
                    "���� ���ݷ�: " + _item.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)_item).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)_item).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)_item).weaponType + "\n" +
                    "����: " + _item.defensivePower;
                break;
            case 9:
                itemInfo.text =
                    "���� ���ݷ�: " + _item.physicalDamage + "\n" +
                    "���� ���ݷ�: " + _item.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)_item).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)_item).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)_item).weaponType;
                break;
            case 10:
                itemInfo.text =
                    "���� ���ݷ�: " + _item.physicalDamage + "\n" +
                    "���� ���ݷ�: " + _item.magicalDamage + "\n" +
                    "���� ����: " + ((Weapon)_item).atkRange + "\n" +
                    "���� �Ÿ�: " + ((Weapon)_item).atkDistance + "\n" +
                    "���� ����: " + ((Weapon)_item).weaponType;
                break;
            case 11:
                itemInfo.text =
                    "ȸ���� : " + _item.value + "\n";
                break;
            case 12:
                itemInfo.text = "�̰��� ����Ʈ ������";
                break;
        }

        SetNecessaryItem(selectCraftRecipe.necessaryItemKey1,0);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount1, 0);
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey2, 1);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount2, 1);
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey3, 2);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount3, 2);
        SetNecessaryItem(selectCraftRecipe.necessaryItemKey4, 3);
        SetNecessaryItemCount(selectCraftRecipe.necessaryItemCount4, 3);
    }

    public void SetNecessaryItem(int _itemKey, int _imageIndex)
    {
        if (_itemKey != -1)
        {
            needsItemImages[_imageIndex].sprite = DatabaseManager.Instance.SelectItem(_itemKey).singleSprite;
        }
    }
    public void SetNecessaryItemCount(int _itemCount, int _textIndex)
    {
        if (_itemCount != -1)
        {
            needsItemTexts[_textIndex].text = _itemCount.ToString();
        }
    }
    public void SetActiveCraftPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
}

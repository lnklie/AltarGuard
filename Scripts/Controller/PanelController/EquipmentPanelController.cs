using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipmentPanelController : MonoBehaviour
{
    [Header("Character EquipmentController")]
    [SerializeField] private EquipmentController[] characterEquipmentController = null;

    [Header("Slots")]
    [SerializeField] private EquipmentSlot[] equipmentSlots = null;

    [Header("Current")]
    [SerializeField] private int selectCharNum = 0;
    [SerializeField] private EquipmentController selectCharacterEqipment = null;
    [SerializeField] private AllyStatus selectCharStatus = null;
    [SerializeField] private Item selectItem = null;

    [Header("Default")]
    [SerializeField] private Sprite UIMask = null;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI equipmentCharacterNameText = null;
    [SerializeField] private GameObject ItemInfoPanel = null;
    [SerializeField] private TextMeshProUGUI equipmentItemNameText = null;
    [SerializeField] private TextMeshProUGUI equipmentItemTypeText = null;
    [SerializeField] private TextMeshProUGUI equipmentItemRankText = null;
    [SerializeField] private TextMeshProUGUI equipmentItemExplainText = null;
    [SerializeField] private Image[] equipmentItemSkillImages = null;
    [SerializeField] private Button[] equipmentItemSkillButtons = null;
    [SerializeField] private TextMeshProUGUI[] characterAllyTargetSettingTexts = null;
    private void Awake()
    {
        equipmentSlots = this.GetComponentsInChildren<EquipmentSlot>();
    }

    public void SelectCharacter(int _index)
    {
        selectCharacterEqipment = characterEquipmentController[_index];
        selectCharStatus = selectCharacterEqipment.GetComponent<AllyStatus>();
        InitCharacterAllyTargetButton();
        SetCharacterAllyTargetButton((int)selectCharStatus.AllyTargetIndex);
    }

    public void ChangeAllEquipmentImage()
    {
        // ���â �̹��� �ٲٱ�
        for (int i = 0; i < selectCharacterEqipment.EquipItems.Length; i++)
        {
            if (selectCharacterEqipment.CheckEquipItems[i])
            {
                SetEquipmentSlotImage(i);
            }
            else
            {
                InitEquipmentSlotImage(i);
            }
        }
    }
    public void SelectSlotItem(Item _item)
    {
        // ���Կ� ������ ������ 
        InitEquipedItemSkillIcon();
        selectItem = _item;
        SetEquipmentInfo();
    }
    public void SetEquipmentInfo()
    {
        ItemInfoPanel.SetActive(true);
        equipmentItemNameText.text = selectItem.itemKorName;
        equipmentItemRankText.text = IntRankToStringRank(selectItem.itemRank);
        equipmentItemTypeText.text = KeyToItemType(selectItem.itemType);
        equipmentItemExplainText.text = SetItemExplain(selectItem);
    }
    public string SetItemExplain(Item item)
    {
        string _explain = null;
        switch (item.itemKey / 1000)
        {
            case 0:
                _explain = "This is Hair";
                break;
            case 1:
                _explain = "This is FaceHair";
                break;
            case 2:
                _explain = "����: " + item.defensivePower;
                break;
            case 3:
                _explain = "����: " + item.defensivePower;
                break;
            case 4:
                _explain = "����: " + item.defensivePower;
                break;
            case 5:
                _explain = "����: " + item.defensivePower;
                break;
            case 6:
                _explain = "����: " + item.defensivePower;
                break;
            case 7:
                _explain = "����: " + item.defensivePower;
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                _explain =
                    "���� ���ݷ�: " + item.physicalDamage + "\n" +
                    "���� ���ݷ�: " + item.magicalDamage + "\n" +
                    "���� ����: " + item.atkRange + "\n" +
                    "���� �Ÿ�: " + item.atkDistance + "\n" +
                    "���� ����: " + item.weaponType + "\n";
                if (item.grace[0] != null)
                    _explain += "ù ��° ����: " + item.grace[0].explain;
                if (item.grace[1] != null)
                    _explain += "�� ��° ����: " + item.grace[1].explain;
                if (item.grace[2] != null)
                    _explain += "�� ��° ����: " + item.grace[2].explain;
                SetEquipedItemSkillIcon(item);
                break;
            case 13:
                _explain =
                    "ȸ���� : " + item.value + "\n";
                break;
            case 14:
                _explain = "�̰��� ����Ʈ ������";
                break;
        }
        return _explain;
    }
    public string IntRankToStringRank(int _Rank)
    {
        string _rank = null;
        switch ((EItemRank)_Rank)
        {
            case EItemRank.Common:
                _rank = "Common";
                break;
            case EItemRank.UnCommon:
                _rank = "UnCommon";
                break;
            case EItemRank.Rare:
                _rank = "Rare";
                break;
            case EItemRank.Unique:
                _rank = "Unique";
                break;
        }
        return _rank;
    }
    public string KeyToItemType(int _key)
    {
        // Ű�� ������ Ÿ������ ����
        string _itemtype = null;
        switch (_key / 1000)
        {
            case 0:
                _itemtype = "Hair";
                break;
            case 1:
                _itemtype = "FaceHair";
                break;
            case 2:
                _itemtype = "Cloth";
                break;
            case 3:
                _itemtype = "Pant";
                break;
            case 4:
                _itemtype = "Helmet";
                break;
            case 5:
                _itemtype = "Armor";
                break;
            case 6:
                _itemtype = "Back";
                break;
            case 7:
                _itemtype = "Sword";
                break;
            case 8:
                _itemtype = "Shield";
                break;
            case 9:
                _itemtype = "Bow";
                break;
            case 10:
                _itemtype = "Wand";
                break;
        }
        return _itemtype;
    }
    public void InitEquipedItemSkillIcon()
    {
        for (int i = 0; i < 3; i++)
        {

            equipmentItemSkillButtons[i].gameObject.SetActive(false); ;
            equipmentItemSkillImages[i].gameObject.SetActive(false);
            equipmentItemSkillImages[i].sprite = null;
        }
    }
    public void SetEquipedItemSkillIcon(Item _item)
    {
        for (int i = 0; i < _item.skills.Count; i++)
        {
            if (_item.skills[i] != null) 
            {
                equipmentItemSkillButtons[i].gameObject.SetActive(true);
                equipmentItemSkillImages[i].sprite = _item.skills[i].singleSprite;
                equipmentItemSkillImages[i].gameObject.SetActive(true);
            }
        }
    }
    public void SetCharacterAllyTargetButton(int _index)
    {
        characterAllyTargetSettingTexts[_index].color = Color.red;
    }
    public void InitCharacterAllyTargetButton()
    {
        for(int i =0;i < 4; i++)
            characterAllyTargetSettingTexts[i].color = Color.white;
    }
    public void InitEquipment()
    {
        // ���â ����
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].CurItem = null;
            equipmentSlots[i].ItemImages[1].sprite = UIMask;
        }
    }
    public void SetEquipmentSlotImage(int _index)
    {
        
        equipmentSlots[_index].CurItem = selectCharacterEqipment.EquipItems[_index];
        equipmentSlots[_index].ItemImages[1].sprite = selectCharacterEqipment.EquipItems[_index].singleSprite;
        equipmentSlots[_index].SlotSetting(selectCharacterEqipment.EquipItems[_index]);
        UpdateEquipmentName();
    }
    public void InitEquipmentSlotImage(int _index)
    {
        // ��� ���� �� ��� ���� ����
        equipmentSlots[_index].CurItem = null;
        equipmentSlots[_index].ItemImages[1].sprite = UIMask;
        equipmentSlots[_index].InitImageSize();
    }
    public void SelectCharacterInEquipment(List<EquipmentController> _charaterList, bool _isUp)
    {
        // ���â���� ĳ���� ����
        InitEquipment();
        if(ItemInfoPanel.activeInHierarchy)
            ItemInfoPanel.SetActive(false);
        if (_isUp)
        {
            if (selectCharNum >= _charaterList.Count - 1)
                selectCharNum = 0;
            else
                selectCharNum++;
        }
        else
        {
            if (selectCharNum <= 0)
                selectCharNum = _charaterList.Count - 1;
            else
                selectCharNum--;

        }
        selectCharacterEqipment = _charaterList[selectCharNum];
        selectCharStatus = _charaterList[selectCharNum].GetComponent<AllyStatus>();
        InitCharacterAllyTargetButton();
        SetCharacterAllyTargetButton((int)selectCharStatus.AllyTargetIndex);
        UpdateEquipmentName();
        ChangeAllEquipmentImage();
    }

    public void UpdateEquipmentName()
    {
        // ���â ĳ���� �̸� ����
        equipmentCharacterNameText.text = selectCharStatus.ObjectName.ToString();
    }
    public void ChangeAllyTargetSetting(int _index)
    {
        selectCharStatus.AllyTargetIndex = (EAllyTargetingSetUp)_index;
        InitCharacterAllyTargetButton();
        SetCharacterAllyTargetButton((int)selectCharStatus.AllyTargetIndex);
    }
    public void ActiveEquipmentPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);

        if (!_bool)
            ItemInfoPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EquipmentPanelController : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] equipmentSlots = null;
    [SerializeField] private int selectCharNum = 0;
    [SerializeField] private EquipmentController[] characterEquipmentController = null;
    [SerializeField] private EquipmentController selectCharacterEqipment = null;
    [SerializeField] private TextMeshProUGUI equipmentNameText = null;
    [SerializeField] private AllyStatus selectCharStatus = null;

    [Header("Default")]
    [SerializeField] private Sprite UIMask = null;
    private void Awake()
    {
        equipmentSlots = this.GetComponentsInChildren<EquipmentSlot>();
    }
    void Start()
    {
        selectCharacterEqipment = characterEquipmentController[0];
    }

    void Update()
    {
        
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
        UpdateEquipmentName();
        ChangeAllEquipmentImage();
    }

    public void UpdateEquipmentName()
    {
        // ���â ĳ���� �̸� ����
        equipmentNameText.text = selectCharStatus.ObjectName.ToString();
    }
    public void ActiveEquipmentPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);
    }
}

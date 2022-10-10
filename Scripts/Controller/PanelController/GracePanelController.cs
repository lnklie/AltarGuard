using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GracePanelController : MonoBehaviour
{
    [SerializeField] private List<GraceSlot> slots = new List<GraceSlot>();
    [SerializeField] private List<Button> slotButtons = new List<Button>();
    [SerializeField] private GameObject graceInfo = null;
    [SerializeField] private TextMeshProUGUI graceName = null;
    [SerializeField] private TextMeshProUGUI graceExplain = null;
    [SerializeField] private Button graceLearnButton = null;
    [SerializeField] private TextMeshProUGUI graceLearnButtonText = null;
    [SerializeField] private TextMeshProUGUI remainingPoint = null;

    private BigGrace selectGrace = null;

    public delegate bool CheckIsActiveGrace(int _key);
    public delegate void AquireGraceDel(int _key);
    private void Awake()
    {
        slots.AddRange(GetComponentsInChildren<GraceSlot>());       
        for(int i = 0; i < slots.Count; i++)
        {
            slotButtons.Add(slots[i].GetComponent<Button>());
            slots[i].Grace = DatabaseManager.Instance.warriorGraceList[i];
        }
    }

    //public void SetSlotGrace(int _egraceType)
    //{
    //    for (int i = 0; i < slots.Count; i++)
    //    {
    //        if(DatabaseManager.Instance.SelectGrace(_egraceType + i) != null)
    //        {
    //            slots[i].Grace = DatabaseManager.Instance.SelectBigGrace(_egraceType + i);
    //        }
    //        else
    //    }
    //}
    public void UpdateGracePoint(int _gracePoint)
    {
        remainingPoint.text = "잔여 포인트: " + _gracePoint;
    }

    public void SelectGrace(int _index, CheckIsActiveGrace _checkIsActiveGrace)
    {
        selectGrace = slots[_index].Grace;
        graceExplain.text = selectGrace.explain;
        graceName.text = selectGrace.graceName;
        if(_checkIsActiveGrace(selectGrace.graceKey))
        {
            graceLearnButton.interactable = false;
            graceLearnButtonText.text = "이미 받음";
        }
        else
        {
            graceLearnButton.interactable = true;
            graceLearnButtonText.text = "받기";
        }
        
    }
    public void ActiveGracePanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }

    public void ActiveGraceInfo(bool _bool)
    {
        graceInfo.SetActive(_bool);
    }
    public void AquireGrace(AquireGraceDel _aquireGrace)
    {
        _aquireGrace(selectGrace.graceKey);
    }
    public void UpdateSlots(CheckIsActiveGrace _checkIsActiveGrace)
    {
        for (int i = 0; i < slots.Count; i++)
        {

            if (_checkIsActiveGrace(slots[i].Grace.necessaryGraceKey) || slots[i].Grace.necessaryGraceKey == -1)
            {
                slotButtons[i].interactable = true;
            }
            else
                slotButtons[i].interactable = false;
        }
    }
}

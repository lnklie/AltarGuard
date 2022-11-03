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
    [SerializeField] private GraceManager graceManager = null;
    private BigGrace selectGrace = null;

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
    //            Debug.Log("해당 은총이 없습니다.");
    //    }
    //}
    public void UpdateGracePoint(int _gracePoint)
    {
        remainingPoint.text = "잔여 포인트: " + _gracePoint;
    }

    public void SelectGrace(int _index)
    {
        selectGrace = slots[_index].Grace;
        graceExplain.text = selectGrace.explain;
        graceName.text = selectGrace.bigGraceName;
        if(graceManager.CheckIsReceive(selectGrace.bigGraceKey))
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
    public void AquireGrace()
    {
        graceManager.AquireBigGrace(selectGrace.bigGraceKey);
        graceManager.ActiveGrace();
    }
    public void UpdateSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (graceManager.CheckIsReceive(slots[i].Grace.necessaryBigGraceKey) || slots[i].Grace.necessaryBigGraceKey == -1)
            {
                slotButtons[i].interactable = true;
            }
            else
                slotButtons[i].interactable = false;
        }
    }
}

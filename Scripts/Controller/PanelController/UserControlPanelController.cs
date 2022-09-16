using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControlPanelController : MonoBehaviour
{
    [SerializeField] private SkillSlot[] skillSlots = null;
    [SerializeField] private PlayerStatus playerStatus = null;
    [SerializeField] private SkillController skillController = null;
    private void Update()
    {
        if(playerStatus.TriggerEquipmentChange)
        {
            SetSkillSlot(skillController.Skills);
        }
    }
    public void InitSkillSlot() 
    {
        for (int i = 0; i < skillSlots.Length; i++)
        { 
            skillSlots[i].InitSlot();
        }
    }
    public void SetSkillSlot(List<Skill> _skills)
    {
        Debug.Log("이것의 길이는 " + skillSlots.Length);
        for(int i = 0; i < skillSlots.Length; i++)
        {
            if (_skills[i] != null)
            {
                skillSlots[i].InitSlot();
                skillSlots[i].SetSlot(_skills[i]);
            }
        }
    }
}

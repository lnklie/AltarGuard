using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfoPanelController : MonoBehaviour
{
    [SerializeField]
    private SkillInfoSlot[] skillInfoSlots = null;
    private void Awake()
    {
        skillInfoSlots = GetComponentsInChildren<SkillInfoSlot>();
    }

    public void ActiveSkillPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
    public void LearnSkill(SkillController _skillController, int _skillKey)
    {
        _skillController.AquireSkill(_skillKey);
        Debug.Log("���� ������ ��ų�� " + _skillController.GetPassiveSkill(_skillKey).skillName);
    }
    public void LevelUpSkill(SkillController _skillController, int _skillKey)
    {
        _skillController.LevelUpSkill(_skillKey);
        Debug.Log("���� ��ų, " + _skillController.GetPassiveSkill(_skillKey).skillName +  "�� ������ " + _skillController.GetPassiveSkill(_skillKey).skillLevel);
    }

    //public void UpdateSkillInfo(SkillController _skillController)
    //{ 
    //    for (int i = 0; i < skillInfoSlots.Length; i++)
    //    {
    //        if (skillInfoSlots[i] != null)
    //        {
    //            skillInfoSlots[i].SetAlterInfoLevel(skillLevel[i]);
    //        }
    //    }
    //}
}

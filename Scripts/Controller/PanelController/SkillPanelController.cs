using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkillPanelController : MonoBehaviour
{
    [SerializeField] private SkillController selectSkillController = null;
    [SerializeField] private CharacterStatus selectCharacter = null;
    [SerializeField] private SkillInfoSlot[] skillInfoSlots = null;
    [SerializeField] private TextMeshProUGUI characterName = null;
    [SerializeField] private TextMeshProUGUI characterLevel = null;
    [SerializeField] private int selectNum = 0;

    public void SelectCharacter(SkillController _equipmentController, CharacterStatus _characterStatus)
    {
        selectSkillController = _equipmentController;
        selectCharacter = _characterStatus;
    }

    public void SetSkillInfo()
    {

        if (selectSkillController.Skills.Count > 0)
        {
            for (int i = 0; i < selectSkillController.Skills.Count ; i++)
            {
                
                skillInfoSlots[i].gameObject.SetActive(true);
                skillInfoSlots[i].SetSkill(selectSkillController.Skills[i], selectCharacter);

            }
        }
    }
    public void InitSkillInfo()
    {
        for (int i = 0; i < 3; i++)
        {

            skillInfoSlots[i].gameObject.SetActive(false);
            skillInfoSlots[i].InitSkill();

        }
    }
    public void SelectCharacterInSkillController(List<SkillController> _charaterList, bool _isUp)
    {
        // 스테이터스 창 캐릭터 선택
        if (_isUp)
        {
            selectNum++;
            Debug.Log(selectNum + " 업");
            if (selectNum == 5)
                selectNum = 0;
        }
        else
        {
            Debug.Log("다운");
            selectNum--;
            if (selectNum < 0)
                selectNum = 4;
        }
        InitSkillInfo();
        SelectCharacter(_charaterList[selectNum], _charaterList[selectNum].GetComponent<AllyStatus>());
        characterName.text = selectCharacter.ObjectName;
        characterLevel.text = "LV." + selectCharacter.CurLevel.ToString();
        SetSkillInfo();
    }
    public void ActiveSkillPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
}

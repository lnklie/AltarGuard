using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelController : MonoBehaviour
{
    [SerializeField] private SkillController selectSkillController = null;
    [SerializeField] private CharacterStatus selectCharacter = null;
    [SerializeField] private SkillInfoSlot[] skillInfoSlots = null;
    private int selectNum = 0;

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
    
    public void SelectCharacterInSkillController(List<SkillController> _charaterList, bool _isUp)
    {
        // 스테이터스 창 캐릭터 선택
        if (_isUp)
        {
            selectNum++;
            if (selectNum == UIManager.Instance.GetMercenaryNum() + 1)
                selectNum = 0;
        }
        else
        {
            selectNum--;
            if (selectNum < 0)
                selectNum = UIManager.Instance.GetMercenaryNum();
        }
        selectCharacter = _charaterList[selectNum].GetComponent<AllyStatus>();
    }
    public void ActiveSkillPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }
}

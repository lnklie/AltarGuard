using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
==============================
 * 최종수정일 : 2022-06-08
 * 작성자 : Inklie
 * 파일명 : StatusPanelController.cs
==============================
*/
public class StatusPanelController : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private TextMeshProUGUI[] statusTexts = null;

    private int selectNum = 0;
    private AllyStatus selectAllyStatus = null;

    [SerializeField] private Button[] statusButtons = null;

    public void UpdateStatusText()
    {
        // 상태 텍스트 업데이트
        string[] status = {
            "체력: " + selectAllyStatus.MaxHp.ToString(),
            "마력: " + selectAllyStatus.MaxMp.ToString(),
            "물리 공격력: " + selectAllyStatus.PhysicalDamage.ToString(),
            "마법 공격력: " + selectAllyStatus.MagicalDamage.ToString(),
            "방어력: " + selectAllyStatus.DefensivePower.ToString(),
            "이동 속도: " + selectAllyStatus.Speed.ToString(),
            "공격 속도: " + selectAllyStatus.AtkSpeed.ToString(),
            "드랍율: " + selectAllyStatus.TotalDropProbability.ToString(),
            "아이템 희귀도: " + selectAllyStatus.TotalItemRarity.ToString(),
            selectAllyStatus.ObjectName.ToString(),
            "힘: " + selectAllyStatus.TotalStr.ToString(),
            "민첩: " + selectAllyStatus.TotalDex.ToString(),
            "지력: " + selectAllyStatus.TotalWiz.ToString(),
            "행운: " + selectAllyStatus.TotalLuck.ToString(),
            "잔여 포인트: " + selectAllyStatus.StatusPoint.ToString(),
            "LV: " + selectAllyStatus.CurLevel.ToString()
        };

        for (int i = 0; i < statusTexts.Length; i++)
        {
            statusTexts[i].text = status[i];
        }
        if (selectAllyStatus.StatusPoint > 0)
        {
            for (int i = 0; i < statusButtons.Length; i++)
                statusButtons[i].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < statusButtons.Length; i++)
                statusButtons[i].gameObject.SetActive(false);
        }
    }
    public void SetPlayer(PlayerStatus _player)
    {
        selectAllyStatus = _player.GetComponent<AllyStatus>();
    }
    public void SelectCharacterInStatus(List<EquipmentController> _charaterList,bool _isUp)
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
        selectAllyStatus = _charaterList[selectNum].GetComponent<AllyStatus>();
        UpdateStatusText();
    }
    public void StatusUp(int _index)
    {
        // 스텟 업
        selectAllyStatus.UpStatus(_index);
        selectAllyStatus.UpdateTotalAbility();
        UpdateStatusText();
        Debug.Log("지점 3");
    }
    public void ActiveStatusPanel(bool _bool)
    {
        // UI 활성화 
        this.gameObject.SetActive(_bool);
    }

}

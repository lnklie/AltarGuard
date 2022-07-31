using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * ���������� : 2022-06-08
 * �ۼ��� : Inklie
 * ���ϸ� : StatusPanelController.cs
==============================
*/
public class StatusPanelController : MonoBehaviour
{
    [Header("Status")]
    [SerializeField]
    private Text[] statusTexts = null;

    private int selectNum = 0;
    private AllyStatus selectAllyStatus = null;

    [SerializeField]
    private Button[] statusButtons = null;

    public void UpdateStatusText()
    {
        // ���� �ؽ�Ʈ ������Ʈ
        string[] status = {
            "HP : " + selectAllyStatus.MaxHp.ToString(),
            "MP : " + selectAllyStatus.MaxMp.ToString(),
            "Physical Damage : " + selectAllyStatus.PhysicalDamage.ToString(),
            "Magical Damage : " + selectAllyStatus.MagicalDamage.ToString(),
            "Defensive : " + selectAllyStatus.DefensivePower.ToString(),
            "Speed : " + selectAllyStatus.Speed.ToString(),
            "Attack Speed : " + selectAllyStatus.AtkSpeed.ToString(),
            "Drop Probability : " + selectAllyStatus.DropProbability.ToString(),
            "ItemRarity : " + selectAllyStatus.ItemRarity.ToString(),
            selectAllyStatus.ObjectName.ToString(),
            "Str : " + selectAllyStatus.TotalStr.ToString(),
            "Dex : " + selectAllyStatus.TotalDex.ToString(),
            "wiz : " + selectAllyStatus.TotalWiz.ToString(),
            "Luck : " + selectAllyStatus.TotalLuck.ToString(),
            "Point : " + selectAllyStatus.StatusPoint.ToString(),
            "Level : " + selectAllyStatus.CurLevel.ToString()
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
        // �������ͽ� â ĳ���� ����
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
        // ���� ��
        selectAllyStatus.UpStatus(_index);
        selectAllyStatus.UpdateAbility();
        UpdateStatusText();
        Debug.Log("���� 3");
    }
    public void ActiveStatusPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);
    }

}

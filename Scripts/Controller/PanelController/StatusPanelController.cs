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
    private CharacterStatus selectCharStatus = null;

    [SerializeField]
    private Button[] statusButtons = null;

    public void UpdateStatusText()
    {
        // ���� �ؽ�Ʈ ������Ʈ
        string[] status = {
            "HP : " + selectCharStatus.MaxHp.ToString(),
            "MP : " + selectCharStatus.MaxMp.ToString(),
            "Physical Damage : " + selectCharStatus.PhysicalDamage.ToString(),
            "Magical Damage : " + selectCharStatus.MagicalDamage.ToString(),
            "Defensive : " + selectCharStatus.DefensivePower.ToString(),
            "Speed : " + selectCharStatus.Speed.ToString(),
            "Attack Speed : " + selectCharStatus.AtkSpeed.ToString(),
            "Drop Probability : " + selectCharStatus.DropProbability.ToString(),
            "ItemRarity : " + selectCharStatus.ItemRarity.ToString(),
            selectCharStatus.ObjectName.ToString(),
            "Str : " + selectCharStatus.TotalStr.ToString(),
            "Dex : " + selectCharStatus.TotalDex.ToString(),
            "wiz : " + selectCharStatus.TotalWiz.ToString(),
            "Luck : " + selectCharStatus.TotalLuck.ToString(),
            "Point : " + selectCharStatus.StatusPoint.ToString(),
            "Level : " + selectCharStatus.CurLevel.ToString()
        };

        for (int i = 0; i < statusTexts.Length; i++)
        {
            statusTexts[i].text = status[i];
        }
        if (selectCharStatus.StatusPoint > 0)
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
        selectCharStatus = _player.GetComponent<CharacterStatus>();
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
        selectCharStatus = _charaterList[selectNum].GetComponent<CharacterStatus>();
        UpdateStatusText();
    }
    public void StatusUp(int _index)
    {
        // ���� ��
        selectCharStatus.UpStatus(_index);
        UpdateStatusText();
    }
    public void ActiveStatusPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);
    }

}

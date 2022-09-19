using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI[] statusTexts = null;

    private int selectNum = 0;
    private AllyStatus selectAllyStatus = null;

    [SerializeField] private Button[] statusButtons = null;

    public void UpdateStatusText()
    {
        // ���� �ؽ�Ʈ ������Ʈ
        string[] status = {
            "ü��: " + selectAllyStatus.MaxHp.ToString(),
            "����: " + selectAllyStatus.MaxMp.ToString(),
            "���� ���ݷ�: " + selectAllyStatus.PhysicalDamage.ToString(),
            "���� ���ݷ�: " + selectAllyStatus.MagicalDamage.ToString(),
            "����: " + selectAllyStatus.DefensivePower.ToString(),
            "�̵� �ӵ�: " + selectAllyStatus.Speed.ToString(),
            "���� �ӵ�: " + selectAllyStatus.AtkSpeed.ToString(),
            "�����: " + selectAllyStatus.TotalDropProbability.ToString(),
            "������ ��͵�: " + selectAllyStatus.TotalItemRarity.ToString(),
            selectAllyStatus.ObjectName.ToString(),
            "��: " + selectAllyStatus.TotalStr.ToString(),
            "��ø: " + selectAllyStatus.TotalDex.ToString(),
            "����: " + selectAllyStatus.TotalWiz.ToString(),
            "���: " + selectAllyStatus.TotalLuck.ToString(),
            "�ܿ� ����Ʈ: " + selectAllyStatus.StatusPoint.ToString(),
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
        selectAllyStatus.UpdateTotalAbility();
        UpdateStatusText();
        Debug.Log("���� 3");
    }
    public void ActiveStatusPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);
    }

}

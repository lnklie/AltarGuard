using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            "ü��: " + selectAllyStatus.TotalStatus[(int)EStatus.MaxHp].ToString(),
            "����: " + selectAllyStatus.TotalStatus[(int)EStatus.MaxMp].ToString(),
            "���� ���ݷ�: " + selectAllyStatus.TotalStatus[(int)EStatus.PhysicalDamage].ToString(),
            "���� ���ݷ�: " + selectAllyStatus.TotalStatus[(int)EStatus.MagicalDamage].ToString(),
            "����: " + selectAllyStatus.TotalStatus[(int)EStatus.DefensivePower].ToString(),
            "�̵� �ӵ�: " + selectAllyStatus.TotalStatus[(int)EStatus.Speed].ToString(),
            "���� �ӵ�: " + selectAllyStatus.TotalStatus[(int)EStatus.AttackSpeed].ToString(),
            "�����: " + selectAllyStatus.TotalStatus[(int)EStatus.DropProbability].ToString(),
            "������ ��͵�: " + selectAllyStatus.TotalStatus[(int)EStatus.ItemRarity].ToString(),
            selectAllyStatus.ObjectName.ToString(),
            "��: " + selectAllyStatus.TotalStatus[(int)EStatus.Str].ToString(),
            "��ø: " + selectAllyStatus.TotalStatus[(int)EStatus.Dex].ToString(),
            "����: " + selectAllyStatus.TotalStatus[(int)EStatus.Wiz].ToString(),
            "���: " + selectAllyStatus.TotalStatus[(int)EStatus.Luck].ToString(),
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
        //selectAllyStatus.UpdateTotalAbility();
        UpdateStatusText();
        Debug.Log("���� 3");
    }
    public void ActiveStatusPanel(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);
    }

}

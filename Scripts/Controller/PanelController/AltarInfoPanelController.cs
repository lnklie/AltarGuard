using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AltarInfoPanelController : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player = null;
    private AltarStatus altar = null;
    [SerializeField]
    private AltarInfoSlot[] altarInfoSlots = null;
    [SerializeField]
    private TextMeshProUGUI moneyText = null;
    private void Awake()
    {
        altarInfoSlots = GetComponentsInChildren<AltarInfoSlot>();
    }

    public void UpdateMoney()
    {
        moneyText.text = player.Money.ToString("N0");
    }

    public void UpdateAltarInfo()
    {
        int[] altarStatusLevel = { 
            altar.HpLevel, altar.DefensivePowerLevel,altar.BuffRangeLevel,
            altar.BuffDamageLevel, altar.BuffDefensivePowerLevel,
            altar.BuffSpeedLevel, altar.BuffHpRegenLevel };
        float[] altarStatusValue = {
            altar.HpLevel * 100, altar.DefensivePowerLevel * 10,altar.BuffRangeLevel * 1,
            altar.BuffDamageLevel * 10, altar.BuffDefensivePowerLevel * 10,
            altar.BuffSpeedLevel * 0.01f, altar.BuffHpRegenLevel * 10};
        for (int i =0; i < altarInfoSlots.Length; i++)
        {
            if(altarInfoSlots[i] != null)
            {
                altarInfoSlots[i].SetAlterInfoLevel(altarStatusLevel[i]);
                altarInfoSlots[i].SetAlterInfoValue(altarStatusValue[i]);
            }    
        }

    }
    public void UpgradeAltarStatus(int _index)
    {
        altar.UpgradeAltar((AltarAbility)_index);
        UpdateAltarInfo();
        altar.IsAltarStatusChange = true;
        UpdateMoney();
    }
    public void SetAltar(AltarStatus _altar)
    {
        altar = _altar;
    }
    public void ActiveAltarInfo(bool _bool)
    {
        // UI Ȱ��ȭ 
        this.gameObject.SetActive(_bool);
        UpdateMoney();

    }
}

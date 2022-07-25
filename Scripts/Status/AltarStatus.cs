using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * ���������� : 2022-06-10
 * �ۼ��� : Inklie
 * ���ϸ� : AltarStatus.cs
==============================
*/
public class AltarStatus : Status
{
    [SerializeField]
    private AltarState altarState = AltarState.Idle;

    [SerializeField]
    protected int hpLevel = 1;

    [SerializeField]
    private int defensivePowerLevel = 1;

    [SerializeField]
    private int buffRangeLevel = 1;
    [SerializeField]
    private int buffDamageLevel = 1;

    [SerializeField]
    private int buffDefensivePowerLevel = 1;

    [SerializeField]
    private int buffSpeedLevel = 1;

    [SerializeField]
    private int buffHpRegenLevel = 1;

    private bool isAltarStatusChange = false;


    #region Property

    public AltarState AltarState
    {
        get { return altarState; }
        set { altarState = value; }
    }
    public int HpLevel
    {
        get { return hpLevel; }
        set { hpLevel = value; }
    }
    public int DefensivePowerLevel
    {
        get { return defensivePowerLevel; }
        set { defensivePowerLevel = value; }
    }
    public int BuffRangeLevel
    {
        get { return buffRangeLevel; }
        set { buffRangeLevel = value; }
    }
    public int BuffDamageLevel
    {
        get { return buffDamageLevel; }
        set { buffDamageLevel = value; }
    }
    public int BuffDefensivePowerLevel
    {
        get { return buffDefensivePowerLevel; }
        set { buffDefensivePowerLevel = value; }
    }
    public int BuffSpeedLevel
    {
        get { return buffSpeedLevel; }
        set { buffSpeedLevel = value; }
    }
    public int BuffHpRegenLevel
    {
        get { return buffHpRegenLevel; }
        set { buffHpRegenLevel = value; }
    }
    public bool IsAltarStatusChange
    {
        get { return isAltarStatusChange; }
        set { isAltarStatusChange = value; }
    }
    #endregion


    public void UpgradeAltar(AltarAbility _altarAbility)
    {
        // ���� ���׷��̵�
        switch (_altarAbility)
        {
            case AltarAbility.Hp:
                hpLevel++;
                break;
            case AltarAbility.DefensivePower:
                defensivePowerLevel++;
                break;
            case AltarAbility.BuffRange:
                buffRangeLevel++;
                break;
            case AltarAbility.Buff_Damage:
                buffDamageLevel++;
                break;
            case AltarAbility.Buff_DefensivePower:
                buffDefensivePowerLevel++;
                break;
            case AltarAbility.Buff_Speed:
                buffSpeedLevel++;
                break;
            case AltarAbility.Buff_Healing:
                buffHpRegenLevel++;
                break;
        }
    }
}

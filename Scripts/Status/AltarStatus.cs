using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-10
 * 작성자 : Inklie
 * 파일명 : AltarStatus.cs
==============================
*/
public class AltarStatus : MonoBehaviour
{
    [SerializeField]
    private AltarState altarState = AltarState.Idle;
    public AltarState AltarState
    {
        get { return altarState; }
        set { altarState = value; }
    }

    [SerializeField]
    protected int curHp = 0;
    public int CurHp
    {
        get { return curHp; }
        set { curHp = value; }
    }

    [SerializeField]
    protected int maxHp = 0;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    protected int hpLevel = 1;
    public int HpLevel
    {
        get { return hpLevel; }
        set { hpLevel = value; }
    }

    [SerializeField]
    private int defensivePowerLevel = 1;
    public int DefensivePowerLevel
    {
        get { return defensivePowerLevel; }
        set { defensivePowerLevel = value; }
    }

    [SerializeField]
    private int buffRangeLevel = 1;
    public int BuffRangeLevel
    {
        get { return buffRangeLevel; }
        set { buffRangeLevel = value; }
    }
    [SerializeField]
    private int buffDamageLevel = 1;
    public int BuffDamageLevel
    {
        get { return buffDamageLevel; }
        set { buffDamageLevel = value; }
    }

    [SerializeField]
    private int buffDefensivePowerLevel = 1;
    public int BuffDefensivePowerLevel
    {
        get { return buffDefensivePowerLevel; }
        set { buffDefensivePowerLevel = value; }
    }

    [SerializeField]
    private int buffSpeedLevel = 1;
    public int BuffSpeedLevel
    {
        get { return buffSpeedLevel; }
        set { buffSpeedLevel = value; }
    }

    [SerializeField]
    private int buffHealingLevel = 1;
    public int BuffHealingLevel
    {
        get { return buffHealingLevel; }
        set { buffHealingLevel = value; }
    }

    private bool isAltarStatusChange = false;
    public bool IsAltarStatusChange
    {
        get { return isAltarStatusChange; }
        set { isAltarStatusChange = value; }
    }
    public void UpgradeAltar(AltarAbility _altarAbility)
    {
        // 제단 업그레이드
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
                buffHealingLevel++;
                break;
        }
    }
}

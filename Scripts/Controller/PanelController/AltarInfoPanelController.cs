using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AltarInfoPanelController : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player = null;
    [SerializeField]
    private AltarStatus altar = null;
    [SerializeField]
    private AltarInfoSlot[] altarInfoSlots = null;
    [SerializeField]
    private TextMeshProUGUI moneyText = null;
    private void Awake()
    {
        altarInfoSlots = GetComponentsInChildren<AltarInfoSlot>();
    }
    private void Start()
    {
        int _propertyLength = DatabaseManager.Instance.altarPropertyList.Count;
        for (int i = 0; i < _propertyLength; i++)
        {
            if (altarInfoSlots[i] != null)
            {
                altarInfoSlots[i].SetAlterProperty(DatabaseManager.Instance.altarPropertyList[i]);
            }
        }
    }
    public void UpdateAltarStatus(int _index)
    {
        switch(_index)
        {
            case 0:
                altar.Hp = altarInfoSlots[_index].PropertyValue;
                break;
            case 1:
                altar.BasicStatus[(int)EStatus.DefensivePower] = altarInfoSlots[_index].PropertyValue;
                break;
            case 2:
                altar.BuffRangeLevel = altarInfoSlots[_index].PropertyValue;
                break;
            case 3:
                altar.BuffDamageLevel = altarInfoSlots[_index].PropertyValue;
                break;
            case 4:
                altar.BuffDefensivePowerLevel = altarInfoSlots[_index].PropertyValue;
                break;
            case 5:
                altar.BuffSpeedLevel = altarInfoSlots[_index].PropertyValue;
                break;
            case 6:
                altar.BuffHpRegenLevel = altarInfoSlots[_index].PropertyValue;
                break;
        }
        
    }
    public void UpdateMoney()
    {
        moneyText.text = player.Money.ToString("N0");
    }

    public void UpdateAltarInfo()
    {
        for (int i =0; i < altarInfoSlots.Length; i++)
        {
            if(altarInfoSlots[i] != null)
            {
                altarInfoSlots[i].SetAltarInfoLevel();
                altarInfoSlots[i].SetAltarPropertyCurrentValue();
                altarInfoSlots[i].SetAltarPropertyNextValue();
                altarInfoSlots[i].SetAltarLevelUpNecessaryMoney();

                if (player.Money >= altarInfoSlots[i].GetNecessaryMoneyByLevel())
                {
                    altarInfoSlots[i].EnableButton(true);
                }
                else
                {
                    altarInfoSlots[i].EnableButton(false);
                }
            }    
        }

    }

    public void UpdateAltarInfo(int index)
    {
        if (altarInfoSlots[index] != null)
        {
            altarInfoSlots[index].SetAltarInfoLevel();
            altarInfoSlots[index].SetAltarPropertyCurrentValue();
            altarInfoSlots[index].SetAltarPropertyNextValue();
            altarInfoSlots[index].SetAltarLevelUpNecessaryMoney();
            
            if(player.Money >= altarInfoSlots[index].GetNecessaryMoneyByLevel())
            {
                altarInfoSlots[index].EnableButton(true);
            }
            else
            {
                altarInfoSlots[index].EnableButton(false);
            }
        }
    }
    
    public void UpgradeAltarStatus(int _index)
    {
        altarInfoSlots[_index].LevelUp();
        UpdateAltarInfo(_index);
        UpdateMoney();
        altar.TriggerAltarStatusChange = true;
        UpdateAltarStatus(_index);
    }
    //public void SetAltar(AltarStatus _altar)
    //{
    //    altar = _altar;
    //}
    public void SetActiveAltarInfo(bool _bool)
    {
        // UI È°¼ºÈ­ 

        this.gameObject.SetActive(_bool);
        altar.SetActiveBuffRange(_bool);
        UpdateMoney();

    }
}

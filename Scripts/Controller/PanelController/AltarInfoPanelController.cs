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
        altar.IsAltarStatusChange = true;
    }
    public void SetAltar(AltarStatus _altar)
    {
        altar = _altar;
    }
    public void ActiveAltarInfo(bool _bool)
    {
        // UI È°¼ºÈ­ 
        this.gameObject.SetActive(_bool);
        UpdateMoney();

    }
}

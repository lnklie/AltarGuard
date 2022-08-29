using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
public class AltarInfoSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI altarPropertyLevel = null;
    [SerializeField]
    private TextMeshProUGUI altarPropertyCurrentValue = null;
    [SerializeField]
    private TextMeshProUGUI altarPropertyNextValue = null;
    [SerializeField]
    private TextMeshProUGUI altarPropertyNecessaryMoney = null;
    [SerializeField]
    private Button levelUpButton = null;
    [SerializeField]
    private AltarProperty altarProperty = null;
    [SerializeField]
    private int propertyValue = 0;

    public int PropertyValue
    {
        get { return propertyValue; }
    }
    public void SetAlterProperty(AltarProperty _altarProperty)
    {
        altarProperty = _altarProperty;
    }
    public void LevelUp()
    {
        altarProperty.propertyLevel++;
    }
    public void EnableButton(bool _bool)
    {
        levelUpButton.interactable = _bool;
    }
    public void SetAltarInfoLevel()
    {
        altarPropertyLevel.text = "LV. " + altarProperty.propertyLevel.ToString();
    }
    public void SetAltarPropertyCurrentValue()
    {
        altarPropertyCurrentValue.text = "ÇöÀç ¼öÄ¡: " + GetCurrentValueByLevel().ToString();
        propertyValue = GetCurrentValueByLevel();
    }
    public void SetAltarPropertyNextValue()
    {
        altarPropertyNextValue.text = "´ÙÀ½ ¼öÄ¡: " + GetNextValueByLevel().ToString();
    }
    public void SetAltarLevelUpNecessaryMoney()
    {
        NumberFormatInfo numberFormat = new CultureInfo("ko-KR", false).NumberFormat;
        altarPropertyNecessaryMoney.text = GetNecessaryMoneyByLevel().ToString("c",numberFormat);
    }

    public int GetCurrentValueByLevel()
    {
        int _value = 0;
        switch(altarProperty.propertyLevel)
        {
            case int n when (n <= 10 && n > 0):
                _value = (altarProperty.rateOfValueIncreaseBySection_1) * n; 
                break;
            case int n when (n <= 20 && n > 10):
                _value = (altarProperty.rateOfValueIncreaseBySection_2) * n;
                break;
            case int n when (n <= 30 && n > 20):
                _value = (altarProperty.rateOfValueIncreaseBySection_3) * n;
                break;
            case int n when (n <= 40 && n > 30):
                _value = (altarProperty.rateOfValueIncreaseBySection_4) * n;
                break;
            case int n when (n <= 50 && n > 40):
                _value = (altarProperty.rateOfValueIncreaseBySection_5) * n;
                break;
            case int n when (n <= 60 && n > 50):
                _value = (altarProperty.rateOfValueIncreaseBySection_6) * n;
                break;
            case int n when (n <= 70 && n > 60):
                _value = (altarProperty.rateOfValueIncreaseBySection_7) * n;
                break;
            case int n when (n <= 80 && n > 70):
                _value = (altarProperty.rateOfValueIncreaseBySection_8) * n;
                break;
            case int n when (n <= 90 && n > 80):
                _value = (altarProperty.rateOfValueIncreaseBySection_9) * n;
                break;
            case int n when (n <= 100 && n > 90):
                _value = (altarProperty.rateOfValueIncreaseBySection_10) * n;
                break;
        }
        return _value;
    }
    public int GetNextValueByLevel()
    {
        int _value = 0;
        switch (altarProperty.propertyLevel + 1)
        {
            case int n when (n <= 10 && n > 1):
                _value = (altarProperty.rateOfValueIncreaseBySection_1) * n;
                break;
            case int n when (n <= 20 && n > 10):
                _value = (altarProperty.rateOfValueIncreaseBySection_2) * n;
                break;
            case int n when (n <= 30 && n > 20):
                _value = (altarProperty.rateOfValueIncreaseBySection_3) * n;
                break;
            case int n when (n <= 40 && n > 30):
                _value = (altarProperty.rateOfValueIncreaseBySection_4) * n;
                break;
            case int n when (n <= 50 && n > 40):
                _value = (altarProperty.rateOfValueIncreaseBySection_5) * n;
                break;
            case int n when (n <= 60 && n > 50):
                _value = (altarProperty.rateOfValueIncreaseBySection_6) * n;
                break;
            case int n when (n <= 70 && n > 60):
                _value = (altarProperty.rateOfValueIncreaseBySection_7) * n;
                break;
            case int n when (n <= 80 && n > 70):
                _value = (altarProperty.rateOfValueIncreaseBySection_8) * n;
                break;
            case int n when (n <= 90 && n > 80):
                _value = (altarProperty.rateOfValueIncreaseBySection_9) * n;
                break;
            case int n when (n <= 100 && n > 90):
                _value = (altarProperty.rateOfValueIncreaseBySection_10) * n;
                break;
        }
        return _value;
    }
    public int GetNecessaryMoneyByLevel()
    {
        int _value = 0;
        switch (altarProperty.propertyLevel)
        {
            case int n when (n <= 10 && n > 0):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_1) * n;
                break;
            case int n when (n <= 20 && n > 10):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_2) * n;
                break;
            case int n when (n <= 30 && n > 20):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_3) * n;
                break;
            case int n when (n <= 40 && n > 30):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_4) * n;
                break;
            case int n when (n <= 50 && n > 40):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_5) * n;
                break;
            case int n when (n <= 60 && n > 50):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_6) * n;
                break;
            case int n when (n <= 70 && n > 60):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_7) * n;
                break;
            case int n when (n <= 80 && n > 70):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_8) * n;
                break;
            case int n when (n <= 90 && n > 80):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_9) * n;
                break;
            case int n when (n <= 100 && n > 90):
                _value = (altarProperty.rateOfMoneyIncreaseBySection_10) * n;
                break;
        }
        return _value;
    }
    public void LevelUpProperty(int _index)
    {
        UIManager.Instance.LevelUpAltarProperty(_index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltarInfoSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] altarInfoTexts = null;

    private void Awake()
    {
        altarInfoTexts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void SetAlterInfoLevel(int _lv)
    {
        altarInfoTexts[2].text = "LV. " + _lv.ToString();
    }
    public void SetAlterInfoValue(float _value)
    {
        altarInfoTexts[3].text = "+ " + _value.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * ���������� : 2022-06-10
 * �ۼ��� : Inklie
 * ���ϸ� : AltarInfoSlot.cs
==============================
*/
public class AltarInfoSlot : MonoBehaviour
{
    [SerializeField]
    private Text[] altarInfoTexts = null;

    private void Awake()
    {
        altarInfoTexts = GetComponentsInChildren<Text>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillInfoSlot : MonoBehaviour
{
    [SerializeField]
    private Text[] skillInfoTexts = null;

    private void Awake()
    {
        skillInfoTexts = GetComponentsInChildren<Text>();
    }

    public void SetAlterInfoLevel(int _lv)
    {
        skillInfoTexts[2].text = "LV. " + _lv.ToString();
    }
    public void SetAlterInfoValue(float _value)
    {
        skillInfoTexts[3].text = "+ " + _value.ToString();
    }
}

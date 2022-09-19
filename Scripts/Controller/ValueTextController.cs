using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueTextController : MonoBehaviour
{
    [SerializeField] private ValueText[] valueTexts = null;
    [SerializeField] private int textIndex = 0;



    public void SetText(int _value, Color _color)
    {
        ValueText _damageText = valueTexts[textIndex];
        _damageText.gameObject.SetActive(true);
        _damageText.SetText(_value, _color);
        textIndex++;
        if (textIndex + 1 > valueTexts.Length)
            textIndex = 0;
    }
}

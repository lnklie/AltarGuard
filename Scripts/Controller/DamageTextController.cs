using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    [SerializeField]
    private DamageText[] damageTexts = null;
    [SerializeField]
    private int textIndex = 0;

    public void SetDamageText(int _damage)
    {
       
        DamageText _damageText = damageTexts[textIndex];
        _damageText.gameObject.SetActive(true);
        _damageText.SetDamageText(_damage);
        textIndex++;
        if (textIndex + 1 > damageTexts.Length)
            textIndex = 0;
    }
}

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
        Debug.Log("지점 1");
        _damageText.gameObject.SetActive(true);
        Debug.Log("지점 2");
        _damageText.SetDamageText(_damage);
        Debug.Log("지점 3");
        textIndex++;
        if (textIndex + 1 > damageTexts.Length)
            textIndex = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Exp.cs
==============================
*/
[System.Serializable]
public class Exp
{
    public int lv;
    public int exp;
    public Exp(object _lv, object _exp)
    {
        lv = (int)_lv;
        exp = (int)_exp;
    }
}

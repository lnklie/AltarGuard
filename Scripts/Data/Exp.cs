using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-09
 * �ۼ��� : Inklie
 * ���ϸ� : Exp.cs
==============================
*/
[System.Serializable]
public class Exp
{
    public int lv;
    public int exp;
    public Exp(int _lv, int _exp)
    {
        lv = _lv;
        exp = _exp;
    }
}

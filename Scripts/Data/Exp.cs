using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Exp.cs
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

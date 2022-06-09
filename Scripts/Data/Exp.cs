using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-09
 * 작성자 : Inklie
 * 파일명 : Exp.cs
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

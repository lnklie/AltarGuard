using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-10
 * 작성자 : Inklie
 * 파일명 : PlayerStatus.cs
==============================
*/
public class PlayerStatus : CharacterStatus
{
    [SerializeField]
    private CharacterStatus[] mercenarys = null;
    public CharacterStatus[] Mercenarys
    {
        get { return mercenarys; }
        set { mercenarys = value; }
    }

    [SerializeField]
    private AltarStatus altarStatus = null;
    public AltarStatus AltarStatus
    {
        get { return altarStatus; }
        set { altarStatus = value; }
    }
    private int stage = 1;
    public int Stage
    {
        get { return stage; }
        set { stage = value; }
    }
    private int money = 100000;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
}

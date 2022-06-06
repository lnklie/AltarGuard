using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : PlayerStatus.cs
==============================
*/
public class PlayerStatus : CharacterStatus
{
    private CharacterStatus[] mercenarys = null;
    public CharacterStatus[] Mercenarys
    {
        get { return mercenarys; }
        set { mercenarys = value; }
    }

    private int stage = 0;
    public int Stage
    {
        get { return stage; }
        set { stage = value; }
    }
}

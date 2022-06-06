using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Altar.cs
==============================
*/
public class Altar : Elements
{

    private AltarState altarState = AltarState.Idle;
    public AltarState AltarState
    {
        get { return altarState; }
        set { altarState = value; }
    }

    [SerializeField]
    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    [SerializeField]
    private float defensivePower = 0;
    public float DefensivePower
    {
        get { return defensivePower; }
        set { defensivePower = value; }
    }

    [SerializeField]
    private float buffRange = 1f;
    public float BuffRange
    {
        get { return buffRange; }
        set { buffRange = value; }
    }

    [SerializeField]
    private int buff_Damage = 10;
    public int Buff_Damage
    {
        get { return buff_Damage; }
        set { buff_Damage = value; }
    }

    [SerializeField]
    private int buff_DefensivePower = 10;
    public int Buff_DefensivePower
    {
        get { return buff_DefensivePower; }
        set { buff_DefensivePower = value; }
    }

    [SerializeField]
    private int buff_Speed = 1;
    public int Buff_Speed
    {
        get { return buff_Speed; }
        set { buff_Speed = value; }
    }

    [SerializeField]
    private int buff_Healing = 10;
    public int Buff_Healing
    {
        get { return buff_Healing; }
        set { buff_Healing = value; }
    }
}
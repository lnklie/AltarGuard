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
public class PlayerStatus : AllyStatus
{
    private RaycastHit2D[] itemSight = default;
    public RaycastHit2D[] ItemSight
    {
        get { return itemSight; }
        set { itemSight = value; }
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

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

    [SerializeField]
    private AltarStatus altarStatus = null;

    private int stage = 1;

    private int money = 100000;
    private bool isAutoMode = false;

    #region
    public bool IsAutoMode
    {
        get { return isAutoMode; }
        set { isAutoMode = value; }
    }
    public RaycastHit2D[] ItemSight
    {
        get { return itemSight; }
        set { itemSight = value; }
    }
    public AltarStatus AltarStatus
    {
        get { return altarStatus; }
        set { altarStatus = value; }
    }
    public int Stage
    {
        get { return stage; }
        set { stage = value; }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    #endregion
}

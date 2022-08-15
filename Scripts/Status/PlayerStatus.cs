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
    private Vector2 dir;
    private EPlayerState playerState = EPlayerState.Play;
    [SerializeField]
    private Item[] quickSlotItems = new Item[4];
    [SerializeField]
    private bool isQuickSlotRegister = false;
    #region Property
    public EPlayerState PlayerState
    {
        get { return playerState; }
        set { playerState = value; }
    }
    public Vector2 Dir
    {
        get { return dir; }
        set { dir = value; }

    }
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
    public Item[] QuickSlotItems
    {
        get { return quickSlotItems; }
        set { quickSlotItems = value; }
    }
    public bool IsQuickSlotRegister
    {
        get { return isQuickSlotRegister; }
        set { isQuickSlotRegister = value; }
    }
    #endregion
}

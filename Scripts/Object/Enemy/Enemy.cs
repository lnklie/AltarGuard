using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Enemy.cs
==============================
*/
public class Enemy : Elements
{
    private int damage = 0;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    
    private float seeRange;
    public float SeeRange
    {
        get { return seeRange; }
        set { seeRange = value; }
    }

    private float atkRange;
    public float AtkRange
    {
        get { return atkRange; }
        set { atkRange = value; }
    }

    private float speed = 0f;
    public float Speed
    {
        get { return speed; }
    }

    private float atkSpeed = 0f;
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }

    private int defeatExp = 0;
    public int DefeatExp
    { 
        get { return defeatExp; }
        set { defeatExp = value; }
    }

    private int[] itemDropKey = { 1, 2, 3, 4, 5 };
    public int[] ItemDropKey
    {
        get { return itemDropKey; }
        set { itemDropKey = value; }    
    }

    private float[] itemDropProb = { 20, 20, 20, 20, 20 };
    public float[] ItemDropProb
    {
        get { return itemDropProb; }
        set { itemDropProb = value; }
    }
}

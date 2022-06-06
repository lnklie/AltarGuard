using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : EnemyStatus.cs
==============================
*/
public class EnemyStatus : Status
{
    private int defeatExp = 0;
    public int DefeatExp
    {
        get { return defeatExp; }
        set { defeatExp = value; }
    }

    private int dmgCombo = 0;
    public int DmgCombo
    {
        get { return dmgCombo; }
        set { dmgCombo = value; }
    }

    float stiffenTime = 0f;
    public float StiffenTime
    {
        get { return stiffenTime; }
        set { stiffenTime = value; }
    }

    float maxStiffenTime = 0f;
    public float MaxStiffenTime
    {
        get { return maxStiffenTime; }
    }

    protected EnemyType enemyType;
    public EnemyType EnemyType
    {
        get { return enemyType; }
    }

    private EnemyState enemyState;
    public EnemyState EnemyState
    {
        get { return enemyState; }
        set { enemyState = value; }
    }

    private bool isKnuckBack = false;
    public bool IsKnuckBack
    {
        get { return isKnuckBack; }
        set { isKnuckBack = value; }
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

    private int damage = 0;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    protected float arrowSpd = 1f;
    public float ArrowSpd
    {
        get { return arrowSpd; }
        set { arrowSpd = value; }
    }
}

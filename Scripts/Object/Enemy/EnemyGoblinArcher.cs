using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : EnemyGoblinArcher.cs
==============================
*/
public class EnemyGoblinArcher : Enemy
{
    private float arrowSpd;
    public float ArrowSpd
    {
        get { return arrowSpd; }
        set { arrowSpd = value; }
    }
}

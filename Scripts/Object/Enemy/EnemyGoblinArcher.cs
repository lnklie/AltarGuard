using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : EnemyGoblinArcher.cs
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

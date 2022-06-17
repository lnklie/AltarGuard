using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemyStatus : EnemyStatus
{
    private SpriteRenderer spriteRenderer = null;

    public override void Awake()
    {
        base.Awake();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }
}

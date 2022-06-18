using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemyStatus : EnemyStatus
{
    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }
}

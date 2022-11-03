using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : CharacterController
{

    public override void Update()
    {
        base.Update();

        if (characterStatus.EnemyTarget)
        {
            destination = characterStatus.EnemyTarget.transform.position;
        }
        else
            destination = characterStatus.Flag.transform.position;
    }
}

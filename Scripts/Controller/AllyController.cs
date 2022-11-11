using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : CharacterController
{



    public override void Update()
    {
        base.Update();

        if (character.Target)
        {
            character.Distance = character.Target.transform.position - character.TargetPos.position;
            character.TargetDir = character.Distance.normalized;
            destination = character.Target.transform.position;
        }
        else
            destination = character.Flag.transform.position;
    }
}

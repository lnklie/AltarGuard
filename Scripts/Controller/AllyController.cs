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
            destination = character.Target.transform.position;
        }
        else
            destination = character.Flag.transform.position;
    }
}

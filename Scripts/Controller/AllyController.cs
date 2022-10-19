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
            Debug.Log("도착 지점 지정 " + characterStatus.ObjectName + " 그리고 마지막은 " + destination);
        }
        else
            destination = characterStatus.Flag.transform.position;
    }
}

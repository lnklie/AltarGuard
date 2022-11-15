using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryController : AllyController
{
    protected MercenaryStatus mercenary = null;

    public override void Awake()
    {
        base.Awake();
        mercenary = this.GetComponent<MercenaryStatus>();
    }

    public override void Update()
    {
        base.Update();
        AIChangeState();
        stateMachine.DoUpdateState();
    }
}

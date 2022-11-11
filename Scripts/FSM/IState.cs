using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void StateStart();
    public void StateUpdate();

    public void StateEnd();

}

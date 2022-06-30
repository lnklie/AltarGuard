using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryStatus : CharacterStatus
{
    [SerializeField]
    private int mercenaryNum = 0;
    public int MercenaryNum
    {
        get { return mercenaryNum; }
        set { mercenaryNum = value; }
    }
}

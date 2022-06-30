using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraceSlot : MonoBehaviour
{
    [SerializeField]
    private Grace grace = null;
    public Grace Grace
    {
        get { return grace; }
        set { grace = value; }
    }
}

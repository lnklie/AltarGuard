using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraceSlot : MonoBehaviour
{
    [SerializeField] private BigGrace grace = null;
    public BigGrace Grace { get { return grace; } set { grace = value; } }
}

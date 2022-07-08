using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBoxController : MonoBehaviour
{
    [SerializeField]
    private bool isTargeting;
    public bool IsTargeting
    {
        set { isTargeting = value; }
    }

    void Update()
    {
        if (isTargeting)
            ActiveBox(true);
        else
            ActiveBox(false);
    }

    public void ActiveBox(bool _bool)
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = _bool;       
    }
}

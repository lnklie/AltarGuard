using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private bool isSelect = false;
    public bool IsSelect { get { return isSelect; } set { isSelect = value; } }

    private void Update()
    {
        if(isSelect)
        {
            Vector2 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector2(Mathf.RoundToInt(_mousePoint.x), Mathf.RoundToInt(_mousePoint.y));
            if (_mousePoint.x > 12f || _mousePoint.x < -12f
                || _mousePoint.y > 12f || _mousePoint.y < -12f)
            {
                this.transform.position = new Vector2(0f, -4f);
            }
        }
    }
}

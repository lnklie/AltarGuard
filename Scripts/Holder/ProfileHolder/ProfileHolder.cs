using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * ���������� : 2022-06-08
 * �ۼ��� : Inklie
 * ���ϸ� : ProfileHolder.cs
==============================
*/
public class ProfileHolder : MonoBehaviour
{
    private Image curImage = null;
    public Image CurImage
    {
        get { return curImage; }
    }

    private void Awake()
    {
        curImage = this.GetComponent<Image>();
    }
}

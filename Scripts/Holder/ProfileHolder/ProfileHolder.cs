using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-08
 * 작성자 : Inklie
 * 파일명 : ProfileHolder.cs
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

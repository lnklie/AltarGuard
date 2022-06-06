using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : DropItem.cs
==============================
*/
public class DropItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;

    private int curItemKey;
    public int CurItemKey
    {
        get { return curItemKey; }
    }

    private bool isItem = false;
    public bool IsItem
    {
        get { return isItem; }
        set { isItem = value; }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item _item)
    {
        // 아이템 세팅
        curItemKey = _item.itemKey;

        if (spriteRenderer != null)
            spriteRenderer.sprite = _item.singleSprite;
        else
            Debug.Log("이미지 비어있음");
    }
}

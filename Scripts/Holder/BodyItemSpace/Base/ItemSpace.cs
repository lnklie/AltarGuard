using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : ItemSpace.cs
==============================
*/
public class ItemSpace : MonoBehaviour
{
    protected SpriteRenderer Sr = null;
    private void Awake()
    {
        Sr = this.GetComponent<SpriteRenderer>();
    }

    public void ChangeItemSprite(Sprite _itemSprite)
    {
        // 해당 오브젝트 스프라이트 변경
        Sr.sprite = _itemSprite;
    }
}

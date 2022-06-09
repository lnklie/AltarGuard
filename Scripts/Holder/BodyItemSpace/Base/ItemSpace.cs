using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : ItemSpace.cs
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
        // �ش� ������Ʈ ��������Ʈ ����
        Sr.sprite = _itemSprite;
    }
}

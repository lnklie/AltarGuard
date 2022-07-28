using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : DropManager.cs
==============================
*/
public class DropManager : SingletonManager<DropManager>
{
    private Queue<GameObject> dropObject = new Queue<GameObject>();

    [SerializeField]
    private List<float> amountItemProb = new List<float>();
    [SerializeField]
    private GameObject dropPrefab = null;
    private void Awake()
    {
        for(int i = 0; i < 50; i++ )
        {
            GameObject gameObject = Instantiate(dropPrefab, this.transform);
            dropObject.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void ReturnItem(GameObject _item)
    {
        // ������ ȸ��
        dropObject.Enqueue(_item);
        _item.SetActive(false);
    }

}

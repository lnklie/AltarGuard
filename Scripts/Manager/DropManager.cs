using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    private List<DropItem> dropObject = new List<DropItem>();
    [SerializeField] private int dropIndex = 49;
    [SerializeField] private GameObject dropPrefab = null;

    public static DropManager Instance = null;
    private void Awake()
    {
        Instance = this;
        for(int i = 0; i < 50; i++ )
        {
            GameObject gameObject = Instantiate(dropPrefab, this.transform);
            dropObject.Add(gameObject.GetComponent<DropItem>());
            gameObject.SetActive(false);
        }
    }
    public void DropItem(Item _item, Vector2 _point)
    {
        DropItem _dropItem = dropObject[dropIndex];
        dropIndex--;
        if (dropIndex == -1)
            dropIndex = 49;
        _dropItem.transform.position = new Vector2(_point.x + Random.value, _point.y + Random.value);
        _dropItem.gameObject.SetActive(true);
        _dropItem.SetItem(_item);
    }

    //public void ReturnItem(DropItem _item)
    //{
    //    // 아이템 회수

    //    _item.gameObject.SetActive(false);
    //}

}

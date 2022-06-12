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
    public void DropItem(Vector2 _enemyPos,List<int> itemDropKeys, List<float> itemDropProb)
    {
        // ������ ���
        int _ranAmount = RandomChoose(amountItemProb);
        if(_ranAmount > 0)
        {
            for (int i = 0; i < _ranAmount; i++)
            {
                int _ranIndex = RandomChoose(itemDropProb);
                DropItem _drops = dropObject.Dequeue().GetComponent<DropItem>();
                _drops.gameObject.SetActive(true);
                _drops.transform.position = new Vector2(
                Random.Range(_enemyPos.x + 0.5f, _enemyPos.x - 0.5f),
                Random.Range(_enemyPos.y + 0.5f, _enemyPos.y - 0.5f));
                _drops.SetItem(DatabaseManager.Instance.SelectItem(itemDropKeys[_ranIndex]));
            }
        }
    } 
    public void ReturnItem(GameObject _item)
    {
        // ������ ȸ��
        dropObject.Enqueue(_item);
        _item.SetActive(false);
    }
    private int RandomChoose(List<float> _probs)
    {
        // ������ ����
        float total = 0;

        foreach (float elem in _probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < _probs.Count; i++)
        {
            if (randomPoint < _probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= _probs[i];
            }
        }
        return _probs.Count - 1;
    }
}

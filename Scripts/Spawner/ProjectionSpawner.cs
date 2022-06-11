using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : ProjectionSpawner.cs
==============================
*/
public class ProjectionSpawner : SingletonManager<ProjectionSpawner>
{
    private Queue<GameObject> arrows = new Queue<GameObject>();
    public Queue<GameObject> Arrows
    {
        get { return arrows; }
        set { arrows = value; }
    }

    [SerializeField] 
    private GameObject arrowPrefab = null;

    private void Start()
    {
        for (int i = 0; i < 300; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, this.transform);
            arrows.Enqueue(arrow);
            arrow.SetActive(false);
        }
    }
    public void ShotArrow(Status _gameObject,int _damage)
    {
        GameObject shotArrow = Arrows.Dequeue();
        shotArrow.transform.position = _gameObject.gameObject.transform.position;
        Arrow arrow = shotArrow.GetComponent<Arrow>();
        arrow.Archer = _gameObject.gameObject;
        arrow.Dir = _gameObject.Dir;
        arrow.Spd = _gameObject.ArrowSpd;
        arrow.Dmg = _damage;
        arrow.gameObject.SetActive(true);
    }
    public int ArrowCount()
    {
        // ȭ�� �� ��ȯ
        return arrows.Count;
    }
    public void ReturnArrow(GameObject _arrow)
    {
        // ȭ�� ���ƿ���
        _arrow.SetActive(false);
        _arrow.GetComponent<Arrow>().InitArrow();
        arrows.Enqueue(_arrow);
        Debug.Log("ȭ�� ���ƿ��� " + ArrowCount());
    }
}

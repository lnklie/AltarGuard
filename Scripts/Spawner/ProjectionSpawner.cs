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
    public int ArrowCount()
    {
        // ȭ�� �� ��ȯ
        return arrows.Count;
    }
    public void ReturnArrow(GameObject _arrow)
    {
        // ȭ�� ���ƿ���
        _arrow.SetActive(false);
        arrows.Enqueue(_arrow);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : ProjectionSpawner.cs
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
        // 화살 수 반환
        return arrows.Count;
    }
    public void ReturnArrow(GameObject _arrow)
    {
        // 화살 돌아오기
        _arrow.SetActive(false);
        arrows.Enqueue(_arrow);
    }
}

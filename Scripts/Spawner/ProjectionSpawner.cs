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
    private Queue<Arrow> arrows = new Queue<Arrow>();
    [SerializeField] 
    private Arrow arrowPrefab = null;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Arrow arrow = Instantiate(arrowPrefab, this.transform);
            arrows.Enqueue(arrow);
            arrow.gameObject.SetActive(false);
        }
    }
    public void ShotArrow(CharacterStatus _status,int _damage)
    {
        Arrow arrow = arrows.Dequeue();
        arrow.gameObject.SetActive(true);
        arrow.gameObject.transform.position = _status.gameObject.transform.position;
        arrow.Archer = _status;
        arrow.Dir = _status.TargetDir;
        arrow.Spd = _status.ArrowSpd;
        arrow.Dmg = _damage;
    }
    public int ArrowCount()
    {
        // 화살 수 반환
        return arrows.Count;
    }
    public void ReturnArrow(Arrow _arrow)
    {
        arrows.Enqueue(_arrow);
        _arrow.GetComponent<Arrow>().InitArrow();
        _arrow.transform.position = this.transform.position;
        _arrow.transform.parent = this.transform;
    }
}

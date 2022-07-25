using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : BaseController.cs
==============================
*/
public class BaseController : MonoBehaviour
{

    private Status status = null;
    public virtual void Awake()

    {
        status = GetComponent<Status>();

    }


    public void SetIsDamaged(bool _bool)
    {
        status.IsDamaged = _bool;
    }
    private Debuff debuff = Debuff.Not;
    public Debuff Debuff

    {
        get { return debuff; }
        set { debuff = value; }
    }


    public float GetDistance(Vector2 _start, Vector2 _end)
    {
        // 대상과의 거리 측정
        float x1 = _start.x;
        float y1 = _start.y;
        float x2 = _end.x;
        float y2 = _end.y;
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }
    public void SetState(CharacterStatus _status, EAIState _state)
    {
        _status.AIState = _state;
        _status.IsStateChange = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : BaseController.cs
==============================
*/
public class BaseController : MonoBehaviour
{
    private Debuff debuff = Debuff.Not;
    public Debuff Debuff
    {
        get { return debuff; }
        set { debuff = value; }
    }




    public int ReviseDamage(int _damage, int _depensivePower)
    {
        return Mathf.CeilToInt(_damage * (1f / (1 + _depensivePower)));
    }
    public float GetDistance(Vector2 _start, Vector2 _end)
    {
        // ������ �Ÿ� ����
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
}

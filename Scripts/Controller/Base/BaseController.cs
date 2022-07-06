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
public abstract class BaseController : MonoBehaviour
{
    private DamageText[] damageTexts;

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
    public void SetState(CharacterStatus _status, AIState _state)
    {
        _status.AIState = _state;
        _status.IsStateChange = true;
    }
    public void ActiveLayer(Animator _ani, LayerName layerName)
    {
        // 애니메이션 레이어 가중치 조절
        for (int i = 0; i < _ani.layerCount; i++)
        {
            _ani.SetLayerWeight(i, 0);
        }
        _ani.SetLayerWeight((int)layerName, 1);
    }
}

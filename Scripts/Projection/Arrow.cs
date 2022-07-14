using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Arrow.cs
==============================
*/
public class Arrow : MonoBehaviour
{
    private Rigidbody2D rig = null;
    private float maxDurationTime = 5f;
    private float durationTime = 0f;

    [SerializeField]
    private Vector2 dir = Vector2.zero;
    public Vector2 Dir
    {
        get { return dir; }
        set { dir = value; }
    }
    [SerializeField]
    private float spd = 0;
    public float Spd
    {
        get { return spd; }
        set { spd = value; }
    }
    [SerializeField]
    private int dmg =0;
    public int Dmg
    {
        get { return dmg; }
        set { dmg = value; }
    }
    [SerializeField]
    private GameObject archer = null;
    public GameObject Archer
    {
        get { return archer; }
        set { archer = value; }
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(archer!= null)
            Shot();
    }
    public void InitArrow()
    {
        this.gameObject.SetActive(false);
        dir = Vector2.zero;
        spd = 0f;
        dmg = 0;
        archer = null;
    }
    private void Shot()
    {
        // 화살 쏘기
        rig.velocity = dir * spd;
        durationTime += Time.deltaTime;
        AngleModification();
        RaycastHit2D ray = HitRay(archer);
        Debug.DrawRay(this.transform.position, dir);
        if(ray)
        {
            Status hitObject = ray.collider.transform.GetComponent<Status>();
            hitObject.CurHp -= ReviseDamage(dmg, hitObject.DefensivePower);
            hitObject.IsDamaged = true;
            if (archer.CompareTag("Mercenary"))
            {
                MercenaryController mercenary = archer.GetComponent<MercenaryController>();
                CharacterStatus mercenaryStatus = mercenary.GetComponent<CharacterStatus>();
                mercenaryStatus.IsAtk = true;
                if (mercenary.IsLastHit(ray.collider.GetComponent<EnemyStatus>(), mercenaryStatus))
                {
                    mercenaryStatus.CurExp += ray.collider.GetComponent<EnemyStatus>().DefeatExp;
                }
            }
            durationTime = 0f;
            ProjectionSpawner.Instance.ReturnArrow(this);

        }
        if (durationTime >= maxDurationTime)
        {
            ProjectionSpawner.Instance.ReturnArrow(this);
            durationTime = 0f;
        }
    }

    public int ReviseDamage(int _damage, int _depensivePower)
    {
        return Mathf.CeilToInt(_damage * (1 / (1.0f + _depensivePower)));
    }

    private RaycastHit2D HitRay(GameObject _archer)
    {
        // 레이를 쏘는 역할
        RaycastHit2D ray = default;
        if (_archer.layer == 3)
            ray = Physics2D.Raycast(this.transform.position, dir, 0.4f, LayerMask.GetMask("Ally"));
        else if (_archer.layer == 8)
            ray = Physics2D.Raycast(this.transform.position, dir, 0.4f, LayerMask.GetMask("Enemy"));
        return ray;
    }
    private void AngleModification()
    {
        // 각도 수정
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

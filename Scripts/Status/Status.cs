using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] protected string objectName = "";
    [SerializeField] protected int curHp = 0;
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int defensivePower = 0;
    [SerializeField] protected bool isDamaged = false;
    [SerializeField] protected int totalMaxHp = 0;
    [SerializeField] protected int totalDefensivePower = 0;
    [SerializeField] protected bool triggerStatusUpdate = false;
    protected BoxCollider2D col = null;
    protected Rigidbody2D rig = null;
    protected Animator ani = null;
    [SerializeField] protected Transform targetPos = null;
    [SerializeField] private ValueTextController damageTextController;
    [SerializeField] protected int defeatExp = 0;
    private SpriteRenderer bodySprites = null;
    #region Property
    public int TotalMaxHp { get { return totalMaxHp; } set { totalMaxHp = value; } }
    public int TotalDefensivePower { get { return totalDefensivePower; } set { totalDefensivePower = value; } }
    public bool TriggerStatusUpdate { get { return triggerStatusUpdate; } set { triggerStatusUpdate = value; } }
    public int DefeatExp { get { return defeatExp; } set { defeatExp = value; } }
    public Transform TargetPos { get { return targetPos; } }
    public string ObjectName { get { return objectName; } set { objectName = value; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int DefensivePower { get { return defensivePower; } set { defensivePower = value; } }
    public bool IsDamaged { get { return isDamaged; } set { isDamaged = value; } }
    //public bool TriggerStateChange { get { return triggerStateChange; } set { triggerStateChange = value; } }
    public BoxCollider2D Col { get { return col; } }
    public Rigidbody2D Rig { get { return rig; } }
    public Animator Ani { get { return ani; } }
    #endregion



    public virtual void Awake()
    {
        col = this.GetComponent<BoxCollider2D>();
        rig = this.GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();
        bodySprites = this.GetComponentInChildren<BodySpace>().GetComponent<SpriteRenderer>();
    }

    public void SetValueText(int _damage, Color _color)
    {
        damageTextController.SetText(_damage, _color);
    }

    public int ReviseDamage(int _damage, int _depensivePower)
    {
        return Mathf.CeilToInt(_damage * (1f / (1 + _depensivePower)));
    }
    public bool IsLastHit()
    {
        if (curHp <= 0f)
            return true;
        else
            return false;
    }
    public virtual void Damaged(int _damage)
    {
        curHp -= ReviseDamage(_damage, defensivePower);
        triggerStatusUpdate = true;
        StartCoroutine(Blink());
        SetValueText(ReviseDamage(_damage, defensivePower),Color.red);
    }
    public virtual void recovered(int _value)
    {
        curHp += _value;
        triggerStatusUpdate = true;
        SetValueText(_value, Color.green);
    }
    private IEnumerator Blink()
    {
        bodySprites.color = new Color(1f, 1f, 1f, 155 / 255f);
        yield return new WaitForSeconds(0.5f);
        bodySprites.color = new Color(1f, 1f, 1f, 1f);
    }
    public void ActiveLayer(LayerName layerName)
    {
        for (int i = 0; i < ani.layerCount; i++)
        {
            ani.SetLayerWeight(i, 0);
        }
        ani.SetLayerWeight((int)layerName, 1);
    }
    public float GetDistance(Vector2 _end)
    {
        // ������ �Ÿ� ���
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float x2 = _end.x;
        float y2 = _end.y;
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }
}

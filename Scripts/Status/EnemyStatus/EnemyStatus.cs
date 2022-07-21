using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : EnemyStatus.cs
==============================
*/
public class EnemyStatus : CharacterStatus
{
    protected RaycastHit2D[] altarRay = default;

    protected EnemyType enemyType;
    private bool isKnuckBack = false;
    protected List<int> itemDropKey = new List<int>();
    protected List<float> itemDropProb = new List<float>();
    protected bool isEnemyChange;

    [SerializeField]
    private TextMesh textMesh = null;

    #region Property
    public RaycastHit2D[] AltarRay
    {
        get { return altarRay; }
        set { altarRay = value; }
    }

    public EnemyType EnemyType
    {
        get { return enemyType; }
    }
    public bool IsKnuckBack
    {
        get { return isKnuckBack; }
        set { isKnuckBack = value; }
    }
    public List<int> ItemDropKey
    {
        get { return itemDropKey; }
        set { itemDropKey = value; }
    }
    public List<float> ItemDropProb
    {
        get { return itemDropProb; }
        set { itemDropProb = value; }
    }
    public bool IsEnemyChange
    {
        get { return isEnemyChange; }
        set { isEnemyChange = value; }
    }
    #endregion

    public override void Awake()
    {
        base.Awake();
        textMesh = this.gameObject.transform.parent.GetComponentInChildren<TextMesh>();
    }
    public override void Update()
    {
        base.Update();
    }

}

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


    protected RaycastHit2D[] allyRay = default;
    public RaycastHit2D[] AltarRay
    {
        get { return allyRay; }
        set { allyRay = value; }
    }

    [SerializeField]
    protected int defeatExp = 0;
    public int DefeatExp
    {
        get { return defeatExp; }
        set { defeatExp = value; }
    }



    protected EnemyType enemyType;
    public EnemyType EnemyType
    {
        get { return enemyType; }
    }


    private bool isKnuckBack = false;
    public bool IsKnuckBack
    {
        get { return isKnuckBack; }
        set { isKnuckBack = value; }
    }

    protected List<int> itemDropKey = new List<int>();
    public List<int> ItemDropKey
    {
        get { return itemDropKey; }
        set { itemDropKey = value; }
    }

    protected List<float> itemDropProb = new List<float>();
    public List<float> ItemDropProb
    {
        get { return itemDropProb; }
        set { itemDropProb = value; }
    }

    protected bool isEnemyChange;
    public bool IsEnemyChange
    {
        get { return isEnemyChange; }
        set { isEnemyChange = value; }
    }
    protected bool isAtk = false;
    public bool IsAtk
    {
        get { return isAtk; }
        set { isAtk = value; }
    }


    [SerializeField]
    private TextMesh textMesh = null;

    public override void Awake()
    {
        base.Awake();
        textMesh = this.GetComponentInChildren<TextMesh>();
    }
    public override void Update()
    {
        base.Update();
        textMesh.text = aiState.ToString();
    }
}

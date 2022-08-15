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
    protected RaycastHit2D altarRay = default;

    protected EnemyType enemyType;
    private bool isKnuckBack = false;
    protected List<int> itemDropKey = new List<int>();
    protected List<float> itemDropProb = new List<float>();
    protected bool isEnemyChange;

    [SerializeField]
    private TextMesh textMesh = null;
    [SerializeField]
    private int enemyIndex = -1;

    #region Property
    public int EnemyIndex
    {
        get { return enemyIndex; }
        set { enemyIndex = value; }
    }
    public RaycastHit2D AltarRay
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
    public Item[] DropItem(float _charTotalLuck)
    {
        // 아이템 드랍
        Item[] _dropItems = new Item[4];
        int[] _ranIndex = RandomChoose(itemDropProb, _charTotalLuck);

        for (int i = 0; i < _ranIndex.Length; i++)
        {
            Debug.Log("얻는 아이템은 " + DatabaseManager.Instance.SelectItem(itemDropKey[_ranIndex[i]]));
            _dropItems[i] = DatabaseManager.Instance.SelectItem(itemDropKey[_ranIndex[i]]);
        }

        return _dropItems;
    }
    private int[] RandomChoose(List<float> _probs, float _characterAquireProb)
    {
        // 무작위 선택
        int[] itemIndexs = new int[4];
        float _total = 0f;
        foreach (var itemProb in _probs)
        {
            _total += itemProb;
        }

        for (int i = 0; i < _probs.Count; i++)
        {
            float _prob = Random.Range(0f, _total);
            Debug.Log("랜덤 프로브는 " + _prob);
            if (_probs[i] + _characterAquireProb > _prob)
            {
                Debug.Log("i는 " + i);
                itemIndexs[i] = i;
            }
        }

        return itemIndexs;
    }
}

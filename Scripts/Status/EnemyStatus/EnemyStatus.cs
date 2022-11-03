using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : CharacterStatus
{
    protected RaycastHit2D altarRay = default;

    protected EnemyType enemyType;
    private bool isKnuckBack = false;
    protected List<int> itemDropKey = new List<int>();
    protected List<float> itemDropProb = new List<float>();
    protected bool isEnemyChange;

    [SerializeField] private int enemyIndex = -1;

    #region Property
    public int EnemyIndex { get { return enemyIndex; } set { enemyIndex = value; } }
    public RaycastHit2D AltarRay { get { return altarRay; } set { altarRay = value; } }

    public EnemyType EnemyType { get { return enemyType; } }
    public bool IsKnuckBack { get { return isKnuckBack; } set { isKnuckBack = value; } }
    public List<int> ItemDropKey { get { return itemDropKey; } set { itemDropKey = value; } }
    public List<float> ItemDropProb { get { return itemDropProb; } set { itemDropProb = value; } }
    public bool IsEnemyChange { get { return isEnemyChange; } set { isEnemyChange = value; } }
    #endregion
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
    }
    public bool[] RandomChoose(List<float> _probs, float _characterAquireProb)
    {
        // 무작위 선택
        bool[] itemIndexs = new bool[5];
        float _total = 0f;
        foreach (var itemProb in _probs)
        {
            _total += itemProb;
        }

        for (int i = 0; i < _probs.Count; i++)
        {
            float _prob = Random.Range(0f, 100f);
            if (_probs[i] + _characterAquireProb > _prob)
            {
                float _we = _probs[i] + _characterAquireProb;
                itemIndexs[i] = true;
            }
            else
            {
                itemIndexs[i] = false;
            }
        }

        return itemIndexs;
    }
}

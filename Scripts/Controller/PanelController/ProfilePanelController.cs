using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class ProfilePanelController : MonoBehaviour
{
    [Header("StateImages & StateTexts")]
    [SerializeField] private Image[] playerStateImages = null;
    [SerializeField] private Image bossStateImages = null;
    [SerializeField] private TextMeshProUGUI[] playerTexts = null;

    [SerializeField] private Image[] mercenaryStateImages = null;
    [SerializeField] private TextMeshProUGUI[] mercenaryTexts = null;
    [SerializeField] private TextMeshProUGUI[] bossTexts = null;

    [Header("ProfileImages")]
    [SerializeField] private GameObject bossProfile = null;
    [SerializeField] private List<GameObject> profiles = new List<GameObject>();

    [SerializeField] private Sprite UIMask;

    [SerializeField] private Conversation[] conversations = null;
    [SerializeField] private bool isconversationActive = true;
    [SerializeField] private bool triggerConversationDeActive = false;
    [SerializeField] private bool triggerConversationActive = false;
    public List<GameObject> Profiles { get { return profiles; } set { profiles = value; } }
    public bool IsconversationActive { get { return isconversationActive; } set { isconversationActive = value; } }

    private void Start()
    {
        if (isconversationActive)
        {
            triggerConversationActive = true;
        }
    }
    private void Update()
    {
        if(triggerConversationDeActive)
        {
            triggerConversationDeActive = false;
            isconversationActive = false;
            StartCoroutine(SetActiveConversation(false));
            StopAllCoroutines();
        }
        else if(triggerConversationActive)
        {
            triggerConversationActive = false;
            isconversationActive = true;
            StartCoroutine(SetActiveConversation(true));
            StartCoroutine(Conversation(DatabaseManager.Instance.SelectGameScript(0)));
        }
    }
    public void ChooseTriggerConversationActive(bool _bool)
    {
        if (_bool)
            triggerConversationActive = true;
        else
            triggerConversationDeActive = true;
    }
    public IEnumerator SetActiveConversation(bool _bool)
    {
        for(int i = 0; i < 4; i++)
        {
            if (_bool == false && conversations[i].CurLines != null)
            {
                conversations[i].StopTalk();
                yield return new WaitForSeconds(conversations[i].CurLines.scriptAniSpeed);
            }
            conversations[i].gameObject.SetActive(_bool);
        }
    }

    #region "보스 업데이트"
    public void SetBossProfile(bool _bool)
    {
        bossProfile.SetActive(_bool);
    }
    public void BossUpdate(BossEnemyStatus _bossEnemy)
    {
        bossTexts[0].text = _bossEnemy.CurHp.ToString() + " / " + _bossEnemy.MaxHp.ToString();
        bossTexts[1].text = _bossEnemy.ObjectName.ToString();

        bossStateImages.fillAmount = (float)_bossEnemy.CurHp / _bossEnemy.MaxHp;
    }
    #endregion

    #region "프로필 업데이트"
    public void UpdatePlayerProfile(PlayerStatus _player)
    {
        string[] infoText = new string[]
            {
            _player.CurHp.ToString() + " / " + _player.MaxHp.ToString(),
            _player.CurMp.ToString() + " / " + _player.MaxMp.ToString(),
            _player.CurExp.ToString() + " / " + _player.MaxExp.ToString(),
            "Lv. " + _player.CurLevel.ToString(),
        };
        float[] infoImage ={
            _player.CurHp / (float)_player.MaxHp ,
            _player.CurMp / (float)_player.MaxMp,
            _player.CurExp / (float)_player.MaxExp
        };
        for (int i = 0; i < 4; i++)
        {
            playerTexts[i].text = infoText[i];
        }
        for (int i = 0; i < 3; i++)
        {
            playerStateImages[i].fillAmount = infoImage[i];
        }
    }
    public void UpdateMercenaryProfile(CharacterStatus _mercenary, int _mercenaryNum)
    {
        string[] infoText = {
            _mercenary.CurHp.ToString() + " / " + _mercenary.MaxHp.ToString(),
            _mercenary.CurMp.ToString() + " / " + _mercenary.MaxMp.ToString(),
            _mercenary.CurExp.ToString() + " / " + _mercenary.MaxExp.ToString(),
            "Lv. " + _mercenary.CurLevel.ToString()};
        float[] infoImage = {
            _mercenary.CurHp / (float)_mercenary.MaxHp ,
            _mercenary.CurMp / (float)_mercenary.MaxMp,
            _mercenary.CurExp / (float)_mercenary.MaxExp
        };


        for (int j = 0; j < infoText.Length; j++)
        {
            mercenaryTexts[4 * _mercenaryNum + j].text = infoText[j];
        }
        for (int j = 0; j < infoImage.Length; j++)
        {
            mercenaryStateImages[3 * _mercenaryNum + j].fillAmount = infoImage[j];
        }

    }
    public IEnumerator Conversation(GameScript _gameScript)
    {
        if(isconversationActive)
            for (int i = 0; i < _gameScript.actorNums.Count; i++)
            {
                if (_gameScript.actorNums[i] != -1)
                {
                    conversations[_gameScript.actorNums[i]].CurLines = _gameScript.lines[i];
                    yield return new WaitForSeconds(conversations[_gameScript.actorNums[i]].CurLines.timeLag);
                    conversations[_gameScript.actorNums[i]].Talk();
                }
                    
            }
    }
    #endregion
    public void ActiveInventory()
    {
        // UI 활성화 
        this.gameObject.SetActive(true);

    }
    public void DeactiveInventory()
    {
        // UI 비활성화
        this.gameObject.SetActive(false);
    }
}

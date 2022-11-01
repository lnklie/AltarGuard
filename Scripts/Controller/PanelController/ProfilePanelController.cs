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



    [SerializeField] private Conversation[] conversations = null;
    [SerializeField] private bool isconversationActive = true;
    [SerializeField] private bool triggerConversationDeActive = false;
    [SerializeField] private bool triggerConversationActive = false;

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
            conversations[i].gameObject.SetActive(_bool);
            yield return null;
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

    #region "보스 업데이트"
    public void SetBossProfile(bool _bool)
    {
        bossProfile.SetActive(_bool);
    }
    public void BossUpdate(BossEnemyStatus _bossEnemy)
    {
        bossTexts[0].text = _bossEnemy.CurHp.ToString() + " / " + _bossEnemy.TotalStatus[(int)EStatus.MaxHp].ToString();
        bossTexts[1].text = _bossEnemy.ObjectName.ToString();

        bossStateImages.fillAmount = _bossEnemy.CurHp / _bossEnemy.TotalStatus[(int)EStatus.MaxHp];
    }
    #endregion

    #region "프로필 업데이트"
    public void UpdatePlayerProfile(PlayerStatus _player)
    {
        string[] infoText = new string[]
            {
            _player.CurHp.ToString() + " / " + _player.TotalStatus[(int)EStatus.MaxHp].ToString(),
            _player.CurMp.ToString() + " / " + _player.TotalStatus[(int)EStatus.MaxMp].ToString(),
            _player.CurExp.ToString() + " / " + _player.MaxExp.ToString(),
            "Lv. " + _player.CurLevel.ToString(),
        };
        float[] infoImageFillAmount ={
            _player.CurHp / _player.TotalStatus[(int)EStatus.MaxHp] ,
            _player.CurMp / _player.TotalStatus[(int)EStatus.MaxMp],
            _player.CurExp / (float)_player.MaxExp
        };
        for (int i = 0; i < 4; i++)
        {
            playerTexts[i].text = infoText[i];
        }
        for (int i = 0; i < 3; i++)
        {
            playerStateImages[i].fillAmount = infoImageFillAmount[i];
        }
    }
    public void UpdateMercenaryProfile(CharacterStatus _mercenary, int _mercenaryNum)
    {
        string[] infoText = {
            _mercenary.CurHp.ToString() + " / " + _mercenary.TotalStatus[(int)EStatus.MaxHp].ToString(),
            _mercenary.CurMp.ToString() + " / " + _mercenary.TotalStatus[(int)EStatus.MaxMp].ToString(),
            _mercenary.CurExp.ToString() + " / " + _mercenary.MaxExp.ToString(),
            "Lv. " + _mercenary.CurLevel.ToString()};
        float[] infoImage = {
            _mercenary.CurHp / _mercenary.TotalStatus[(int)EStatus.MaxHp] ,
            _mercenary.CurMp / _mercenary.TotalStatus[(int)EStatus.MaxMp],
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

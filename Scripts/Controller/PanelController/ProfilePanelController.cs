using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
==============================
 * 최종수정일 : 2022-06-08
 * 작성자 : Inklie
 * 파일명 : ProfilePanelController.cs
==============================
*/
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

    public List<GameObject> Profiles { get { return profiles; } set { profiles = value; } }



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
        if(_gameScript.actorNum0 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum0].Talk(_gameScript.script0, _gameScript.scriptSpeed0, _gameScript.scriptAniSpeed0));
        }
        yield return new WaitForSeconds(_gameScript.timeLag0);
        if (_gameScript.actorNum1 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum1].Talk(_gameScript.script1, _gameScript.scriptSpeed1, _gameScript.scriptAniSpeed1));
        }
        yield return new WaitForSeconds(_gameScript.timeLag1);
        if (_gameScript.actorNum2 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum2].Talk(_gameScript.script2, _gameScript.scriptSpeed2, _gameScript.scriptAniSpeed2));
        }
        yield return new WaitForSeconds(_gameScript.timeLag2);
        if (_gameScript.actorNum3 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum3].Talk(_gameScript.script3, _gameScript.scriptSpeed3, _gameScript.scriptAniSpeed3));
        }
        yield return new WaitForSeconds(_gameScript.timeLag3);
        if (_gameScript.actorNum4 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum4].Talk(_gameScript.script4, _gameScript.scriptSpeed4, _gameScript.scriptAniSpeed4));
        }
        yield return new WaitForSeconds(_gameScript.timeLag4);
        if (_gameScript.actorNum5 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum5].Talk(_gameScript.script5, _gameScript.scriptSpeed5, _gameScript.scriptAniSpeed5));
        }
        yield return new WaitForSeconds(_gameScript.timeLag5);
        if (_gameScript.actorNum6 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum6].Talk(_gameScript.script6, _gameScript.scriptSpeed6, _gameScript.scriptAniSpeed6));
        }
        yield return new WaitForSeconds(_gameScript.timeLag6);
        if (_gameScript.actorNum7 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum7].Talk(_gameScript.script7, _gameScript.scriptSpeed7, _gameScript.scriptAniSpeed7));
        }
        yield return new WaitForSeconds(_gameScript.timeLag7);
        if (_gameScript.actorNum8 != -1)
        {
            StartCoroutine(conversations[_gameScript.actorNum8].Talk(_gameScript.script8, _gameScript.scriptSpeed8, _gameScript.scriptAniSpeed8));
        }
        yield return new WaitForSeconds(_gameScript.timeLag8);
        if (_gameScript.actorNum9 != -1)
        {
            Debug.Log("요긱");
            StartCoroutine(conversations[_gameScript.actorNum9].Talk(_gameScript.script9, _gameScript.scriptSpeed9, _gameScript.scriptAniSpeed9));
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

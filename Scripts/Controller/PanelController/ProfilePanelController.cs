using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-08
 * 작성자 : Inklie
 * 파일명 : ProfilePanelController.cs
==============================
*/
public class ProfilePanelController : MonoBehaviour
{
    [Header("UIImages")]
    [SerializeField]
    private GameObject UIImages = null;

    [Header("StateImages & StateTexts")]
    [SerializeField]
    private Image[] playerStateImages = null;
    [SerializeField]
    private Image[] mercenaryStateImages = null;
    [SerializeField]
    private Image bossStateImages = null;
    [SerializeField]
    private Text[] playerTexts = null;
    [SerializeField]
    private Text[] mercenaryTexts = null;
    [SerializeField]
    private Text[] bossTexts = null;

    [Header("ProfileImages")]

    [SerializeField]
    private GameObject playerProfile = null;
    [SerializeField]
    private GameObject mercenaryAProfile = null;
    [SerializeField]
    private GameObject mercenaryBProfile = null;
    [SerializeField]
    private GameObject mercenaryCProfile = null;
    [SerializeField]
    private GameObject mercenaryDProfile = null;

    [SerializeField]
    private Image[] playerProfileImages = new Image[9];
    [SerializeField]
    private Image[] mercenaryAProfileImages = new Image[9];
    [SerializeField]
    private Image[] mercenaryBProfileImages = new Image[9];
    [SerializeField]
    private Image[] mercenaryCProfileImages = new Image[9];
    [SerializeField]
    private Image[] mercenaryDProfileImages = new Image[9];

    [SerializeField]
    private Sprite UIMask;
    private void Awake()
    {
        for(int i = 0; i <9; i++)
        {
            playerProfileImages[i] = playerProfile.GetComponentsInChildren<ProfileHolder>()[i].CurImage;

        }
        for (int i = 0; i < 9; i++)
            mercenaryAProfileImages[i] = playerProfile.GetComponentsInChildren<ProfileHolder>()[i].CurImage;
        for (int i = 0; i < 9; i++)
            mercenaryBProfileImages[i] = playerProfile.GetComponentsInChildren<ProfileHolder>()[i].CurImage;
        for (int i = 0; i < 9; i++)
            mercenaryCProfileImages[i] = playerProfile.GetComponentsInChildren<ProfileHolder>()[i].CurImage;
        for (int i = 0; i < 9; i++)
            mercenaryDProfileImages[i] = playerProfile.GetComponentsInChildren<ProfileHolder>()[i].CurImage;
    }
    #region "보스 업데이트"
    public void BossUpdate(EnemyStatus _bossEnemy)
    {
        bossTexts[0].text = _bossEnemy.CurHp.ToString() + " / " + _bossEnemy.MaxHp.ToString();
        bossTexts[1].text = _bossEnemy.ObjectName.ToString();

        bossStateImages.fillAmount = _bossEnemy.CurHp / _bossEnemy.MaxHp;
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
            _player.ObjectName
        };
        float[] infoImage ={
            _player.CurHp / _player.MaxHp ,
            _player.CurMp / _player.MaxMp,
            (float)_player.CurExp / _player.MaxExp
        };
        for (int i = 0; i < 5; i++)
        {
            playerTexts[i].text = infoText[i];
        }
        for (int i = 0; i < 3; i++)
        {
            playerStateImages[i].fillAmount = infoImage[i];
        }
    }
    public void UpdateMercenaryProfile(CharacterStatus[] _mercenary)
    {
        for (int i = 0; i < _mercenary.Length; i++)
        {
            string[] infoText = {
            _mercenary[i].CurHp.ToString() + " / " + _mercenary[i].MaxHp.ToString(),
            _mercenary[i].CurMp.ToString() + " / " + _mercenary[i].MaxMp.ToString(),
            _mercenary[i].CurExp.ToString() + " / " + _mercenary[i].MaxExp.ToString(),
            "Lv. " + _mercenary[i].CurLevel.ToString()};
            float[] infoImage = {
            _mercenary[i].CurHp / _mercenary[i].MaxHp ,
            _mercenary[i].CurMp / _mercenary[i].MaxMp,
            (float)_mercenary[i].CurExp / _mercenary[i].MaxExp
        };

            for (int j = 0; j < 4; j++)
            {
                mercenaryTexts[4 * i + j].text = infoText[j];
            }
            for (int j = 0; j < 3; j++)
            {
                mercenaryStateImages[3 * i + j].fillAmount = infoImage[j];
            }
        };
    }
    public void ChangePlayerUIItemImage(List<EquipmentController> _characterList)
    {
        // 플레이어 프로필 UI 변경하기
        // 머리
        if (_characterList[0].CheckEquipItems[0])
        {
            playerProfileImages[7].sprite = _characterList[0].EquipItems[0].spList[0];
        }
        else
            playerProfileImages[7].sprite = UIMask;

        // 얼굴장식
        if (_characterList[0].CheckEquipItems[1])
            playerProfileImages[6].sprite = _characterList[0].EquipItems[1].spList[0];
        else
            playerProfileImages[6].sprite = UIMask;
        // 위옷
        if (_characterList[0].CheckEquipItems[2])
        {
            playerProfileImages[4].sprite = _characterList[0].EquipItems[2].spList[0];
            playerProfileImages[2].sprite = _characterList[0].EquipItems[2].spList[1];
            playerProfileImages[0].sprite = _characterList[0].EquipItems[2].spList[2];
        }
        else
        {
            playerProfileImages[4].sprite = UIMask;
            playerProfileImages[2].sprite = UIMask;
            playerProfileImages[0].sprite = UIMask;
        }
        // 투구
        if (_characterList[0].CheckEquipItems[4])
            playerProfileImages[8].sprite = _characterList[0].EquipItems[4].spList[0];
        else
            playerProfileImages[8].sprite = UIMask;
        // 갑옷
        if (_characterList[0].CheckEquipItems[5])
        {
            playerProfileImages[5].sprite = _characterList[0].EquipItems[5].spList[0];
            playerProfileImages[3].sprite = _characterList[0].EquipItems[5].spList[1];
            playerProfileImages[1].sprite = _characterList[0].EquipItems[5].spList[2];
        }
        else
        {
            playerProfileImages[5].sprite = UIMask;
            playerProfileImages[3].sprite = UIMask;
            playerProfileImages[1].sprite = UIMask;
        }
    }
    public void ChangeMercenaryUIItemImage(List<EquipmentController> _characterList,int _index)
    {
        // 용병 프로필 UI 변경하기
        if (_index == 0)
        {
            // 머리
            if (_characterList[_index + 1].CheckEquipItems[0])
                mercenaryAProfileImages[7].sprite = _characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryAProfileImages[7].sprite = UIMask;
            // 얼굴장식
            if (_characterList[_index + 1].CheckEquipItems[1])
                mercenaryAProfileImages[6].sprite = _characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryAProfileImages[6].sprite = UIMask;
            // 위옷
            if (_characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryAProfileImages[4].sprite = _characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryAProfileImages[2].sprite = _characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryAProfileImages[0].sprite = _characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryAProfileImages[4].sprite = UIMask;
                mercenaryAProfileImages[2].sprite = UIMask;
                mercenaryAProfileImages[0].sprite = UIMask;
            }
            // 투구
            if (_characterList[_index + 1].CheckEquipItems[4])
                mercenaryAProfileImages[8].sprite = _characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryAProfileImages[8].sprite = UIMask;
            // 갑옷
            if (_characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryAProfileImages[5].sprite = _characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryAProfileImages[3].sprite = _characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryAProfileImages[1].sprite = _characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryAProfileImages[5].sprite = UIMask;
                mercenaryAProfileImages[3].sprite = UIMask;
                mercenaryAProfileImages[1].sprite = UIMask;
            }
        }
        else if (_index == 1)
        {
            // 머리
            if (_characterList[_index + 1].CheckEquipItems[0])
                mercenaryBProfileImages[7].sprite = _characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryBProfileImages[7].sprite = UIMask;
            // 얼굴장식
            if (_characterList[_index + 1].CheckEquipItems[1])
                mercenaryBProfileImages[6].sprite = _characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryBProfileImages[6].sprite = UIMask;
            // 위옷
            if (_characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryBProfileImages[4].sprite = _characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryBProfileImages[2].sprite = _characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryBProfileImages[0].sprite = _characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryBProfileImages[4].sprite = UIMask;
                mercenaryBProfileImages[2].sprite = UIMask;
                mercenaryBProfileImages[0].sprite = UIMask;
            }
            // 투구
            if (_characterList[_index + 1].CheckEquipItems[4])
                mercenaryBProfileImages[8].sprite = _characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryBProfileImages[8].sprite = UIMask;
            // 갑옷
            if (_characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryBProfileImages[5].sprite = _characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryBProfileImages[3].sprite = _characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryBProfileImages[1].sprite = _characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryBProfileImages[5].sprite = UIMask;
                mercenaryBProfileImages[3].sprite = UIMask;
                mercenaryBProfileImages[1].sprite = UIMask;
            }
        }
        if (_index == 2)
        {
            // 머리
            if (_characterList[_index + 1].CheckEquipItems[0])
                mercenaryBProfileImages[7].sprite = _characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryBProfileImages[7].sprite = UIMask;
            // 얼굴장식
            if (_characterList[_index + 1].CheckEquipItems[1])
                mercenaryBProfileImages[6].sprite = _characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryBProfileImages[6].sprite = UIMask;
            // 위옷
            if (_characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryBProfileImages[4].sprite = _characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryBProfileImages[2].sprite = _characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryBProfileImages[0].sprite = _characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryBProfileImages[4].sprite = UIMask;
                mercenaryBProfileImages[2].sprite = UIMask;
                mercenaryAProfileImages[0].sprite = UIMask;
            }
            // 투구
            if (_characterList[_index + 1].CheckEquipItems[4])
                mercenaryBProfileImages[8].sprite = _characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryBProfileImages[8].sprite = UIMask;
            // 갑옷
            if (_characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryBProfileImages[5].sprite = _characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryBProfileImages[3].sprite = _characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryBProfileImages[1].sprite = _characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryBProfileImages[5].sprite = UIMask;
                mercenaryBProfileImages[3].sprite = UIMask;
                mercenaryBProfileImages[1].sprite = UIMask;
            }
        }
        if (_index == 3)
        {
            // 머리
            if (_characterList[_index + 1].CheckEquipItems[0])
                mercenaryCProfileImages[7].sprite = _characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryCProfileImages[7].sprite = UIMask;
            // 얼굴장식
            if (_characterList[_index + 1].CheckEquipItems[1])
                mercenaryCProfileImages[6].sprite = _characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryCProfileImages[6].sprite = UIMask;
            // 위옷
            if (_characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryCProfileImages[4].sprite = _characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryCProfileImages[2].sprite = _characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryCProfileImages[0].sprite = _characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryCProfileImages[4].sprite = UIMask;
                mercenaryCProfileImages[2].sprite = UIMask;
                mercenaryCProfileImages[0].sprite = UIMask;
            }
            // 투구
            if (_characterList[_index + 1].CheckEquipItems[4])
                mercenaryCProfileImages[8].sprite = _characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryCProfileImages[8].sprite = UIMask;
            // 갑옷
            if (_characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryCProfileImages[5].sprite = _characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryCProfileImages[3].sprite = _characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryCProfileImages[1].sprite = _characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryCProfileImages[5].sprite = UIMask;
                mercenaryCProfileImages[3].sprite = UIMask;
                mercenaryCProfileImages[1].sprite = UIMask;
            }
        }
    }
    #endregion
    public void ActiveInventory()
    {
        // UI 활성화 
        UIImages.SetActive(true);

    }
    public void DeactiveInventory()
    {
        // UI 비활성화
        UIImages.SetActive(false);
    }
}

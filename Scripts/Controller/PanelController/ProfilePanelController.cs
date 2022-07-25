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
    [Header("StateImages & StateTexts")]
    [SerializeField]
    private Image[] playerStateImages = null;
    [SerializeField]
    private Image bossStateImages = null;
    [SerializeField]
    private Text[] playerTexts = null;

    [SerializeField]
    private Image[] mercenaryStateImages = null;
    [SerializeField]
    private Text[] mercenaryTexts = null;
    [SerializeField]
    private Text[] bossTexts = null;

    [Header("ProfileImages")]
    [SerializeField]
    private GameObject bossProfile = null;

    [SerializeField]
    private List<GameObject> profiles = new List<GameObject>();
    public List<GameObject> Profiles
    {
        get { return profiles; }
        set { profiles = value; }
    }

    private List<Image> playerProfileImages = new List<Image>();
    private List<Image> mercenaryAProfileImages = new List<Image>();
    private List<Image> mercenaryBProfileImages = new List<Image>();
    private List<Image> mercenaryCProfileImages = new List<Image>();
    private List<Image> mercenaryDProfileImages = new List<Image>();

    [SerializeField]
    private Sprite UIMask;
    private void Awake()
    {
        for(int i = 0; i <9; i++)
        {
            playerProfileImages.Add(profiles[0].GetComponentsInChildren<ProfileHolder>()[i].GetComponent<Image>());
            mercenaryAProfileImages.Add(profiles[1].GetComponentsInChildren<ProfileHolder>()[i].GetComponent<Image>());
            mercenaryBProfileImages.Add(profiles[2].GetComponentsInChildren<ProfileHolder>()[i].GetComponent<Image>());
            mercenaryCProfileImages.Add(profiles[3].GetComponentsInChildren<ProfileHolder>()[i].GetComponent<Image>());
            mercenaryDProfileImages.Add(profiles[4].GetComponentsInChildren<ProfileHolder>()[i].GetComponent<Image>());
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
            _player.ObjectName
        };
        float[] infoImage ={
            _player.CurHp / (float)_player.MaxHp ,
            _player.CurMp / (float)_player.MaxMp,
            _player.CurExp / (float)_player.MaxExp
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
    public void ChangePlayerUIItemImage(List<EquipmentController> _characterList)
    {
        // 플레이어 프로필 UI 변경하기
        // 머리
        if (_characterList[0].CheckEquipItems[0])
        {
            playerProfileImages[7].sprite = _characterList[0].EquipItems[0].spList[0];
            playerProfileImages[7].rectTransform.pivot = playerProfileImages[7].sprite.pivot / playerProfileImages[7].sprite.rect.size;
        }
        else
            playerProfileImages[7].sprite = UIMask;

        // 얼굴장식
        if (_characterList[0].CheckEquipItems[1])
        {
            playerProfileImages[6].sprite = _characterList[0].EquipItems[1].spList[0];
            playerProfileImages[6].rectTransform.pivot = playerProfileImages[6].sprite.pivot / playerProfileImages[6].sprite.rect.size;
        }
        else
            playerProfileImages[6].sprite = UIMask;
        // 위옷
        if (_characterList[0].CheckEquipItems[2])
        {
            playerProfileImages[4].sprite = _characterList[0].EquipItems[2].spList[1];
            playerProfileImages[2].sprite = _characterList[0].EquipItems[2].spList[0];
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
        {
            playerProfileImages[8].sprite = _characterList[0].EquipItems[4].spList[0];
            playerProfileImages[8].rectTransform.pivot = playerProfileImages[8].sprite.pivot / playerProfileImages[8].sprite.rect.size;
        }
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
                mercenaryAProfileImages[7].sprite = _characterList[_index  +1].EquipItems[0].spList[0];
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
        if (_index == 3)
        {
            // 머리
            if (_characterList[_index + 1].CheckEquipItems[0])
                mercenaryDProfileImages[7].sprite = _characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryDProfileImages[7].sprite = UIMask;
            // 얼굴장식
            if (_characterList[_index + 1].CheckEquipItems[1])
                mercenaryDProfileImages[6].sprite = _characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryDProfileImages[6].sprite = UIMask;
            // 위옷
            if (_characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryDProfileImages[4].sprite = _characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryDProfileImages[2].sprite = _characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryDProfileImages[0].sprite = _characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryDProfileImages[4].sprite = UIMask;
                mercenaryDProfileImages[2].sprite = UIMask;
                mercenaryDProfileImages[0].sprite = UIMask;
            }
            // 투구
            if (_characterList[_index + 1].CheckEquipItems[4])
                mercenaryDProfileImages[8].sprite = _characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryDProfileImages[8].sprite = UIMask;
            // 갑옷
            if (_characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryDProfileImages[5].sprite = _characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryDProfileImages[3].sprite = _characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryDProfileImages[1].sprite = _characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryDProfileImages[5].sprite = UIMask;
                mercenaryDProfileImages[3].sprite = UIMask;
                mercenaryDProfileImages[1].sprite = UIMask;
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

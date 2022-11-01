using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetUpPanelController : MonoBehaviour
{
    [SerializeField] private PlayerStatus player = null;
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private Slider[] soundSliders = null;
    [SerializeField] private Slider[] portionUseConditonSliders = null;
    [SerializeField] private TextMeshProUGUI[] sleepTimeTexts;
    [SerializeField] private TextMeshProUGUI[] portionUseConditionPercents;
    

    public void SetCheckControlOnAutoPlay(bool _bool)
    {
        playerController.CheckControlOnAutoPlay = _bool;
    }

    public void SetSoundVolume(int _index)
    {
        SoundManager.Instance.SetSoundOption((ESound)_index, soundSliders[_index].value);
    }
    public void SetPortionUseCondition(int _index)
    {
        player.PortionAutoUsePercent[_index] = (int)portionUseConditonSliders[_index].value;
        portionUseConditionPercents[_index].text = ((int)portionUseConditonSliders[_index].value).ToString() +"%";
    }

    public void SetActiveSetUpPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }

    public void SetSleepMode(int _index)
    {
        sleepTimeTexts[(int)GameManager.Instance.SleepModeTime].color = Color.white;
        GameManager.Instance.SleepModeTime = (ESleepModeTime)_index;
        sleepTimeTexts[_index].color = Color.red;
    }
    public void SetSleeModeImmediately()
    {
        GameManager.Instance.OperateSleepMode();
    }
    public void SetSleeModeImmediately()
    {
        GameManager.Instance.OperateSleepMode();
    }
}

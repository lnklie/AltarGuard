using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetUpPanelController : MonoBehaviour
{
    [SerializeField] private Slider[] soundSliders = null;
    [SerializeField] private TextMeshProUGUI[] textMeshProUGUIs;

    public void SetSoundVolume(int _index)
    {
        SoundManager.Instance.SetSoundOption((ESound)_index, soundSliders[_index].value);
    }

    public void SetActiveSetUpPanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }

    public void SetSleepMode(int _index)
    {
        textMeshProUGUIs[(int)GameManager.Instance.SleepModeTime].color = Color.white;
        GameManager.Instance.SleepModeTime = (ESleepModeTime)_index;
        textMeshProUGUIs[_index].color = Color.red;
    }
}

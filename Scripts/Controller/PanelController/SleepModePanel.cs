using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SleepModePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sleepModeText = null;
    [SerializeField] private TextMeshProUGUI countText = null;

    [SerializeField] private bool isOpen = false;
    [SerializeField] private float keepTime = 0f;
    [SerializeField] private int count = 0;
    [SerializeField] private float maxKeepTime = 10f;
    private void Update()
    {
        if (!isOpen)
        {
            if(Input.anyKeyDown)
            {
                StartCoroutine(SetText(true));
            }
        }
        else
        {
            keepTime += Time.deltaTime;
            if (Input.anyKeyDown)
            {
                count++;
                keepTime = 0f;
                countText.text = "(" + count.ToString() + " / 5)";
            }

            if(keepTime >= maxKeepTime)
            {
                InitSleepMode();
                StartCoroutine(SetText(false));
            }
            if(count >= 5)
            {
                InitSleepMode();
                this.gameObject.SetActive(false);
                GameManager.Instance.IsSleepMode = false;
            }
        }
    }
    public void InitSleepMode()
    {
        keepTime = 0f;
        count = 0;
        countText.text = "(0 / 5)";
    }
    public IEnumerator SetText(bool _bool)
    {
        bool isEnd = false;
        isOpen = _bool;
        while (!isEnd)
        {
            if (!_bool)
            {
                sleepModeText.color -= new Color(0, 0, 0, 0.01f);
                countText.color -= new Color(0, 0, 0, 0.01f);
                if (sleepModeText.color.a <= 0f)
                {
                    isEnd = true;
                }
            }
            else
            {
                sleepModeText.color += new Color(0, 0, 0, 0.01f);
                countText.color += new Color(0, 0, 0, 0.01f);
                if (sleepModeText.color.a >= 1f)
                {
                    isEnd = true;
                }
            }

            yield return null;
        }
        
    }
}

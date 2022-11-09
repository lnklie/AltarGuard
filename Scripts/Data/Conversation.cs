using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Conversation : MonoBehaviour
{
    [SerializeField] private Animator ani = null;
    [SerializeField] private TextMeshProUGUI conversationText = null;
    [SerializeField] private Lines curLines = null;
    [SerializeField] private bool isTalk = false;
    [SerializeField] private float defaultDelayBetweenLetters = 0.1f;
    [SerializeField] private float timePadding = 1.0f;
    [SerializeField] public float letterSpeed = 1.0f;
    [SerializeField] public float readTime = 5f;


    public Lines CurLines { get { return curLines; } set { curLines = value; } }
    public bool IsTalk { get { return isTalk; } set { isTalk = value; } }
    private void Awake()
    {
        if (defaultDelayBetweenLetters < 0.02f)
            defaultDelayBetweenLetters = 0.02f;
    }

    public void Talk() 
    {
        StartCoroutine(Talk(curLines.script,curLines.scriptSpeed,curLines.scriptAniSpeed));
    }
    public IEnumerator Talk(string _text, float _textSpeed, float _frameAniSpeed)
    {
        if(isTalk)
        {
            isTalk = false;
            conversationText.text = null;
            ani.SetBool("isTalk", false);
            yield return new WaitForSeconds(curLines.scriptAniSpeed);
        }

        isTalk = true;
        ani.SetBool("isTalk", true);
        ani.speed = _frameAniSpeed;
        yield return new WaitForSeconds(1f / _frameAniSpeed);
        StartCoroutine(DisplayTyping(_text, _textSpeed));
        yield return new WaitForSeconds(_text.Length / readTime);
        conversationText.text = null;
        isTalk = false;
        ani.SetBool("isTalk", false);
    }
    IEnumerator DisplayTyping(string sub, float displayFor)
    {
        float timer = 0;
        conversationText.text = "";
        float timeBetweenLetters = (displayFor + .001f) / ((float)sub.Length);
        float bonusPadding = 0; //any displayFor time that's left after typing has finished gets added to the timePadding

        if (timeBetweenLetters > defaultDelayBetweenLetters)
        {
            timeBetweenLetters = defaultDelayBetweenLetters;
            bonusPadding = displayFor - timeBetweenLetters * sub.Length;
            if (bonusPadding < 0)//for fringe cases
                bonusPadding = 0;
        }

        while (sub.Length > 0 )
        {
            timer += Time.deltaTime;
            float onDis = Mathf.Round(timer / timeBetweenLetters);
            onDis -= conversationText.text.Length;
            for (int i = 0; i < onDis; i++)
            {
                conversationText.text += sub[0];
                sub = sub.Remove(0, 1);
                if (sub.Length <= 0)
                    break;
            }
            //scrollRect.verticalNormalizedPosition = 0.0f;//scroll back to the top
            yield return null;
        }

        //if hurried, post the rest of the phrase immediately and set scroll to max
        if (sub.Length > 0)
            conversationText.text += sub;
        yield return null;//give UI time to update before fixing scroll position

        if (timePadding + bonusPadding > 0)
            yield return new WaitForSeconds(timePadding + bonusPadding);
    }
}

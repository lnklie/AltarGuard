using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class IntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText = null;
    [SerializeField] private bool isChange = false;
    [SerializeField] private float backgroundSpeed = 0f;
    private void Start()
    {
        StartCoroutine(FlickerLoadingText());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            loadingText.text = "·ÎµùÁß";
            GameManager.Instance.LoadScene(1);
        }
        CameraMove();
    }
    public void CameraMove()
    {
        Camera.main.gameObject.transform.position -= new Vector3(backgroundSpeed * Time.deltaTime, 0, 0);
        if (Camera.main.gameObject.transform.position.x <= -40)
            Camera.main.gameObject.transform.position = new Vector3(20f, 0, -10);
    }
    public IEnumerator FlickerLoadingText()
    {
        bool _isUp = false;
        while(true)
        {
            if (loadingText.color.a >= 0.99f)
            {
                yield return new WaitForSeconds(0.5f);
                _isUp = false;
            }
            else if (loadingText.color.a <= 0f)
            {
                yield return new WaitForSeconds(0.5f);
                _isUp = true;
            }

            if(!_isUp)
                loadingText.color -= new Color(0, 0, 0, 0.01f);
            else
                loadingText.color += new Color(0, 0, 0, 0.01f);
            yield return null;
        }
    }
}

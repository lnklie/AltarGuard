using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScrollController : MonoBehaviour
{
    private RectTransform rectTransform = null;
    
    [SerializeField] private Vector3 preMousePos = Vector3.zero;
    [SerializeField] private float maxRectPos = 0f;
    [SerializeField] private float minRectPos = 0f;
    [SerializeField] private float scrollingSpeed = 0f;
    [SerializeField] private bool isScrolling = false;

    public bool IsScrolling { get { return isScrolling; } }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (IsPointerOverUIObject(Input.mousePosition) && !UIManager.Instance.IsUIOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                preMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                isScrolling = true;
                if (Input.mousePosition.y > preMousePos.y)
                {
                    if (rectTransform.anchoredPosition.y > minRectPos)
                    {
                        rectTransform.anchoredPosition -= Vector2.up * scrollingSpeed;
                    }

                }
                else if (Input.mousePosition.y < preMousePos.y)
                {
                    if (rectTransform.anchoredPosition.y < maxRectPos)
                    {
                        rectTransform.anchoredPosition += Vector2.up * scrollingSpeed;
                    }
                }
                preMousePos = Input.mousePosition;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isScrolling = false;
        }
    }
    public void InitRectPos()
    {
        rectTransform.anchoredPosition = new Vector2(0f, minRectPos);
    }
    public void SetLimitRectPos(float _max, float _min)
    {
        maxRectPos = _max;
        minRectPos = _min;
    }

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}

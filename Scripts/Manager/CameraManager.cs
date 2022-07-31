using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : CameraManager.cs
==============================
*/
public class CameraManager : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 minBound = Vector3.zero;
    private Vector3 maxBound = Vector3.zero;

    private float halfWidth = 0f;
    private float halfHeight = 0f;

    private Camera M_camera = null;
    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private BoxCollider2D bound = null;
    [SerializeField]
    private float speed = 0f;
    private void Start()
    {
        M_camera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = M_camera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
    private void Update()
    {
        Targeting();
    }

    private void Targeting()
    {
        // 카메라의 플레이어 타게팅
        if (target != null)
        {
            targetPosition = target.transform.position;
            targetPosition.z = this.gameObject.transform.position.z;

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, speed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }
    public void SetBound(BoxCollider2D newBound)
    {
        // 경계 설정
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}

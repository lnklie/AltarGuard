using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMesh textMesh;
    private float durationTime = 0f;

    private void Awake()
    {
        textMesh = this.GetComponent<TextMesh>();
    }
    private void Start()
    {
        SetDamageText(100);
    }
    void Update()
    {
        durationTime += Time.deltaTime;
        MoveText();
        PersistDurationTime();
    }
    public void MoveText()
    {
        Vector2 dir = new Vector2(0.25f, 0.25f);
        this.transform.position = Vector2.Lerp(this.transform.position, dir, Time.deltaTime);
    }
    public void SetDamageText(int _damage)
    {
        textMesh.text = _damage.ToString();
    }
    public void PersistDurationTime()
    {
        durationTime += Time.deltaTime;
        if (durationTime >= 2f)
        {
            durationTime = 0f;
            this.transform.gameObject.SetActive(false);
            this.transform.position = Vector2.zero;
        }
    }
}

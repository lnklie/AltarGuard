using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMesh textMesh;
    [SerializeField]
    private float durationTime = 0f;

    private void Awake()
    {
        textMesh = this.GetComponent<TextMesh>();
    }
    void Update()
    {
        durationTime += Time.deltaTime;
        MoveText();
        PersistDurationTime();
    }
    public void MoveText()
    {
        this.transform.Translate(Vector2.up * 1f * Time.deltaTime);
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
            this.transform.localPosition = Vector2.zero;
            this.transform.gameObject.SetActive(false);
        }
    }
}

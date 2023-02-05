using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] float ptsPerSecond;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text final;
    [SerializeField, TextArea] string textFormat;
    [SerializeField, TextArea] string textFormatFinal;

    public static Score Instance { get; private set; }

    public float Value
    {
        get => score;

        set
        {
            score = value;
            text.text = string.Format(textFormat, score.ToString("N0"));
        }
    }

    float score;

    void Awake()
    {
        Value = 0;
        Instance = this;
    }

    void Update()
    {
        if (Root.Instance.IsRooted) return;

        Value += ptsPerSecond * Time.deltaTime;
    }

    void OnDisable()
    {
        final.text = string.Format(textFormatFinal, score.ToString("N0")); 
        text.enabled = false;
    }
}

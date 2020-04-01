using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speech : MonoBehaviour
{
    [SerializeField]
    private GameObject speechBubble;
    [SerializeField]
    private TextMeshProUGUI textBox;
    [SerializeField]
    [TextArea]
    private List<string> proximityTexts = new List<string>();
    [SerializeField]
    [TextArea]
    private List<string> clickTexts = new List<string>();
    [SerializeField]
    private float offset = 300;

    private int proximitySpeechIndex = 0;
    private int clickSpeechIndex = 0;

    public engineerLines _engineerLines;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 position = transform.position;
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(position);
        speechBubble.GetComponent<RectTransform>().anchorMin = viewportPoint;
        speechBubble.GetComponent<RectTransform>().anchorMax = viewportPoint;
        speechBubble.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, offset, 0);
    }

    public void DisplayProximityText()
    {
        //todo: display text ui
        speechBubble.SetActive(true);

        _engineerLines.callTyping(false, proximitySpeechIndex);
        // textBox.text = textToDisplay;

        proximitySpeechIndex++;
        if (proximitySpeechIndex >= proximityTexts.Count)
        {
            proximitySpeechIndex = 0;
        }
    }


    public void DisplayClickText()
    {
        //todo: display text ui
        speechBubble.SetActive(true);

        _engineerLines.callTyping(true, clickSpeechIndex);

        clickSpeechIndex += 2;
        if (clickSpeechIndex >= clickTexts.Count)
        {
            clickSpeechIndex = 0;
        }
    }

    public void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }


    
}

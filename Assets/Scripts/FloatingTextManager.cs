using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    public void Show(string message
        , int fontSize
        , Color color
        , Vector3 position
        , Vector3 motion
        , float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.text.text = message;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        //Transfer world space to screen space so we can use it in the UI
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    //Floating text management
    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.isActive);

        if(txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.text = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
            return txt;
        }

        return txt;
    }

    private void Update()
    {
        foreach(FloatingText text in floatingTexts)
        {
            text.UpdateFloatingText();
        }
    }
}

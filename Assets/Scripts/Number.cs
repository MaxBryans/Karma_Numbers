using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Number : MonoBehaviour
{
    public Canvas_Controller CC; // N.B. injected by the creating Canvas Controller

    public int myValue;
    public TextMeshProUGUI text;
    public bool Highlighted;

    private RectTransform myRT;
    [SerializeField]
    public bool selected = false;
    private int selectedPosition;

    public void configure (int value, Vector2 position)
    {
        setNumber(value);

        myRT.sizeDelta = new Vector2(CC.width, CC.pitch);
        myRT.position = position;
        Highlight(false);
    }

    public void setNumber(int number)
    {
        myValue = number;
        text.text = myValue.ToString();
    }

    public void setPosition (Vector2 position)
    {
        myRT.position = position;
    }

    public void Highlight (bool On_Off)
    {
        if (!On_Off)
        {
            text.color = Color.white;
            Highlighted = false;
        }
        else
        {
            text.color = Color.green;
            Highlighted = true;
        }
    }

    void Awake()
    {
        myRT = GetComponent<RectTransform>();
    }

public void killMe()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Canvas_Controller CC; // N.B. injected by the creating Canvas Controller

    public int myValue;
    public int myPosition;
    public Text text;

    private RectTransform myRT;

    public void configure (int value, int position)
    {
        myValue = value;
        myPosition = position;
        text.text = value.ToString();

        myRT.sizeDelta = new Vector2(CC.width, CC.pitch);
        myRT.position = new Vector2(CC.width / 2f, CC.top - (myPosition * CC.pitch));
    }

    void Awake()
    {
        myRT = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

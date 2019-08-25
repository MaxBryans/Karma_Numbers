using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Canvas_Controller CC;

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
        myRT.position = new Vector2(CC.width / 2f, CC.top + (myPosition * CC.pitch));
    }

    // Start is called before the first frame update

    void Awake()
    {
        CC = FindObjectOfType<Canvas_Controller>(); // we will ONLY ever have 1 ... honest
        myRT = GetComponent<RectTransform>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

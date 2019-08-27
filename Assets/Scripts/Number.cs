using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Number : MonoBehaviour
{
    public Canvas_Controller CC; // N.B. injected by the creating Canvas Controller

    public int myValue;
    public Text text;

    private RectTransform myRT;
    [SerializeField]
    private bool selected = false;
    private int selectedPosition;

    public void configure (int value, Vector2 position)
    {
        myValue = value;
        text.text = value.ToString();

        myRT.sizeDelta = new Vector2(CC.width, CC.pitch);
        myRT.position = position;
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

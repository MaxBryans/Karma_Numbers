using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{

    public CanvasGroup PlayCanvas;
    public CanvasGroup LoadingCanvas;

    public RectTransform Title;
    public RectTransform Play;
    public RectTransform Controls;
    public RectTransform DirectionUp;
    public RectTransform DirectionDown;

    public GameObject numberPreFab;

    private Vector2 screen = new Vector2();
    private Number[] myNumbers;

    public float top   { get; private set; }
    public float width { get; private set; }
    public float pitch { get; private set; }

    void ConfigurePanels()
    {
        Title.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
        Play.sizeDelta = new Vector2(screen.x, screen.y * 0.7f);
        Controls.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
        DirectionUp.sizeDelta = DirectionDown.sizeDelta = new Vector2(screen.x / 4, screen.y * 0.7f);
        DirectionUp.position = new Vector3(screen.x * 7/8, screen.y / 2f);
        DirectionDown.position = new Vector3(screen.x * 1 / 8, screen.y / 2f);
    }

    public void SpawnNumbers (int[] numbers, bool Ascending)
    {
        SetLoadingCanvas(false);
        myNumbers = new Number[numbers.Length];
        if (Ascending) { DirectionUp.gameObject.SetActive(true); DirectionDown.gameObject.SetActive(false); }
        else { DirectionUp.gameObject.SetActive(false); DirectionDown.gameObject.SetActive(true); }

        for (int i = 0; i < numbers.Length; i++)
        {
            GameObject num = Instantiate(numberPreFab);
            Number numC = num.GetComponent<Number>();
            numC.CC = this; // tell the new number who is boss
            numC.configure(numbers[i], i);
            num.transform.SetParent(Play.transform);

            myNumbers[i] = numC;
        }
    }

    private void SetLoadingCanvas (bool On_Off)
    {
        PlayCanvas.enabled = !On_Off;
        LoadingCanvas.enabled = On_Off;
    }


    void Awake ()
    {
        PlayCanvas.enabled = false;
        LoadingCanvas.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        screen.x = width = Screen.width;
        screen.y = Screen.height;
        pitch = screen.y * 0.07f;
        top = screen.y * (0.85f - 0.035f);
        ConfigurePanels();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}

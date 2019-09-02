using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{
    public GC gc; // injected by GC upon Load

    public Load_Rotator LoadScreen;

    public CanvasGroup PlayCanvas;
    public CanvasGroup LoadingCanvas;

    public RectTransform Title;
    public RectTransform Play;
    public RectTransform Controls;
    public RectTransform Instructions;
    public Text Upper;
    public Text Lower;
    public RectTransform Welcome;
    public RectTransform StartIntro;
    public RectTransform GameOver;

    public GameObject numberPreFab;

    public Vector2 screen { get; private set; }
    public Number[] myNumbers { get; private set; }
    public int[] gameNumbers { get; private set; }
    public bool Ascending = false;
    public Vector2[] myPositions { get; private set; } // the "centre point) of each grid to be displayed.  Storing reference for speed later

    public float top   { get; private set; }
    public float width { get; private set; }
    public float pitch { get; private set; }
    public float offset { get; private set; }

    void ConfigurePanels()
    {
        Title.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
        Play.sizeDelta = new Vector2(screen.x, screen.y * 0.7f);
        Controls.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
        Instructions.sizeDelta = new Vector2(screen.x / 4, screen.y * 0.7f);
        Instructions.position = new Vector3(screen.x * 1 / 8, screen.y / 2f);
    }

    public void SwapNumbers (int index1, int index2)
    {
        int temp = myNumbers[index1].myValue;
        myNumbers[index1].setNumber(myNumbers[index2].myValue);
        myNumbers[index2].setNumber(temp);
        bool tmpbool = myNumbers[index1].selected;
        myNumbers[index1].selected = myNumbers[index2].selected;
        myNumbers[index2].selected = tmpbool;
    }

    public void MoveBox (int box, Vector2 offset)
    {
        myNumbers[box].setPosition(offset + myPositions[box]);
    }

    public void DoOffsets (int selected, int hoverover)
    {
            for (int i = 0; i < myNumbers.Length; i++)
            {
              myNumbers[i].setPosition(myPositions[i]);
              if (i > selected && i <= hoverover) myNumbers[i].setPosition(myPositions[i-1]);
              if (i < selected && i >= hoverover) myNumbers[i].setPosition(myPositions[i + 1]);

             }
    }

    public void SelectBox (int box)
    {
        DeselectAllBoxes();
        myNumbers[box].Highlight(true);
    }

    public void UpdateNumbers (int[] values)
    {
        for (int i = 0, len = values.Length; i < len; i++)
        {
            myNumbers[i].setNumber(values[i]);
            myNumbers[i].Highlight(false);
            myNumbers[i].setPosition(myPositions[i]);
        }
    }

    public void CounterFinished ()
    {
        StateChange(GameState.Playing);
    }

    public void NewGameCalled ()
    {
        StateChange(GameState.Loading);
    }

    public void StateChange (GameState state)
    {
        switch (state)
        {
            case GameState.Loading:
                Welcome.gameObject.SetActive(false);
                LoadingCanvas.gameObject.SetActive(true);
                StartIntro.gameObject.SetActive(false);
                Play.gameObject.SetActive(false);
                GameOver.gameObject.SetActive(false);
                gc.ChangeState(GameState.Loading);
                break;

            case GameState.Welcome:
                Welcome.gameObject.SetActive(true);
                LoadingCanvas.gameObject.SetActive(false);
                StartIntro.gameObject.SetActive(false);
                Play.gameObject.SetActive(false);
                GameOver.gameObject.SetActive(false);
                break;

            case GameState.Game_Intro:
                Welcome.gameObject.SetActive(false);
                LoadingCanvas.gameObject.SetActive(false);
                StartIntro.gameObject.SetActive(true);
                StartIntro.gameObject.GetComponentInChildren<Intro>().StartCounter(Ascending);
                Play.gameObject.SetActive(false);
                GameOver.gameObject.SetActive(false);
                break;

            case GameState.Playing:
                Welcome.gameObject.SetActive(false);
                LoadingCanvas.gameObject.SetActive(false);
                StartIntro.gameObject.SetActive(false);
                Play.gameObject.SetActive(true);
                GameOver.gameObject.SetActive(false);
                GameStart();
                gc.ChangeState(GameState.Playing);
                break;

            case GameState.Game_Over:
                Welcome.gameObject.SetActive(false);
                LoadingCanvas.gameObject.SetActive(false);
                StartIntro.gameObject.SetActive(false);
                Play.gameObject.SetActive(false);
                GameOver.gameObject.SetActive(true);
                foreach (Number num in myNumbers) num.killMe();
                break;

            case GameState.Awaiting_Start:
                Welcome.gameObject.SetActive(false);
                LoadingCanvas.gameObject.SetActive(false);
                StartIntro.gameObject.SetActive(false);
                Play.gameObject.SetActive(false);
                GameOver.gameObject.SetActive(false);
                break;

            default:
                break;

        }
    }

    public void DeselectAllBoxes ()
    {
        foreach (Number num in myNumbers) num.Highlight(false);
    }

    private void SetPositions (int number)
    {
        if (number <=0 || number > 10)
        {
            Debug.Log("Cannot set Positions: improper parameter");
            return;
        }

        float mod = 0;
        if (number % 2 != 0) mod = 0.5f;
        mod += Mathf.Floor(number / 2);
        mod = (screen.y / 2f) + (mod * pitch);
        myPositions = new Vector2[number];
        for (int i = 0; i < number; i++)
        {
            myPositions[i] = new Vector2(screen.x / 2, mod - ((float)(i+0.5) * pitch));
        }
    }

    public void SpawnNumbers (int[] numbers, bool Ascending)
    {
        // store what's been passed for use after co-routine
        gameNumbers = numbers;
        this.Ascending = Ascending;

        StateChange(GameState.Game_Intro);

    }

    private void GameStart()
    {
        offset = (10 - gameNumbers.Length) / 2 * pitch;
        SetLoadingCanvas(false);
        myNumbers = new Number[gameNumbers.Length];
        SetPositions(gameNumbers.Length);
        if (Ascending) {
            Upper.text = "Low";
            Upper.color = Color.red;
            Lower.text = "High";
            Lower.color = Color.green;
        }
        else {
            Lower.text = "Low";
            Lower.color = Color.red;
            Upper.text = "High";
            Upper.color = Color.green;
        }

        for (int i = 0; i < gameNumbers.Length; i++)
        {
            GameObject num = Instantiate(numberPreFab);
            Number numC = num.GetComponent<Number>();
            numC.CC = this; // tell the new number who is boss
            numC.configure(gameNumbers[i], myPositions[i]);
            num.transform.SetParent(Play.transform);

            myNumbers[i] = numC;
        }
    }

    private void SetLoadingCanvas (bool On_Off)
    {
        PlayCanvas.enabled = !On_Off;
        LoadScreen.Enable(On_Off);
    }

    // Button Management
    public void Welcome_Start_Button ()
    {
        StateChange(GameState.Loading);
    }

    void Awake ()
    {
        PlayCanvas.enabled = false;
        LoadingCanvas.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        screen = new Vector2(width = Screen.width, Screen.height);
        pitch = screen.y * 0.07f;
        top = screen.y * (0.85f - 0.035f);
        ConfigurePanels();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Loading,
    Welcome,
    Awaiting_Start,
    Game_Intro,
    Playing,
    Game_Over
}


// Game Controller (GC) class
// responsible for controlling the Game State and other components
public class GC : MonoBehaviour
{

    #region Members
    public GameState state = GameState.Loading;

    public Karma_API api;
    private Karma_API_JSON level = new Karma_API_JSON();

    [SerializeField] private bool levelLoaded = false;
    [SerializeField] private bool paused = false;
    private Vector2 screen;
    private Vector2 offset = new Vector2(0, 0);
    private Vector2 datum = new Vector2(0, 0);
    [SerializeField] private bool Ascending = false;
    [SerializeField] private int[] numbers;
    [SerializeField] private int[] MovingNumbers;

    public Vector2 mouse = new Vector2(0,0); // always handy for debugging, stored as Ratio of screen size (0-1)
    [SerializeField]
    private int BoxSelected = -1;
    [SerializeField]
    private int BoxHoverOver = -1;
    [SerializeField]
    private int OldHover = -1;
    [SerializeField]
    private bool MouseDown = false;
    [SerializeField]
    private float timer = 0f;
    private bool timerRunning = false;
    public Text timertext;

    // need to talk to the Canvas_Controller
    public Canvas_Controller CC;

    #endregion

    #region Methods

    // Call the API service to get a level
    public void GetLevel ()
    {
        state = GameState.Loading;
        StartCoroutine(api.GetLevel(OnLevelLoad));
    }

    // Process Response
    public void OnLevelLoad(Karma_API_JSON Newlevel)
    {
        level = Newlevel;
        Ascending = false;
        if (level.order == "ASC") Ascending = true;
        numbers = level.numbers;
        levelLoaded = true;
        Debug.Log("Controller says top number = " + numbers[0].ToString());
        // State Change
        ChangeState();
    }

    private void ChangeState()
    {
        if (state == GameState.Loading)
        {
            state = GameState.Awaiting_Start;
            Debug.Log("Numbers are in order : " + CheckNumbers());
            // Not the right place, but an initial test
            CC.SpawnNumbers(numbers,Ascending);
            CC.StateChange(GameState.Game_Intro);
            timer = 0f;
            timerRunning = true;
        }
    }

    private bool CheckNumbers()
    {
        if (Ascending)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] < numbers[i+1])
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] > numbers[i+1])
                {
                    return false;
                }
            }
        }
        return true;
    }

    // if there's a touch .. use that as "mouse" ... else use the mouse
    void GetMouseTouch()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            mouse.x = touch.position.x / screen.x;
            mouse.y = touch.position.y / screen.y;
            // mouse = new Vector2(touch.position.x / screen.x, touch.position.y / screen.y);
        }
        else
        {
            mouse.x = Input.mousePosition.x / screen.x;
            mouse.y = Input.mousePosition.y / screen.y;
            // mouse = new Vector2(Input.mousePosition.x / screen.x, Input.mousePosition.y / screen.y);
        }

        // now to populate what "box" it's touching ... only if playing
        if (state == GameState.Playing)
        {
            BoxHoverOver = -1;
            for (int i = 0; i < CC.myPositions.Length; i++)
            {
                float t = (CC.myPositions[i].y + (CC.pitch/2)) / screen.y;
                float b = (CC.myPositions[i].y - (CC.pitch / 2)) / screen.y;
                if (t > mouse.y && b < mouse.y) BoxHoverOver = i;
            }
        }
    }

    private void CheckSwap()
    {
        // do something

        if (BoxSelected != -1 && BoxHoverOver != -1 && BoxSelected != BoxHoverOver)
        {
            ReOrder(BoxSelected,BoxHoverOver);
        }
        CC.UpdateNumbers(numbers);
        BoxSelected = -1;
        MouseDown = false;
        if (CheckNumbers()) timerRunning = false;

    }

    private void ReOrder (int selected, int hover)
    {
        int len = numbers.Length;
        int[] newNum = new int[len];
        for (int i = 0; i < len; i++)
        {
            if (i == hover) newNum[i] = numbers[selected];
            else if (i >= selected && i < hover) newNum[i] = numbers[i + 1];
            else if (i <= selected && i > hover) newNum[i] = numbers[i - 1];
            else { newNum[i] = numbers[i]; }
        }
        numbers = newNum;
    }

    private void CheckMove()
    {
        // move the tiles
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        int sec = Mathf.FloorToInt(timer);
        int mod = sec % 60;
        string padding = "";
        if (mod < 10) padding = "0";
        timertext.text = ((sec - mod) / 60).ToString() + ":" + padding+  mod.ToString();
    }

    #endregion

    #region Unity API

    // Start is called before the first frame update
    void Start()
    {
        screen = new Vector2(Screen.width, Screen.height);
        offset.x = screen.x * 0.1f;
        CC.gc = this;
        GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseTouch();


        if (state == GameState.Playing)
        {
            // frame mouse first clicked
            if (Input.GetMouseButtonDown(0) )
            {
                datum.y = mouse.y;
                MouseDown = true;
                BoxSelected = BoxHoverOver;
                if (BoxHoverOver != -1)
                {
                    CC.SelectBox(BoxHoverOver);
                }
            }
            // mouse is already down
            else if (Input.GetMouseButton(0))
            {
                CheckMove();
                Debug.Log("CheckMove()");
                offset.y = (mouse.y - datum.y) * screen.y;
                if (BoxSelected != -1 && BoxHoverOver != -1)
                {
                    CC.DoOffsets(BoxSelected, BoxHoverOver);
                    CC.MoveBox(BoxSelected, offset);
                }

            }
            // mouse released
            if (Input.GetMouseButtonUp(0))
            {
                datum.y = 0;
                Debug.Log("CheckSwap()");
                CheckSwap();
            }

            // fallout case ... if player has scrolled over/under the play field ... need to drop
            if (OldHover >=0 && BoxHoverOver == -1 && BoxSelected != -1)
            {
                BoxHoverOver = OldHover;
                CheckSwap();
            }

            // Update timer
            if (timerRunning) UpdateTimer();
        }


        // store HoverOver as a reference at end of frame
        OldHover = BoxHoverOver;
    }
    #endregion
}

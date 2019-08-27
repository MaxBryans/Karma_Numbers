using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// Game Controller (GC) class
// responsible for controlling the Game State and other components
public class GC : MonoBehaviour
{
    private enum GameState
    {
        Loading,
        Awaiting_Start,
        Playing,
        Paused
    }

    #region Members

    public Karma_API api;
    private Karma_API_JSON level = new Karma_API_JSON();
    [SerializeField] private GameState state = GameState.Loading;
    [SerializeField] private bool levelLoaded = false;
    [SerializeField] private bool paused = false;
    private Vector2 screen;
    [SerializeField] private bool Ascending = false;
    [SerializeField] private int[] numbers;

    [SerializeField] private Vector2 mouse = new Vector2(); // always handy for debugging, stored as Ratio of screen size (0-1)

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
        }
        else
        {
            mouse.x = Input.mousePosition.x / screen.x;
            mouse.y = Input.mousePosition.y / screen.y;
        }
    }

    #endregion

    #region Unity API

    // Start is called before the first frame update
    void Start()
    {
        screen = new Vector2(Screen.width, Screen.height);
        GetLevel();

    }

    // Update is called once per frame
    void Update()
    {
        GetMouseTouch();
    }
    #endregion
}

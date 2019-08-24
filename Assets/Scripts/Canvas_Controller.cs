using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{

    public RectTransform Title;
    public RectTransform Play;
    public RectTransform Controls;

    private Vector2 screen = new Vector2();


    void ConfigurePanels()
    {
        Title.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
        Play.sizeDelta = new Vector2(screen.x, screen.y * 0.7f);
        Controls.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
    }

    // Start is called before the first frame update
    void Start()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;
        ConfigurePanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

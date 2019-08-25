using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{

    public RectTransform Title;
    public RectTransform Play;
    public RectTransform Controls;

    public GameObject numberPreFab;

    private Vector2 screen = new Vector2();
    private Number[] myNumbers;

    public float top;
    public float width;
    public float pitch;

    void ConfigurePanels()
    {
        Title.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
        Play.sizeDelta = new Vector2(screen.x, screen.y * 0.7f);
        Controls.sizeDelta = new Vector2(screen.x, screen.y * 0.15f);
    }

    public void SpawnNumbers (int[] numbers)
    {
        myNumbers = new Number[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            GameObject num = Instantiate(numberPreFab);
            Number numC = num.GetComponent<Number>();
            numC.configure(numbers[i], i);
            num.transform.SetParent(Play.transform);

            myNumbers[i] = numC;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        screen.x = width = Screen.width;
        screen.y = Screen.height;
        pitch = screen.y * 0.07f;
        top = screen.y * (0.15f + 0.07f);
        ConfigurePanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

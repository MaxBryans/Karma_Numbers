using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Intro : MonoBehaviour
{
    public Canvas_Controller CC;
    public TextMeshProUGUI direction;
    public TextMeshProUGUI counter;
    public RectTransform counterbox;
    private Vector3 O_Scale;

    public void StartCounter (bool Ascending)
    {
        switch (Ascending)
        {
            case true:
                direction.text = "ASCENDING";
                direction.color = Color.green;
                break;
            case false:
                direction.text = "DESCENDING";
                direction.color = Color.red;
                break;
        }
        StartCoroutine(timer());
    }

    private IEnumerator timer ()
    {
        float myCount = 0f;
        counter.text = "3";
        while (myCount <=1f)
        {
            myCount += Time.deltaTime;
            ScaleMyBox(0.5f + myCount);
            yield return null;
        }

        myCount = 0f;
        counter.text = "2";
        while (myCount <= 1f)
        {
            myCount += Time.deltaTime;
            ScaleMyBox(0.5f + myCount);
            yield return null;
        }

        myCount = 0f;
        counter.text = "1";
        while (myCount <= 1f)
        {
            myCount += Time.deltaTime;
            ScaleMyBox(0.5f + myCount);
            yield return null;
        }

        myCount = 0f;
        counter.text = "GO !!";
        while (myCount <= 0.5f)
        {
            myCount += Time.deltaTime;
            ScaleMyBox(1f + myCount);
            yield return null;
        }
        CC.CounterFinished();
    }

    private void ScaleMyBox(float scaleFactor)
    {
        counterbox.localScale = O_Scale * (scaleFactor);
    }

    // Start is called before the first frame update
    void Start()
    {
        O_Scale = counterbox.localScale;
    }

}

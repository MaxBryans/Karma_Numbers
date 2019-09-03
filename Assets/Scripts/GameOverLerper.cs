using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLerper : MonoBehaviour
{
    public RectTransform myRT;
    public TweenFactory globalTF;
    public Button myButton;

    public float TransitionTime;
    public Tween myTween;

    public float StartScale = 0.5f;
    public float FinishScale = 1f;

    private Vector3 StartPosition;
    private Vector3 FinalPosition;

 

    void Start ()
    {
        FinalPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        StartPosition = new Vector3(Screen.width / 2, Screen.height + (myRT.sizeDelta.y / 2), 0f);
    }

    public void DoIt ()
    {
        StartCoroutine(DoMyTween());
    }

    IEnumerator DoMyTween()
    {
        float t = 0;
        myButton.enabled = false;
        while (t <= TransitionTime)
        {
            myRT.localPosition = globalTF.DoTweenV3(StartPosition, FinalPosition, myTween, t / TransitionTime);
            t += Time.deltaTime;
            yield return null;
        }
        myButton.enabled = true;
    }

    void OnDestroy ()
    {
        StopAllCoroutines();
    }


}

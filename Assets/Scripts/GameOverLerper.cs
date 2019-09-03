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

    [SerializeField] private Vector3 StartPosition;
    [SerializeField] private Vector3 FinalPosition;

 

    void Awake ()
    {
        FinalPosition = Vector3.zero;
        StartPosition = new Vector3(0f, Screen.height, 0f);
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
            myRT.localScale = Vector3.one * (StartScale + globalTF.DoTween(myTween, (FinishScale - StartScale) * (t / TransitionTime)));
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

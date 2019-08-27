using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_Rotator : MonoBehaviour
{
    private RectTransform me;
    private bool IsActive;
    private Image[] myImages;

    void Awake ()
    {
        me = GetComponent<RectTransform>();
        myImages = GetComponents<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive) me.Rotate(Vector3.back, 60f * Time.deltaTime);
    }

    public void Enable(bool On_Off)
    {
        if (On_Off)
        {
            foreach (Image img in myImages) img.enabled = true;
        }
        else
        {
            foreach (Image img in myImages) img.enabled = false;
        }
        IsActive = On_Off;
    }


}

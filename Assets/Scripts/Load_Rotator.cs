using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_Rotator : MonoBehaviour
{
    private RectTransform me;

    void Awake ()
    {
        me = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        me.Rotate(Vector3.back, 60f * Time.deltaTime);
    }
}

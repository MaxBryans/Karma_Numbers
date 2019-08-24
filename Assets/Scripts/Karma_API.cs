using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


// may want to edit later to allow asynchronous loading of a "next" level

public class Karma_API : MonoBehaviour
{
    #region Members
    private const string KarmaPath = "https://unity.karma.live/api/level";
    #endregion

    #region Public Methods

    public IEnumerator GetLevel(Action<Karma_API_JSON> callback)
    {
        return CallAPI(KarmaPath, callback);
    }

    #endregion

    #region Private Methods
    private IEnumerator CallAPI(string url, Action<Karma_API_JSON> callback)
    {

        WWW www = new WWW(url);
        yield return www;

        // Breakoutcode should we get garbage
        if (!IsResponseValid(www)) yield break;

        // Should have Karma API string now ... so to convert
        Debug.Log(www.text);
        Karma_API_JSON lvl = JsonUtility.FromJson<Karma_API_JSON>(www.text);
        Debug.Log(lvl.numbers[0].ToString());
        callback(lvl);
    }

    // check for error response (routine from Uni coursework)
    private bool IsResponseValid(WWW www)
    {
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Bad Connection");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("Bad Data");
            return false;
        }
        else // all good
        {
            return true;
        }
    }



    #endregion

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(CallAPI());
    }

    // insurance
    void OnDestroy()
    {
        StopAllCoroutines();
    }

    #endregion

}

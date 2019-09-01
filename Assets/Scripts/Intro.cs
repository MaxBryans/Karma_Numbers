using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Canvas_Controller CC;
    public Text direction;
    public Text counter;

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
        counter.text = "3";
        yield return new WaitForSeconds(1);
        counter.text = "2";
        yield return new WaitForSeconds(1);
        counter.text = "1";
        yield return new WaitForSeconds(1);
        counter.text = "GO !!";
        yield return new WaitForSeconds(0.5f);
        CC.CounterFinished();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoTextAdder : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timedMethod());
    }

    public IEnumerator timedMethod()
    {
        yield return null;
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(800f, 2000f) / 1000f);
            Chatbox.AddText("Hello world " + Random.Range(1000,9999));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

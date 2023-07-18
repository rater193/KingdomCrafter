using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chatbox : MonoBehaviour
{
    public static Chatbox Singleton = null;

    private GameObject _prefabText;
    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        _prefabText = Resources.Load<GameObject>("Prefabs/GUI/Chatbox_Text");
    }

    //This adds text to the textbox
    public static TMP_Text AddText(string text)
    {
        //Stop executing if the singleton has not been set
        if (!Singleton) return null;
        
        //Now to create the text
        TMP_Text ret = null;
        
        GameObject newObj = GameObject.Instantiate<GameObject>(Singleton._prefabText, Singleton.transform);
        ret = newObj.transform.Find("TMP_Text").GetComponent<TMP_Text>();
        ret.text = text;
        
        return ret;
    }

    public static TMP_Text Shout(string text, Vector3 positionToShoutFrom, float maxHearingDistance)
    {
        if (!Singleton || !CharacterController.Singleton) return null;
        float distanceToLocalPlayer =
            Vector3.Distance(CharacterController.Singleton.transform.position, positionToShoutFrom);
        
        if(distanceToLocalPlayer<=maxHearingDistance)
        {
            return AddText(text);
        }
        
        return null;

    }
}

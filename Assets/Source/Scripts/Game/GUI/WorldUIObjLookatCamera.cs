using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIObjLookatCamera : MonoBehaviour
{
    private Camera mainCam;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCam.transform.rotation;
    }
}

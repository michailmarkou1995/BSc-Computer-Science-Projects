using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshotURP : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.A))
        {
            ScreenCapture.CaptureScreenshot("GameScreenshot.png");
        } 
    }
}

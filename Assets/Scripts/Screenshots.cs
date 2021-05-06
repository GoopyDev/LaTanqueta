using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshots : MonoBehaviour
{
    public int NroActual = 0;
    public string NroScreenshot = "00";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ScreenCapture.CaptureScreenshot("DEMO_MEDIA/"+NroScreenshot+".png");
            NroActual++;
            NroScreenshot = NroActual.ToString();
        }
    }
}
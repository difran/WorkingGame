using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelect : MonoBehaviour {

    CameraDrag cameraFirstFloor;
    CameraDrag cameraSecondFloor;
    CameraFixed cameraAllMAp;
   
    // Use this for initialization
    void Start () {

        CameraDrag[] camerasDrags = this.GetComponents<CameraDrag>();

        cameraFirstFloor = camerasDrags[1];
        cameraSecondFloor = camerasDrags[0];

        cameraAllMAp = this.GetComponent<CameraFixed>();
    }

    public void LookAtFirstFloor()
    {
        cameraFirstFloor.enabled = true;
        cameraSecondFloor.enabled = false;
        cameraAllMAp.enabled = false;
    }

    public void LookAtSecondFloor()
    {
        cameraFirstFloor.enabled = false;
        cameraSecondFloor.enabled = true;
        cameraAllMAp.enabled = false;
    }

    public void LookAtAllMap()
    {
        cameraFirstFloor.enabled = false;
        cameraSecondFloor.enabled = false;
        cameraAllMAp.enabled = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlannyInGameInfo : MonoBehaviour{

    public FlannysMenu flanny;
    public BubbleColor color;

    public FlannysMenu Flanny
    {
        get
        {
            return flanny;
        }
        set
        {
            flanny = value;
        }
    }

    public BubbleColor Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }

}

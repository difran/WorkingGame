using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourcesList : MonoBehaviour {

    public List<Sprite> colours;
    public List<Sprite> flannys;

    public Sprite GetColor(BubbleColor color)
    {
        return colours[(int)color];
    }

    public Sprite GetFlanny(FlannysMenu flanny)
    {
        return flannys[(int)flanny];
    }

}

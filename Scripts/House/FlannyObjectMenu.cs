using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlannyObjectMenu : MonoBehaviour {

    public FlannysMenu flanny;
    public BubbleColor color;

    public Image myColor;
    public Image myFlanny;

    GameObject resourcesList;
    ResourcesList r;

	// Use this for initialization
	void Start () {
       
        SetFlannyUI(flanny, color);
    }

    public void SetFlannyUI(FlannysMenu flannyU, BubbleColor colorU)
    {
        resourcesList = GameObject.Find("Canvas/Menu Flannys");
        r = resourcesList.GetComponent<ResourcesList>();
        flanny = flannyU;
        color = colorU;
        myColor.sprite = r.GetColor(colorU);
        myFlanny.sprite = r.GetFlanny(flannyU);
    }
}

public enum FlannysMenu
{
    Flanny01,
    Flanny02,
    Flanny03,
    Flanny04,
    Flanny05,
    Flanny06,
    Flanny07,
    Flanny08,
    Flanny09,
    Flanny10,
    Flanny11,
    Flanny12,
    Flanny13,
    Flanny14,
    Flanny15,
}

public enum BubbleColor
{
    Green,
    LightBlue,
    Pink,
    Violet,
    Yellow,
}

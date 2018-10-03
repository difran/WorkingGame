using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using MenuHouse;

public class InputOnUi : MonoBehaviour {

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    PullMenuObject objectMenu;

    public GameObject canvas;

    void Start()
    {
       
        //Fetch the Raycaster from the GameObject (the Canvas)
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        eventSystem = canvas.GetComponent<EventSystem>();

        objectMenu = this.gameObject.GetComponent<PullMenuObject>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject flanny = GetSomeObjectUi("Flanny");
            if(flanny != null)
            objectMenu.OnTouchBegin(flanny);
        }
    }


    public GameObject GetSomeObjectUi(string name, bool modifiquedPosition = false, float x = 0, float y = 0)
    {
        //Set up the new Pointer Event
        pointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

       if(modifiquedPosition) pointerEventData.position -= new Vector2(x, y);

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        raycaster.Raycast(pointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
            if (result.gameObject.name == name)
            {
                return(result.gameObject);
            }
        }

        return null;
    }


  
}
